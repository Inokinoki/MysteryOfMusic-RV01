using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;



public class RightControllerInput : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    public GameObject[] projectiles;
    public GameObject magicIndicator;
    public GameObject tipsManager;
    public GameObject magicBallGenerator;
  
    private int index = -1;

    public int baseNote = NOTES.C3;

    private MIDIPlayer midiPlayer;

    private float timer = 0;
    // 2
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void FixedUpdate()
    {
        // Time counter for tips text
        if (this.timer > 0)
        {
            this.timer -= Time.deltaTime;
            if (this.timer <= 0)
            {
                // Stop the note
                this.midiPlayer.setNote(-1);
            }
        }
    }

    void Start()
    {
        this.timer = 0;
        this.midiPlayer = GameObject.FindGameObjectWithTag("MIDIPlayer").GetComponent<MIDIPlayer>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;
        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }

    public void SetIndex(int _index){
		index = _index;
	}
    // Update is called once per frame
    void Update () {

        if (Controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 cc = Controller.GetAxis();
            float jiaodu = VectorAngle(new Vector2(1, 0), cc);
            if ((jiaodu > 0 && jiaodu < 45) || (jiaodu > -45 && jiaodu < 0) || (jiaodu < 180 && jiaodu > 135) || (jiaodu < -135 && jiaodu > -180))
            {
                if (this.index >= 0 && this.timer <= 0)
                {
                    this.midiPlayer.setNote(this.index + this.baseNote);
                    this.timer = 1.0f;
                }
            }
        }

        if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.Grip))
        {

            // Get the ball
            RaycastHit hit;

            if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.transform.gameObject.CompareTag("MagicBall"))
                {
                    MagicBallController magicBallController = hit.transform.gameObject.GetComponent<MagicBallController>();
                    Debug.Log("hit magic ball: " + hit.transform.gameObject.name);
                    //magicBallController.SetRotationArround(new Vector3(0, 0, 0));

                    if (magicBallGenerator.GetComponent<MagicBallGenerator>().HasMagicBall(hit.transform.gameObject))
                    {
                        magicBallGenerator.GetComponent<MagicBallGenerator>().RetrieveMagicBall(hit.transform.gameObject);
                        //magicBallController.SetRotationArround(this.transform.position);
                    }

                    hit.transform.Translate((this.transform.position - hit.transform.position) * Time.deltaTime * 2.0f, Space.World);

                    // Charge with the ball
                    if ((this.transform.position - hit.transform.position).magnitude < 0.8f)
                    {
                        if (this.index >= 0 && (Mathf.Abs(this.index - magicBallController.index) == 2))
                        {
                            // NOT TO-DO: condition -> C and B... B and C have no interval...
                            // If composition is possible
                            this.index = (this.index + magicBallController.index) / 2;
                        }
                        else
                            this.index = magicBallController.index;

                        // Set Magic Indicator
                        if (this.magicIndicator)
                        {
                            this.magicIndicator.GetComponent<Image>().color =
                                magicBallController.materials[this.index].color;
                        }   
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }

        if (Controller.GetHairTriggerDown())
        {
                if (this.index >= 0)
                {
					Controller.TriggerHapticPulse(2500);
                    if (this.projectiles.Length > this.index)
                    {
                        GameObject projectile = Instantiate(this.projectiles[this.index], this.transform.position - new Vector3(0, 0.5f, 0), this.transform.rotation);
                        projectile.GetComponent<ProjectileFly>().index = this.index;
                         projectile.GetComponent<ProjectileFly>().SetTarget(
                             GameObject.FindGameObjectWithTag("MainCamera").
                             GetComponent<TargetLocked>().GetTarget());

                        // Set magic charge to non charge
                    this.index = -1;

                        if (this.magicIndicator)
                        {
                            this.magicIndicator.GetComponent<Image>().color = Color.white;
                        }
                    }
                }
                else
                {
                    Debug.Log("Not charged");
                    if (this.tipsManager)
                    {
                        this.timer = 3.0f;
                        this.tipsManager.GetComponent<Text>().text = "You are not charged";
                    }
                }

            Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * 8.0f, Color.red);
            Debug.Log(gameObject.name + " Trigger Press");
        }


        RaycastHit detect_hit;

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
        }



    }
}
