using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDaemon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        /*
         * Called when the effect fall onto the terrain 
         */
        if (other.CompareTag("MagicEffect"))
        {
            Destroy(other.transform.parent.gameObject);
            Debug.Log("Magic Effect onto terrain");
        }
    }
}
