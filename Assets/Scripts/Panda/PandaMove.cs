using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PandaMove : MonoBehaviour {

    private NavMeshAgent navAgent;  // L'objet qui va naviguer

    public GameObject target;

    public int index = -1;


    //private Vector3 target; // La cible vers laquelle l'objet se déplace
    // Use this for initialization
    void Start () {

        MeshRenderer renderer = this.GetComponentInChildren<MeshRenderer>();
        renderer.enabled = false;
        navAgent = GetComponent<NavMeshAgent>();
      
    }

    // Update is called once per frame
    void Update () {
        if (this.target)
        {
            navAgent.SetDestination(this.target.transform.position);
            
            if (distance(transform.position, this.target.transform.position) < 5.0f)
            {
                // Reduce the state of status
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        /*
         * Called when the effect fall onto the terrain 
         */
        if (other.CompareTag("MagicEffect"))
        {
            if (this.index == other.transform.parent.gameObject.GetComponent<ProjectileFly>().index)
            {
                // Destroy the panda
                Destroy(this.gameObject);
            }
            
            Debug.Log("Magic Effect " + other.transform.parent.gameObject.GetComponent<ProjectileFly>().index + " onto Panda " + this.index);

            // Destroy the magic effect
            Destroy(other.transform.parent.gameObject);
        }
    }

    private float distance(Vector3 v1, Vector3 v2)
    {
        return Mathf.Sqrt(Mathf.Pow((v1.x - v2.x), 2) + Mathf.Pow((v1.y - v2.y), 2) + Mathf.Pow((v1.z - v2.z), 2));
    }

    public void Lock()
    {
         MeshRenderer renderer = this.GetComponentInChildren<MeshRenderer>();
         renderer.enabled = true;
    }

    public void Unlock()
    {
        MeshRenderer renderer = this.GetComponentInChildren<MeshRenderer>();
        renderer.enabled = false;
    }

    public void unlocked ()
    {
        MeshRenderer renderer = this.GetComponentInChildren<MeshRenderer>();
        renderer.enabled = false;
    }

}
