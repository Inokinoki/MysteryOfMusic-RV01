using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallGenerator : MonoBehaviour {

    public GameObject magicBallPrefab;

    private List<GameObject> magicBalls;

    private Stack<Vector3> lastPositions;

    private Stack<int> lastIndexs;

    public int[] indexAvailable;

    private int maxLength = 12;

    public float ud_dis = 2.0f;

    public float lg_dis = 2.0f;

    public float eye_dis = 5.0f;

    public float height = 4.0f;

    //public float deltaHeight = 1.0f;

    public bool RetrieveMagicBall(GameObject ball)
    {

        // Retrieve Magic Ball from array
        if (this.magicBalls.Contains(ball))
        {
            this.lastPositions.Push(ball.transform.position);
            int index = ball.GetComponent<MagicBallController>().index;
            this.lastIndexs.Push(index);
            this.magicBalls.Remove(ball);

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

    public void HideMagicBall()
    {
        for (int i = 0; i < this.magicBalls.Count; i++)
        {
            GameObject magicBall = this.magicBalls[i];
            Destroy(magicBall);
        }

        this.magicBalls.Clear();
    }

    public void ShowMagicBall()
    {
        for (int i = 0; i < this.indexAvailable.Length; i++)
        {
            GameObject magicBall = Instantiate(magicBallPrefab, 
                this.transform.position + 
                this.transform.TransformDirection(
                    Quaternion.AngleAxis(20 * (i - this.indexAvailable.Length / 2), Vector3.up) * Vector3.forward
                    ) * 5.0f + Vector3.up * this.height, 
                Quaternion.identity);
            magicBall.GetComponent<MagicBallController>().index = this.indexAvailable[i];
            this.magicBalls.Add(magicBall);
        }
    }


    // Use this for initialization
    void Start()
    {
        this.magicBalls = new List<GameObject>(this.maxLength);
        this.lastPositions = new Stack<Vector3>(this.maxLength);
        this.lastIndexs = new Stack<int>(this.maxLength);
    }
    
    public void SetAvailableIndex(List<int> list)
    {
        indexAvailable = new int[list.Count];
        list.CopyTo(indexAvailable);
    }

}
