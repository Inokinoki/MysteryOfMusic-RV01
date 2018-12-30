using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        Debug.Log("Test");
        MIDIPlayer midiplayer = GameObject.FindGameObjectWithTag("MIDIPlayer").GetComponent<MIDIPlayer>();
        midiplayer.setNote(Random.Range(61, 75));
    }
}
