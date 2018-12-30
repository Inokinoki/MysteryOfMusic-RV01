using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFly : MonoBehaviour {

    public int index = -1;          // index, represent the note

    private float speed = 80.0f;    // Move speed

    public float dieTime = 20.0f;   // Can alive 20s

    private float accumulatedTime;  // Alive time

    private GameObject target;  // Target locked

	// Use this for initialization
	void Start () {
        this.accumulatedTime = 0.0f;    // Init accumelated time
    }
	
	void Update () {
        // Move here
        if (this.target == null)
        {
            // No target locked
            this.transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
        }
        else
        {
            // Target locked, turn to it and go ahead 
            Debug.Log("Target position " + this.target.transform.position);
            this.transform.LookAt(this.target.transform.position);
            this.transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
        }
    }

    public void SetTarget(GameObject _target)
    {
        Debug.Log("Target setted " + target);
        this.target = _target;
    }

    private void FixedUpdate()
    {
        // Accumulate alive time here
        this.accumulatedTime += Time.deltaTime;

        if (this.accumulatedTime > this.dieTime)
        {
            Destroy(this.gameObject);
        }
    }
}
