using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

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

    private PlayerTeleport teleport;  // Reference of Teleport script
    private MIDIPlayer midiPlayer;
    private MagicBallGenerator magicBallGenerator;
    private ProjectileManager projectileManager;

    private GameObject currentCamera;

    public GameObject leftManette;
    public GameObject rightManette;

	// Use this for initialization
	void Start () {
        teleport = GetComponent<PlayerTeleport>();
        midiPlayer = GetComponent<MIDIPlayer>();
        magicBallGenerator = GetComponent<MagicBallGenerator>();

        availableNotes = Configuration.availableNotes;

        // Set available notes
        if (magicBallGenerator != null && availableNotes != null)
        {
            magicBallGenerator.SetAvailableIndex(availableNotes);
        }

        projectileManager = GameObject.FindGameObjectWithTag("Terrain").GetComponent<ProjectileManager>();  // Get Projectile manager

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

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
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
            if (teleport)
            {
                teleport.SwitchPosition();
            }
        }

        // Get raycast object
        GameObject gameObject = GetRaycastedObject(currentCamera);
        if (gameObject != null)
        {
            if (gameObject.CompareTag("Enermy"))
            {
                OnEnermyEnter(gameObject);
            }
            else if (targetLocked != null && targetLocked.CompareTag("Enermy"))
            {
                PandaMove lastPandaMove = targetLocked.GetComponent<PandaMove>();
                if (lastPandaMove != null)
                {
                    lastPandaMove.Unlock();
                }

                targetLocked = null;
            }

            if (Input.GetMouseButtonUp(0) && gameObject.CompareTag("MagicBall"))
            {
                OnBallPlayed(gameObject);
            }

            if (Input.GetMouseButton(1) && gameObject.CompareTag("MagicBall"))
            {
                OnBallGet(gameObject, currentCamera);
            }
        }
        else if (targetLocked != null && targetLocked.CompareTag("Enermy"))
        {
            PandaMove lastPandaMove = targetLocked.GetComponent<PandaMove>();
            if (lastPandaMove != null)
            {
                lastPandaMove.Unlock();
            }

            targetLocked = null;
        }

        // Launch projectile
        if (Input.GetMouseButtonUp(0))
        {
            LaunchProjectile(currentCamera);
        }

        // Middle Click, show balls
        if (Input.GetMouseButtonUp(2))
        {
            if (magicBallGenerator)
            {
                if (isBallShowed)
                {
                    magicBallGenerator.HideMagicBall();
                    isBallShowed = false;
                }
                else
                {
                    magicBallGenerator.ShowMagicBall();
                    isBallShowed = true;
                }
            }
        }
    }

    /*
     * VR Control listener
     */
    private void VRControl()
    {
        // Teleport - Right Hand Grip pressed
        if (ViveInput.GetPressUp(HandRole.RightHand, ControllerButton.Grip))
        {
            if (teleport)
            {
                teleport.SwitchPosition();
            }
        }

        // Get raycast object of MainCamera
        GameObject gameObject = GetRaycastedObject(currentCamera);

        if (gameObject != null)
        {
            if (gameObject.CompareTag("Enermy"))
            {
                OnEnermyEnter(gameObject);
            }
            else if (targetLocked != null && targetLocked.CompareTag("Enermy"))
            {
                PandaMove lastPandaMove = targetLocked.GetComponent<PandaMove>();
                if (lastPandaMove != null)
                {
                    lastPandaMove.Unlock();
                }

                targetLocked = null;
            }
        }
        else if (targetLocked != null && targetLocked.CompareTag("Enermy"))
        {
            PandaMove lastPandaMove = targetLocked.GetComponent<PandaMove>();
            if (lastPandaMove != null)
            {
                lastPandaMove.Unlock();
            }

            targetLocked = null;
        }

        // Left hand play note of ball
        gameObject = GetRaycastedObject(leftManette);
        if (gameObject != null)
        {
            Animator leftAnimator = leftManette.GetComponentInChildren<Animator>();

            if (leftAnimator != null)
            {
                // Lighten magic effects
                // We can also use setActive of sub components, but this is for practicing animator
                leftAnimator.SetBool("IsHalt", false);
                leftAnimator.SetBool("IsAttracting", true);
            }

            if (ViveInput.GetPadPressVector(HandRole.LeftHand) != Vector2.zero && gameObject.CompareTag("MagicBall"))
            {
                OnBallPlayed(gameObject);

                ViveInput.TriggerHapticPulse(HandRole.LeftHand, 500);
            }

            if (ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Trigger) && gameObject.CompareTag("MagicBall"))
            {
                if (OnBallGet(gameObject, leftManette) != null)
                {
                    Animator rightAnimator = rightManette.GetComponentInChildren<Animator>();
                    if (rightAnimator != null)
                    {
                        // Light magic effects
                        rightAnimator.SetBool("IsLaunched", false);
                        rightAnimator.SetBool("IsCharged", true);
                    }
                }
            }
        }
        else
        {
            // Need test
            Animator leftAnimator = leftManette.GetComponentInChildren<Animator>();
            if (leftAnimator != null)
            {
                // Close magic effects
                leftAnimator.SetBool("IsHalt", true);
                leftAnimator.SetBool("IsAttracting", false);
            }
        }

        // Launch projectile
        if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Trigger))
        {
            LaunchProjectile(rightManette);

            Animator rightAnimator = rightManette.GetComponentInChildren<Animator>();
            if (rightAnimator != null)
            {
                // Close magic effects
                rightAnimator.SetBool("IsLaunched", true);
                rightAnimator.SetBool("IsCharged", false);
            }

            ViveInput.TriggerHapticPulse(HandRole.RightHand, 500);
        }

        // Right touch pad, play current note
        if (ViveInput.GetPadPressVector(HandRole.RightHand) != Vector2.zero)
        {
            if (chargedIndex != -1)
            {
                if (midiPlayer)
                {
                    midiPlayer.AddNote(new SingleNote(baseNote + chargedIndex, 0.5f));
                }
                ViveInput.TriggerHapticPulse(HandRole.RightHand, 500);
            }
        }

        // Left grib, show balls
        if (ViveInput.GetPressUp(HandRole.LeftHand, ControllerButton.Grip))
        {
            if (magicBallGenerator)
            {
                if (isBallShowed)
                {
                    magicBallGenerator.HideMagicBall();
                    isBallShowed = false;
                }
                else
                {
                    magicBallGenerator.ShowMagicBall();
                    isBallShowed = true;
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

    /*
     * Handle entered enermy
     */
    private PandaMove OnEnermyEnter(GameObject gameObject)
    {
        PandaMove pandaMove = null;
        // Lock target if target was null or target changed
        if (targetLocked == null || targetLocked != gameObject)
        {
            if (targetLocked != null)
            {
                // Unlock
                PandaMove lastPandaMove = targetLocked.GetComponent<PandaMove>();
                if (lastPandaMove != null)
                {
                    lastPandaMove.Unlock();
                }
            }

            targetLocked = gameObject;
            pandaMove = gameObject.GetComponent<PandaMove>();
            if (pandaMove != null)
            {
                if (midiPlayer)
                {
                    midiPlayer.AddNote(new SingleNote(baseNote + pandaMove.index, 0.5f));
                }

                pandaMove.Lock();
            }
        }

        return pandaMove;
    }

    /*
     * Play note of entered ball
     */
    private MagicBallController OnBallPlayed(GameObject gameObject)
    {
        MagicBallController magicBallController = null;
        // Lock target if target was null or target changed
        if (gameObject != null)
        {
            magicBallController = gameObject.GetComponent<MagicBallController>();
            if (magicBallController != null)
            {
                // If it's not the lastest played note, play for 0.5s
                if (targetBall == null || targetBall != gameObject)
                {
                    if (midiPlayer)
                    {
                        midiPlayer.AddNote(new SingleNote(baseNote + magicBallController.index, 0.5f));
                        Invoke("ClearTargetBall", 0.5f);    // After 500ms clear target ball
                    }

                    targetBall = gameObject;
                }
            }
        }

        return magicBallController;
    }

    /*
     * Play note of entered ball
     */
    private MagicBallController OnBallGet(GameObject gameObject, GameObject origin)
    {
        MagicBallController magicBallController = null;

        if (gameObject)
        {
            magicBallController = gameObject.GetComponent<MagicBallController>();

            if (origin != null)
            {
                // Move to player
                gameObject.transform.Translate((origin.transform.position - gameObject.transform.position) * Time.deltaTime * 2.0f, Space.World);

                // Charge with the ball
                if ((origin.transform.position - gameObject.transform.position).magnitude < 0.8f)
                {
                    if (chargedIndex >= 0 && (Mathf.Abs(chargedIndex - magicBallController.index) == 2))
                    {
                        // If combinaison is possible
                        chargedIndex = (chargedIndex + magicBallController.index) / 2;
                    }
                    else
                    {
                        // Overwrite charged state
                        chargedIndex = magicBallController.index;
                    }
                    
                    Destroy(gameObject);
                }
            }
        }

        /*if (magicBallGenerator.GetComponent<MagicBallGenerator>().HasMagicBall(hit.transform.gameObject))
        {
            magicBallGenerator.GetComponent<MagicBallGenerator>().RetrieveMagicBall(hit.transform.gameObject);
            //magicBallController.SetRotationArround(this.transform.position);
        }*/

        return magicBallController;
    }

    private void LaunchProjectile(GameObject origin)
    {
        if (origin == null)
        {
            origin = this.gameObject;   // Set default to this.gameObject
        }

        Debug.Log("Charged with " + chargedIndex + " launching");

        if (chargedIndex >= 0)
        {
            if (projectileManager != null)
            {
                if (projectileManager.projectiles.Length > chargedIndex)
                {
                    GameObject projectile = Instantiate(projectileManager.projectiles[chargedIndex],
                        origin.transform.position + origin.transform.TransformDirection(Vector3.forward),
                        origin.transform.rotation);
                    projectile.GetComponent<ProjectileFly>().index = chargedIndex;
                    projectile.GetComponent<ProjectileFly>().SetTarget(targetLocked);

                    // Set magic charge to non charge
                    this.chargedIndex = -1;
                }
            }
        }
    }

    public void ClearTargetBall()
    {
        targetBall = null;
    }

    // Dirty properties
    private GameObject targetLocked;
    private GameObject targetBall;

    private List<int> availableNotes;

    private int chargedIndex = -1;

    private bool isBallShowed = false;

}
