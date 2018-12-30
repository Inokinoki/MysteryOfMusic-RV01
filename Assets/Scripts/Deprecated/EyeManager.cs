using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeManager : MonoBehaviour {

    private float timer = 0;

    public GameObject tipsManager;

    private GameObject targetedObject;

    // Use this for initialization
    void Start () {
		
	}

    private void FixedUpdate()
    {
        // Time counter for tips text
        if (this.timer > 0)
        {
            this.timer -= Time.deltaTime;
            if (this.timer < 0)
            {
                if (this.tipsManager)
                {
                    this.tipsManager.GetComponent<Text>().text = "";
                }
            }
        }
    }

    public GameObject GetTargetEnermy()
    {
        if (this.targetedObject)
        {
            if (this.targetedObject.CompareTag("Enermy"))
            {
                return this.targetedObject;
            }
        }
        return null;
    }

    public GameObject GetTargetMagicBall()
    {
        RaycastHit detect_hit;

        if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out detect_hit))
        {
            if (detect_hit.transform.gameObject.CompareTag("MagicBall"))
            {
                return detect_hit.transform.gameObject;
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update () {
        Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * 80.0f, Color.blue);

        RaycastHit detect_hit;

        if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out detect_hit))
        {
            if (detect_hit.transform.gameObject.CompareTag("Enermy"))
            {
                int _index = detect_hit.transform.gameObject.GetComponent<PandaMove>().index;

                this.targetedObject = detect_hit.transform.gameObject;

                if (this.tipsManager)
                {
                    this.timer = 0.5f;
                    if (_index != -1)
                    {
                        this.tipsManager.GetComponent<Text>().text = "Target Panda " +
                            GameObject.FindGameObjectWithTag("MagicBall").GetComponent<MagicBallController>().notes[_index];

                        GameObject.FindGameObjectWithTag("MagicController").GetComponent<MagicController>().PlayAnotherNote(_index);
                    }
                }
            }
            else if (detect_hit.transform.gameObject.CompareTag("MagicBall"))
            {
                int _index = detect_hit.transform.gameObject.GetComponent<MagicBallController>().index;
                if (this.tipsManager)
                {
                    this.timer = 0.5f;
                    if (_index != -1)
                    {
                        this.tipsManager.GetComponent<Text>().text = "Target Magic Ball " +
                            GameObject.FindGameObjectWithTag("MagicBall").GetComponent<MagicBallController>().notes[_index];
                    }
                }
            }
        }
        else
        {
            this.targetedObject = null;
        }
    }
}
