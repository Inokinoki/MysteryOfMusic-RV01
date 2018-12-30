using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallController : MonoBehaviour {

    public Material[] materials;

    public string[] notes;

    public float switchTime = 2.0f;

    private float accumulatedTime;

    public int index;

    private int previousIndex;

    private Renderer mRenderer;

    private Vector3 rotationArround;

    public void SetRotationArround(Vector3 t)
    {
        this.rotationArround = t;
    }

	// Use this for initialization
	void Start ()
    {
        accumulatedTime = 0;
        this.mRenderer = this.GetComponent<Renderer>();

        this.previousIndex = -1;

        Debug.Log("GameObject " + this.index + " OnStart");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (this.previousIndex != this.index)
        {
            this.mRenderer.material = this.materials[this.index];
            this.previousIndex = this.index;
        }
    }
}
