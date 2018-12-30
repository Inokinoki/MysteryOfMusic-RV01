using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CSharpSynth.Effects;
using CSharpSynth.Sequencer;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;


[RequireComponent(typeof(AudioSource))]
public class MIDIPlayer : MonoBehaviour
{
    //Try also: "FM Bank/fm" or "Analog Bank/analog" for some different sounds
    public string bankFilePath = "GM Bank/gm";

    public int bufferSize = 1024;

    public int midiNote = 60;

    public int midiNoteVolume = 100;

    [Range(0, 127)] //From Piano to Gunshot

    public int midiInstrument = 0;

    private float[] sampleBuffer;

    private float gain = 1f;

    private MidiSequencer midiSequencer;

    private StreamSynthesizer midiStreamSynthesizer;

    private float sliderValue = 1.0f;

    private float maxSliderValue = 127.0f;

    private List<SingleNote> notePlaying; // The note is being played

    // Awake is called when the script instance
    // is being loaded.
    void Awake()
    {
        midiStreamSynthesizer = new StreamSynthesizer(44100, 2, bufferSize, 40);
        sampleBuffer = new float[midiStreamSynthesizer.BufferSize];
        
        midiStreamSynthesizer.LoadBank(bankFilePath);
        
        midiSequencer = new MidiSequencer(midiStreamSynthesizer);
        
        //These will be fired by the midiSequencer when a song plays. Check the console for messages if you uncomment these
        midiSequencer.NoteOnEvent += new MidiSequencer.NoteOnEventHandler(MidiNoteOnHandler);
        midiSequencer.NoteOffEvent += new MidiSequencer.NoteOffEventHandler(MidiNoteOffHandler);
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        //This uses the Unity specific float method we added to get the buffer
        midiStreamSynthesizer.GetNext(sampleBuffer);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = sampleBuffer[i] * gain;
        }
    }

    public void MidiNoteOnHandler(int channel, int note, int velocity)
    {
        Debug.Log("NoteOn: " + note.ToString() + " Velocity: " + velocity.ToString());
    }

    public void MidiNoteOffHandler(int channel, int note)
    {
        Debug.Log("NoteOff: " + note.ToString());
    }

    // Start is called just before any of the
    // Update methods is called the first time.
    void Start()
    {
        // Init currently playing note list
        this.notePlaying = new List<SingleNote>();
    }

    /**
     * Play one single note
     * The note can be referenced by constants in class NOTES
     * When note==-1, just stop the note which we just played using this method
     */
    public void setNote(int note)
    {
        midiStreamSynthesizer.NoteOff(0, midiNote);

        if (note != -1)
        {
            this.midiNote = note;
            midiStreamSynthesizer.NoteOn(0, midiNote, midiNoteVolume, midiInstrument);
        }
    }

    /**
     * Add a note to be played
     * With this function we can play multiple notes
     */
    public void AddNote(SingleNote note)
    {
        // Add a simple note and play it
        note.StartAt = Time.time;
        this.notePlaying.Add(note);

        Debug.Log("Playing " + this.notePlaying.Count + " to be played " + note.Note);

        if (note.Note >= 0)
        {
            // Begin to play
            midiStreamSynthesizer.NoteOn(0, note.Note, midiNoteVolume, midiInstrument);
        }
    }

    /**
     * Remove a note being played
     * With this function we can forcely cancel a currently playing note
     */
    public void RemoveNote(SingleNote _note)
    {
        Debug.Log("Playing " + this.notePlaying.Count);

        this.notePlaying.ForEach(delegate (SingleNote note)
        {
            if (note.Note == _note.Note)
            {
                this.midiStreamSynthesizer.NoteOff(0, _note.Note);
                this.notePlaying.Remove(note);
            }
        });
    }

    // Update is called every frame, if the
    // MonoBehaviour is enabled.
    // To garantee the real-time playing of note, we choose to use FixedUpdate
    void Update()
    {

    }

    void FixedUpdate()
    {
        // Delete the expired note and stop playing it
        this.notePlaying.ForEach(delegate (SingleNote note)
        {
            if (!note.IsUnlimited && note.StartAt + note.Duree < Time.time)
            {
                this.midiStreamSynthesizer.NoteOff(0, note.Note);
                this.notePlaying.Remove(note);
            }
        });
    }
}
