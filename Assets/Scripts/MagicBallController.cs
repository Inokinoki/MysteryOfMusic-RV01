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
        
        /*this.accumulatedTime += Time.deltaTime;
       
        if (this.accumulatedTime > this.switchTime)
        {
            this.accumulatedTime = 0;
            this.mRenderer.material = this.materials[this.index];
            if (this.index == this.materials.Length - 1) this.index = 0;
            else this.index++;
        }*/
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(this.tag))
        {
            int collsionIndex = collision.gameObject.GetComponent<MagicBallController>().index;

            // Destroy(this.gameObject);

            Vector3 new_position = (this.transform.position + collision.gameObject.transform.position) / 2;

            // Destroy(collision.gameObject);

            if (Mathf.Abs(this.index - collsionIndex) == 2)
            {
                Debug.Log("Collision Okay");

                // magicBall.GetComponent<MagicBallController>().index = (this.index + collsionIndex) / 2;
                this.index = (this.index + collsionIndex) / 2;
                collision.gameObject.GetComponent<MagicBallController>().index = (this.index + collsionIndex) / 2;
                Debug.Log("Index: " + ((this.index + collsionIndex) / 2));
            }
            else
            {
                Debug.Log("不可合成");
            }
        }
    }
}
