using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{

    public Vector3 arenaCentre;

    public Transform startPoint;

    public GameKeeper manager;
    public Shooting shootScript;

    public GameObject ghostImage;
    public bool ghostExists = false;
    public float placeMovementSpeed = 2f;



    // Start is called before the first frame update
    void Start()
    {
        arenaCentre = this.transform.position;

        startPoint = GameObject.Find("PositionOne").transform;

        shootScript = GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        SetupPlacement();
    }

    public void SetupPlacement()
    {
        if (manager.setupPhase & !ghostExists)
        {
            Instantiate(ghostImage, startPoint);
            ghostExists = true;

            shootScript.ballCam.gameObject.SetActive(false);
        }
    }

    public void RoundSetup()
    {
        Instantiate(ghostImage, startPoint);
        ghostExists = true;
    }

}
