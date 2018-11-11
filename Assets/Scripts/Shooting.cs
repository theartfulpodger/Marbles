using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject shooterBall;

    public GameKeeper gameKeeper;

    public Rigidbody rb;

    public Camera ballCam;

    public LineRender aimLine;

    [Header("Shot Power")]
    public float shotPower = 0.0f;
    [Range(10.0f,20.0f)]
    public float shotPowerSpeed = 15.0f;

    [Header("Aiming")]
    public float turnSpeedBase = 10.0f;
    public float turnSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameKeeper = GameObject.Find("GameManager").GetComponent<GameKeeper>();
        ballCam = GameObject.Find("BallCam").GetComponent<Camera>();
        aimLine = shooterBall.gameObject.GetComponent<LineRender>();

        ballCam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        shooterBall = GameObject.FindGameObjectWithTag("Player");

        ShotDirection();
        ShootBall();
    }

    public void ShotDirection()
    {
        // for rotation reference https://unity3d.com/learn/tutorials/topics/scripting/translate-and-rotate

        if (gameKeeper.shootPhase && shooterBall != null)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                shooterBall.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                shooterBall.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
            }
        }

        // Faster Turn with shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            turnSpeed = turnSpeedBase * 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            turnSpeed = turnSpeedBase;
        }
    }

    public void ShootBall()
    {


        if (gameKeeper.shootPhase)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                shotPower += shotPowerSpeed * Time.deltaTime;

                gameKeeper.currentRoundTime = gameKeeper.maxRoundTime;
                gameKeeper.roundCountDown = true;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                rb = shooterBall.GetComponent<Rigidbody>();
                aimLine = shooterBall.gameObject.GetComponent<LineRender>();

                rb.AddForce((shooterBall.gameObject.transform.forward * shotPower) * 100);
                ballCam.gameObject.SetActive(true);
                shooterBall.transform.GetChild(0).gameObject.SetActive(false);

                gameKeeper.shootPhase = false;
                gameKeeper.postShotPhase = true;

                aimLine.on = false;
            }
        }
    }
}
