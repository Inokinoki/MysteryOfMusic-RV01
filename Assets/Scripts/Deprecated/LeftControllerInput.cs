using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Valve.VR;
using HTC.UnityPlugin.Vive;

public class LeftControllerInput : MonoBehaviour
{
	public GameObject magicBallGenerator;

    public GameObject magicIndicator;
    private bool isHidden;
    // Use this for initialization
    private SteamVR_TrackedObject trackedObj;
    // 2
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        isHidden = true;
    }

    float VectorAngle(Vector2 from, Vector2 to)
    {
        Vector3 cross = Vector3.Cross(from, to);
        float angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }

    // Update is called once per frame
    void Update()
    {
        //recharge magic balls
        if (Controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 cc = Controller.GetAxis();
            float angle = VectorAngle(new Vector2(1, 0), cc);
            if ((angle > 0 && angle < 45) || (angle > -45 && angle < 0) || (angle < 180 && angle > 135) || (angle < -135 && angle > -180))
            {
                Debug.Log("Test");
                if (isHidden)
                {
                    magicBallGenerator.GetComponent<MagicBallGenerator>().ShowMagicBall();
                    isHidden = false;
                }
                else
                {
                    magicBallGenerator.GetComponent<MagicBallGenerator>().HideMagicBall();
                    isHidden = true;
                }
            }
        }

        if (Controller.GetHairTriggerDown())
        {
            Debug.Log("Test");
            this.GetComponent<SteamVR_LaserPointer>().open = true;
        }

        if (Controller.GetHairTriggerUp())
        {
            Debug.Log("Test");
            this.GetComponent<SteamVR_LaserPointer>().open = false;
        }
    }
}
