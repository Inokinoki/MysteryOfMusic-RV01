using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallGenerator : MonoBehaviour {

    public GameObject magicBallPrefab;

    private List<GameObject> magicBalls;

    private Stack<Vector3> lastPositions;

    public int[] indexAvailable;

    public int maxLength = 16;

    public float radius = 5.0f;

    public float height = 6.0f;

    public float deltaHeight = 1.0f;

    private float timer = 0;

    public bool RetrieveMagicBall(GameObject ball)
    {
        // Retrieve Magic Ball from array
        if (this.magicBalls.Contains(ball))
        {
            this.lastPositions.Push(ball.transform.position);
            this.magicBalls.Remove(ball);

            // Turn up the ball circle to avoid the selection
            this.height += this.deltaHeight;
            this.Relocation();
            this.timer = 2.0f;

            return true;
        }
        return false;
    }

    public bool HasMagicBall(GameObject ball)
    {
        // Retrieve Magic Ball from array
        if (this.magicBalls.Contains(ball))
        {
            return true;
        }
        return false;
    }

    public void Relocation()
    {
        int count = 0;
        for (List<GameObject>.Enumerator e = this.magicBalls.GetEnumerator(); e.MoveNext();)
        {
            e.Current.transform.position = new Vector3(
                    this.transform.position.x + this.radius * Mathf.Cos(Mathf.PI * 2.0f / this.maxLength * count),
                    this.transform.position.y + this.height,
                    this.transform.position.z + this.radius * Mathf.Sin(Mathf.PI * 2.0f / this.maxLength * count)
            );
            count++;
        }
    }

    // Use this for initialization
    void Start () {
        this.magicBalls = new List<GameObject>(this.maxLength);
        this.lastPositions = new Stack<Vector3>(this.maxLength);

        for (int i = 0; i < this.maxLength; i++)
        {
            GameObject magicBall = Instantiate(magicBallPrefab, new Vector3(
                    this.transform.position.x + this.radius * Mathf.Cos(Mathf.PI * 2.0f / this.maxLength * i),
                    this.transform.position.y + this.height,
                    this.transform.position.z + this.radius * Mathf.Sin(Mathf.PI * 2.0f / this.maxLength * i)
                ), Quaternion.identity);
            magicBall.GetComponent<MagicBallController>().index = this.indexAvailable[Random.Range(0, this.indexAvailable.Length)];
            this.magicBalls.Add(magicBall);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        bool isMoving = false;

        if (this.timer > 0)
        {
            isMoving = true;
            this.timer -= Time.deltaTime;
            if (this.timer <= 0)
            {
                // Recovery the height
                this.height -= this.deltaHeight;
                this.Relocation();
            }
        }

        while(this.magicBalls.Count < this.maxLength)
        {
            // To garantee the generation of magic ball is at the same height of the others
            Vector3 lastPosition = this.lastPositions.Pop();
            lastPosition.y = this.transform.position.y + this.height;

            GameObject magicBall = Instantiate(magicBallPrefab, lastPosition, Quaternion.identity);
            magicBall.GetComponent<MagicBallController>().index = this.indexAvailable[Random.Range(0, this.indexAvailable.Length)];
            this.magicBalls.Add(magicBall);
        }

		for (List<GameObject>.Enumerator e = this.magicBalls.GetEnumerator(); e.MoveNext();)
        {
            e.Current.transform.RotateAround(this.transform.position, Vector3.up, 0.5f);
        }
	}
}
