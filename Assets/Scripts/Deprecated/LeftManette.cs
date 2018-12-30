using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftManette : MonoBehaviour {

    private float timer = 0;

    private MagicController magicController;

    private GameObject tipsManager;

    // Use this for initialization
    void Start () {
        this.magicController = this.GetComponentInParent<MagicController>();

        this.tipsManager = this.magicController.tipsManager;
    }

    void AcquireMagicBall(object sender)
    {
        Debug.Log("Left trigger");
        GameObject eyeManager = GameObject.FindGameObjectWithTag("MainCamera");
        if (eyeManager != null)
        {
            GameObject magicBall = eyeManager.GetComponent<EyeManager>().GetTargetMagicBall();

            if (magicBall != null)
            {
                this.magicController.RemoveMagicBallInPool(magicBall);

                magicBall.transform.Translate((this.transform.position - magicBall.transform.position) * Time.deltaTime * 2.0f, Space.World);

                // Charge with the ball
                if ((this.transform.position - magicBall.transform.position).magnitude < 1.5f)
                {
                    this.magicController.ChargeWithMagicBall(magicBall);
                }
            }
        }
    }

    void PlayCurrentNote(object sender)
    {
        this.magicController.PlayCurrentNote();
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

    // Update is called once per frame
    void Update () {
        Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * 80.0f, Color.red);

        /*RaycastHit detect_hit;

        if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out detect_hit))
        {
            if (detect_hit.transform.gameObject.CompareTag("Enermy"))
            {
                int _index = detect_hit.transform.gameObject.GetComponent<PandaMove>().index;
                if (this.tipsManager)
                {
                    this.timer = 0.5f;
                    if (_index != -1)
                    {
                        this.tipsManager.GetComponent<Text>().text = "Target Panda " +
                            GameObject.FindGameObjectWithTag("MagicBall").GetComponent<MagicBallController>().notes[_index];
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
        }*/
    }
}
