using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    public GameObject shooter;

    public GameKeeper manager;

    public float newFOV = 15.0f;

    public float zoomSpeed = 15.0f;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shooter = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(shooter.transform);

        if (manager.setupPhase)
        {
            GetComponent<Camera>().fieldOfView = 70.0f;
        }

        if(GetComponent<Camera>().fieldOfView >= newFOV)
        {
            GetComponent<Camera>().fieldOfView -= zoomSpeed * Time.deltaTime;
        }
    }
}
