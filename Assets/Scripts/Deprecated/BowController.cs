using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour {

    private Vector3 baseScale;

    public int baseNote = NOTES.C3;

    private MIDIPlayer midiPlayer;

    private int ratio;

    private float millis;

	// Use this for initialization
	void Start () {
        baseScale = this.transform.localScale;
        ratio = 0;
        millis = -1000;
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKey("j"))
        {
            millis += 20;

            if (millis >= 1000)
            {
                millis = 0;
                if (ratio < 11)
                    ratio++;
                this.midiPlayer.setNote(ratio + this.baseNote);
            }
        }
        else if (Input.GetKey("k"))
        {
            millis -= 0;
            if (millis < -1000)
            {
                millis = 1000;

                if (ratio > 0)
                    ratio--;
                this.midiPlayer.setNote(ratio + this.baseNote);
            }
        }

        transform.localScale = new Vector3(baseScale.x * (0.5f + 0.5f / 12 * ratio), baseScale.y, baseScale.z);
    }
}
