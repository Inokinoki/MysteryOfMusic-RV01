using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerController : MonoBehaviour {

    public bool isVR = true;   // Is VR mode flag

    private int baseNote = NOTES.C3;
    public int BaseNote
    {
        get { return baseNote; }
        set
        {
            baseNote = value;
            // Reset all notes
        }
    }
    
    private MIDIPlayer midiPlayer;

    private GameObject currentCamera;

    public GameObject leftManette;
    public GameObject rightManette;

	// Use this for initialization
	void Start () {
        midiPlayer = GetComponent<MIDIPlayer>();
        
        currentCamera = GetCameraObject();
	}
	
	// Update is called once per frame
	void Update () {
		if (isVR)
        {
            VRControl();
        }
        else
        {
            Control();
        }
	}

    /*
     * Keyboard/Mouse Control detector
     */
    private void Control()
    {
        // Teleport
        if (Input.GetKeyUp(KeyCode.T))
        {
        }
        
        RaycastHit hit;

        if (Physics.Raycast(currentCamera.transform.position, currentCamera.transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.transform.CompareTag("MenuInteraction"))
            {
                if (lastHighlight != null && lastHighlight != hit.transform.gameObject)
                {
                    lastHighlight.GetComponent<MenuInteraction>().OnRemoveHighLight();
                }

                hit.transform.GetComponent<MenuInteraction>().OnHighlight();
                lastHighlight = hit.transform.gameObject;

                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.transform.GetComponent<MenuInteraction>().HandleRayCastClick != null)
                    {
                        hit.transform.GetComponent<MenuInteraction>().HandleRayCastClick();
                    }
                    midiPlayer.AddNote(
                        new SingleNote(hit.transform.GetComponent<MenuInteraction>().index, 0.5f));
                }
            }
            else
            {
                if (lastHighlight != null)
                {
                    lastHighlight.GetComponent<MenuInteraction>().OnRemoveHighLight();
                    lastHighlight = null;
                }
            }
        }
        
    }

    /*
     * VR Control listener
     */
    private void VRControl()
    {

        GameObject leftPointed = GetRaycastedObject(leftManette);

        if (leftPointed)
        {
            if (leftPointed.CompareTag("MenuInteraction"))
            {
                if (lastLeftHighlight != null && lastLeftHighlight != leftPointed)
                {
                    lastLeftHighlight.GetComponent<MenuInteraction>().OnRemoveHighLight();
                }

                leftPointed.GetComponent<MenuInteraction>().OnHighlight();
                lastLeftHighlight = leftPointed;

                if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Trigger))
                {
                    if (leftPointed.GetComponent<MenuInteraction>().HandleRayCastClick != null)
                    {
                        leftPointed.GetComponent<MenuInteraction>().HandleRayCastClick();
                    }
                    midiPlayer.AddNote(
                        new SingleNote(leftPointed.GetComponent<MenuInteraction>().index, 0.5f));

                    ViveInput.TriggerHapticPulse(HandRole.LeftHand, 500);
                }
            }
            else
            {
                if (lastLeftHighlight != null)
                {
                    lastLeftHighlight.GetComponent<MenuInteraction>().OnRemoveHighLight();
                    lastLeftHighlight = null;
                }
            }
        }

        GameObject rightPointed = GetRaycastedObject(rightManette);

        if (rightPointed)
        {
            if (rightPointed.CompareTag("MenuInteraction"))
            {
                if (lastRightHighlight != null && lastRightHighlight != rightPointed)
                {
                    lastRightHighlight.GetComponent<MenuInteraction>().OnRemoveHighLight();
                }

                rightPointed.GetComponent<MenuInteraction>().OnHighlight();
                lastRightHighlight = rightPointed;

                if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Trigger))
                {
                    if (rightPointed.GetComponent<MenuInteraction>().HandleRayCastClick != null)
                    {
                        rightPointed.GetComponent<MenuInteraction>().HandleRayCastClick();
                    }
                    midiPlayer.AddNote(
                        new SingleNote(rightPointed.GetComponent<MenuInteraction>().index, 0.5f));

                    ViveInput.TriggerHapticPulse(HandRole.RightHand, 500);
                }
            }
            else
            {
                if (lastRightHighlight != null)
                {
                    lastRightHighlight.GetComponent<MenuInteraction>().OnRemoveHighLight();
                    lastRightHighlight = null;
                }
            }
        }
    }

    /*
     * Get camera object
     */
    private GameObject GetCameraObject()
    {
        if (isVR)
        {
            return GameObject.FindGameObjectWithTag("MainCamera");
        }
        else
        {
            return GameObject.FindGameObjectWithTag("MainCameraNoVR");
        }
    }

    /*
     * Get the object at which raycast arrive
     */
    private GameObject GetRaycastedObject(GameObject origin)
    {
        if (origin != null)
        {
            // Debug Raycast in Scene
            Debug.DrawRay(origin.transform.position, origin.transform.TransformDirection(Vector3.forward) * 10, Color.red);

            RaycastHit hit;

            if (Physics.Raycast(origin.transform.position, origin.transform.TransformDirection(Vector3.forward), out hit))
            {
                return hit.transform.gameObject;
            }
        }

        return null;
    }

    private GameObject lastHighlight;
    private GameObject lastLeftHighlight;
    private GameObject lastRightHighlight;
}
