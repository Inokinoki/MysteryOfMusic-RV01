
public class SingleNote
{
    public int Note { get; set; }       // Note to be played

    public float Duree { get; set; }    // How long the note will be lasted

    public float StartAt { get; set; }  // Set by MIDIPlayer, the time when the note is played

    public bool IsUnlimited { get; set; }   // If the note is played permanently

    public SingleNote(int note, bool isUnlimited)
    {
        this.Note = note;
        this.Duree = 0;
        this.IsUnlimited = isUnlimited;
        this.StartAt = 0;
    }

    public SingleNote(int note, float duree)
    {
        this.Note = note;
        this.Duree = duree;
        this.IsUnlimited = false;
        this.StartAt = 0;
    }

}
