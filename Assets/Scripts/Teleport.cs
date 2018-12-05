using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public GameObject point_a;
    public GameObject point_b;

    private GameObject point;

	// Use this for initialization
	void Start () {
        if (this.point_b) {
            this.point = this.point_b;
            this.transform.position = this.point.transform.position;
            this.transform.rotation = this.point.transform.rotation;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp("tab")) {
            // Change position of character
            if (this.point == this.point_a)
            {
                this.point = this.point_b;
                this.transform.position = this.point.transform.position;
                this.transform.rotation = this.point.transform.rotation;

                this.GetComponent<MagicBallGenerator>().Relocation();
            }
            else if (this.point == this.point_b)
            {
                this.point = this.point_a;
                this.transform.position = this.point.transform.position;
                this.transform.rotation = this.point.transform.rotation;

                this.GetComponent<MagicBallGenerator>().Relocation();
            }
        }
    }
}
