using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteraction : MonoBehaviour
{

    public int index = NOTES.C3;

    private Color originColor;

    public delegate void OnRaycastClick();

    public OnRaycastClick HandleRayCastClick;

    // Use this for initialization
    void Start()
    {
        originColor = this.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHighlight()
    {
        this.GetComponent<Renderer>().material.color = Color.gray;
    }

    public void OnRemoveHighLight()
    {
        this.GetComponent<Renderer>().material.color = originColor;
    }
}
