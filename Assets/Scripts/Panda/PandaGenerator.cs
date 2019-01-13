using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaGenerator : MonoBehaviour {
    
    private float minGenerateInterval = 2.0f;
    private float maxGenerateInterval = 8.0f;

    private int minIndex = 0;
    private int maxIndex = 11;

    private float timer;

    public GameObject panda; // Le prefab Panda
    public GameObject destination;
    public GameObject departure;

    void Start()
    {
        // Get configurations
        minGenerateInterval = Configuration.enermyGenerateMinInterval;
        maxGenerateInterval = Configuration.enermyGenerateMaxInterval;

        Debug.Log(Configuration.availablePanda.Count);

        minIndex = 0;
        ProjectileManager manager = GameObject.FindGameObjectWithTag("Terrain").GetComponent<ProjectileManager>();
        if (manager != null)
        {
            maxIndex = manager.projectiles.Length - 1;
        }

        timer = Random.Range(minGenerateInterval, maxGenerateInterval);
    }

    void Update()
    {
        // Timer count
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameObject pandaInstance = Instantiate(panda, departure.transform.position, Quaternion.identity);

            PandaMove pandaMove = pandaInstance.GetComponent<PandaMove>();

            if (pandaMove != null)
            {
                pandaInstance.GetComponent<PandaMove>().target = destination;
                pandaInstance.GetComponent<PandaMove>().index =  Configuration.availablePanda[Random.Range(0, Configuration.availablePanda.Count)];
            }
            timer = Random.Range(minGenerateInterval, maxGenerateInterval);
        }
    }

}
