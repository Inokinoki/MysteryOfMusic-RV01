using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaGenerator : MonoBehaviour {

    public float minGenerateInterval = 2.0f;
    public float maxGenerateInterval = 8.0f;

    private float timer;

    public GameObject panda; // Le prefab Cube
    public GameObject dest;
   
    void Start()
    {
        this.timer = Random.Range(this.minGenerateInterval, this.maxGenerateInterval);
    }


    void Update()
    {
        this.timer -= Time.deltaTime;
        if (this.timer <= 0)
        {
            GameObject pandaInstance = Instantiate(this.panda, this.transform.position, Quaternion.identity);
            pandaInstance.GetComponent<PandaMove>().target = dest;
            pandaInstance.GetComponent<PandaMove>().index = Random.Range(0, 11);
            this.timer = Random.Range(this.minGenerateInterval, this.maxGenerateInterval);
        }
    }

}
