using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightManette : MonoBehaviour {

    private MagicController magicController;

	// Use this for initialization
	void Start ()
    {
        this.magicController = this.GetComponentInParent<MagicController>();
    }
    
    // Update is called once per frame
    void Update ()
    {
		
	}
}
