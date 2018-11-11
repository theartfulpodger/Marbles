using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // This is the movement and restriction script for the placement phase only

    public float placeMovementSpeedBase = 3f;
    public float placeMovementSpeed;

    public Transform centre;
    Vector3 circleCentre;
    public float radius = 24.5f;

    // Start is called before the first frame update
    void Start()
    {
        centre = GameObject.Find("GameManager").transform;
        circleCentre = centre.position;
        placeMovementSpeed = placeMovementSpeedBase;
    }

    // Update is called once per frame
    void Update()
    {
        PlaceMovement();
    }

    public void PlaceMovement()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * placeMovementSpeed);


        //restrict to circle https://answers.unity.com/questions/809266/restrict-player-movement-to-circles-circumference.html?childToView=809624
        Vector3 offset = transform.position - circleCentre;
        offset = offset.normalized * radius;
        transform.position = offset;

        //look at centre https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
        transform.LookAt(centre);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            placeMovementSpeed = placeMovementSpeedBase * 2;
        }else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            placeMovementSpeed = placeMovementSpeedBase;
        }
    }
}
