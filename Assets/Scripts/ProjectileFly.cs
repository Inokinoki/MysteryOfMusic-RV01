using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFly : MonoBehaviour {

    public int index = -1;

    private float speed = 80.0f;

    public float dieTime = 20.0f;

    private float accumulatedTime;

	// Use this for initialization
	void Start () {
        // ParticleSystem particleSystem = this.GetComponentInChildren<ParticleSystem>();
        Transform particleTransform = this.transform.GetChild(0);
        ParticleSystem particleSystem = particleTransform.GetComponent<ParticleSystem>();
        this.gameObject.SetActive(true);
        particleSystem.gameObject.SetActive(true);
        particleSystem.gameObject.SetActive(true);
        if (particleSystem)
        {
            Debug.Log("Going to Play " + particleSystem.isPlaying);
            particleSystem.Play();
            Debug.Log("Going to Play " + particleSystem.isPlaying + " " + particleSystem.isEmitting);
        }

        this.accumulatedTime = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
	}

    private void FixedUpdate()
    {
        this.accumulatedTime += Time.deltaTime;

        if (this.accumulatedTime > this.dieTime)
        {
            Destroy(this.gameObject);
        }
    }
}
