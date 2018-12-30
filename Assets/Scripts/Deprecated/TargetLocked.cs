
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetLocked : MonoBehaviour {

    // Use this for initialization
    private GameObject target;//射线目标   
    public int locktedindex = -1;

    public int baseNote = NOTES.C3;

    private MIDIPlayer midiPlayer;

    private float timer;

    void Start () {
        timer = 0;
    }

    private void FixedUpdate()
    {
        // Time counter for tips text
        if (this.timer > 0)
        {
            this.timer -= Time.deltaTime;
            if (this.timer <= 0)
            {
                // Stop the note
                this.midiPlayer.setNote(-1);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;

        Ray ray = new Ray(transform.position, forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Enermy"))//明确可以实现凝视功能的物体         
            {
                if (target != null)
                {
                    target.GetComponent<PandaMove>().unlocked();
                }

                if (hit.transform.gameObject != target)
                {
                    target = hit.transform.gameObject;     //射线目标变为凝视目标  
                    
                }
                else
                {
                    Debug.Log("Locked enemy...... index :" + target.GetComponent<PandaMove>().index);
                    this.locktedindex = target.GetComponent<PandaMove>().index;
                    
                    // Play note
                    //this.midiPlayer.setNote(-1);
                    if (this.locktedindex >= 0 && this.timer <= 0)
                    {
                        this.midiPlayer.setNote(target.GetComponent<PandaMove>().index + this.baseNote);
                        this.timer = 1.0f;
                    }

                    // target.GetComponent<PandaMove>().locked();
   
                }
            }
        }
        else
        {
            if (target != null)
            {
                target.GetComponent<PandaMove>().unlocked();
                target = null;
            }
        }
     
    }
    public GameObject GetTarget()
    {
        return target;
    }

}
