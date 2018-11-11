using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float cameraHeight = 20.0f;
    public float cameraDistance = 20.0f;

    public GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.z += cameraHeight;
        transform.position = pos;
    }
}
