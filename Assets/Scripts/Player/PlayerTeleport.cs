using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour {

    public GameObject point_a;
    public GameObject point_b;

    private GameObject point;

	// Use this for initialization
	void Start ()
    {
        // Init a point to located
        if (this.point_b) {
            this.point = this.point_b;
            this.transform.position = this.point.transform.position;
            this.transform.rotation = this.point.transform.rotation;
        }
        else if (this.point_a)
        {
            this.point = this.point_a;
            this.transform.position = this.point.transform.position;
            this.transform.rotation = this.point.transform.rotation;
        }
    }

    public void SwitchPosition()
    {
        // Change position of character
        if (this.point == this.point_a && this.point_b != null)
        {
            this.point = this.point_b;
            this.transform.position = this.point.transform.position;
            this.transform.rotation = this.point.transform.rotation;

            this.transform.position = new Vector3(this.point.transform.position.x, this.point.transform.position.y + 4.0f, this.point.transform.position.z);
            this.transform.rotation = this.point.transform.rotation;
        }
        else if (this.point == this.point_b && this.point_a != null)
        {
            this.point = this.point_a;
            this.transform.position = this.point.transform.position;
            this.transform.rotation = this.point.transform.rotation;

            this.transform.position = new Vector3(this.point.transform.position.x, this.point.transform.position.y + 4.0f, this.point.transform.position.z);
            this.transform.rotation = this.point.transform.rotation;
        }
        else
        {
            Debug.LogWarning("No location set, cannot teleport");
        }
    }
}
