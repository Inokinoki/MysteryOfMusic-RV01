using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicController : MonoBehaviour {

    public GameObject[] projectiles;

    public GameObject magicIndicator;
    public GameObject scoreCounter;
    public GameObject tipsManager;
    public GameObject noteManager;

    private GameObject lastFocused;

    private int index = -1;

    private float timer = 0;

    private int score = 0;

    // Use this for initialization
    void Start () {
        if (this.magicIndicator)
        {
            this.magicIndicator.GetComponent<Image>().color = Color.white;
        }

        if (this.noteManager)
        {
            this.noteManager.GetComponent<Text>().text = "None";
        }
    }

    public void AddScore(int _score)
    {
        this.score += _score;
        if (this.scoreCounter)
        {
            this.scoreCounter.GetComponent<Text>().text = "Score : " + this.score;
        }
    }

    public void AddScore()
    {
        this.AddScore(1);
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
        if (Input.GetMouseButtonUp(0))
        {
            if (this.index >= 0)
            {
                if (this.projectiles.Length > this.index)
                {
                    GameObject projectile = Instantiate(this.projectiles[this.index], this.transform.position - new Vector3(0, 0.5f, 0), this.transform.rotation);
                    projectile.GetComponent<ProjectileFly>().index = this.index;

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
        }

        Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * 8.0f, Color.red);

        if (Input.GetMouseButton(1))
        {
            // Get the ball
            RaycastHit hit;

            if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.transform.gameObject.CompareTag("MagicBall"))
                {
                    MagicBallController magicBallController = hit.transform.gameObject.GetComponent<MagicBallController>();

                    magicBallController.SetRotationArround(new Vector3(0, 0, 0));

                    if (this.GetComponentInParent<MagicBallGenerator>().HasMagicBall(hit.transform.gameObject))
                    {
                        this.GetComponentInParent<MagicBallGenerator>().RetrieveMagicBall(hit.transform.gameObject);
                        magicBallController.SetRotationArround(this.transform.position);
                    }

                    hit.transform.Translate((this.transform.position - hit.transform.position) * Time.deltaTime * 2.0f, Space.World);

                    // Charge with the ball
                    if ((this.transform.position - hit.transform.position).magnitude < 1.5f)
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

                        if (this.noteManager)
                        {
                            this.noteManager.GetComponent<Text>().text =
                                magicBallController.notes[this.index];
                        }

                        Destroy(hit.transform.gameObject);
                    }
                }
            }
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
