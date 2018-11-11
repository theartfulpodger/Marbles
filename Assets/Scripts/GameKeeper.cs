using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameKeeper : MonoBehaviour
{
    public bool setupPhase = true;
    public bool shootPhase = false;
    public bool postShotPhase = false;

    public GameObject shooterBall;
    public Shooting shooting;
    public Placement placement;

    public int startPoints = 0;
    public int currentPoints = 0;

    public Vector3 pos;
    public Quaternion rotation;

    public GameObject[] pawnRb;
    public Rigidbody shooterRb;

    [Header("UI Objects")]
    public GameObject inGameMenu;
    public GameObject inGameGuide;
    public GameObject aboutPanel;
    public GameObject howToPanel;
    public GameObject rulesPanel;
    public GameObject shotClock;

    [Header("Final Score")]
    public GameObject finalScore;
    public GameObject finalOne;
    public GameObject finalTwo;
    public GameObject finalThree;
    public GameObject finalFour;
    public GameObject finalFive;
    public GameObject totalScore;

    [Header("Round Tracker")]
    public int currentRound = 1;
    public int maxRoundCount = 5;
    public int minRoundCount = 1;
    [Space]
    public bool roundCountDown = false;
    public float currentRoundTime = 0.0f;
    public float maxRoundTime = 10.0f;
    [Space]
    public bool shotClockCountdown = false;
    public float shotClockTimer = 0.0f;
    public float maxShotClockTimer = 30.0f;

    public int roundOneScore = 0;
    public int roundTwoScore = 0;
    public int roundThreeScore = 0;
    public int roundFourScore = 0;
    public int roundFiveScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        inGameMenu.SetActive(false);
        inGameGuide.SetActive(false);

        pawnRb = GameObject.FindGameObjectsWithTag("Pawn");
        shooterRb = shooterBall.GetComponent<Rigidbody>();

        // Placing ghost ball at the start is currently in the placement script
        // TODO move the ghost ball placement into this script from placement script
    }

    // Update is called once per frame
    void Update()
    {
        SetupPhase();
        OpenMenu();
        TurnTracking();
        ShotClockCountdown();

        currentPoints = roundOneScore + roundTwoScore + roundThreeScore + roundFourScore + roundFiveScore;
    }

    public void SetupPhase()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            setupPhase = false;
            shootPhase = true;

            pos = GameObject.Find("ShooterMarbleGhost(Clone)").transform.position;
            rotation = GameObject.Find("ShooterMarbleGhost(Clone)").transform.rotation;

            Instantiate(shooterBall, pos, rotation);
            Destroy(GameObject.Find("ShooterMarbleGhost(Clone)"));
            shooterBall.GetComponent<LineRender>().enabled = true;

            shotClockTimer = maxShotClockTimer;
            shotClockCountdown = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Pawn"))
        {
            Destroy(other.gameObject);
            if (currentRound == 1)
            {
                roundOneScore++;
            }
            else if (currentRound == 2)
            {
                roundTwoScore++;
            }
            else if (currentRound == 3)
            {
                roundThreeScore++;
            }
            else if (currentRound == 4)
            {
                roundFourScore++;
            }
            else if (currentRound == 5)
            {
                roundFiveScore++;
            }
        }

        if (other.tag == "Player")
        {
            if (currentRound <= maxRoundCount)
            {
                StopBalls();
                currentRound++;
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                setupPhase = true;
                placement.RoundSetup();
                shotClockTimer = maxShotClockTimer;
                shotClockCountdown = true;
                currentRoundTime = maxRoundTime;
                shooting.shotPower = 0;
                shooting.ballCam.gameObject.SetActive(false);
                shotClockCountdown = false;
                roundCountDown = false;
            }
            else
            {
                EndCurrentGame();
            }

        }
    }

    public void TurnTracking()
    {
        if (((int)currentRoundTime) < 0)
        {
            shootPhase = false;
            currentRound++;
            if (currentRound <= maxRoundCount)
            {
                StopBalls();
                setupPhase = true;
                placement.RoundSetup();
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                shotClockTimer = maxShotClockTimer;
                shotClockCountdown = true;
                currentRoundTime = maxRoundTime;
                shooting.shotPower = 0;
                shooting.ballCam.gameObject.SetActive(false);
                shotClockCountdown = false;
                roundCountDown = false;
            }
            else
            {
                EndCurrentGame();
            }
        }

        // starts the overall round time countdown
        if (shootPhase && shotClockCountdown)
        {
            shotClockTimer -= 1 * Time.deltaTime;
        }

        // starts the countdown after the shot is taken to end the round
        if (postShotPhase && roundCountDown)
        {
            currentRoundTime -= 1 * Time.deltaTime;
        }
    }

    public void EndCurrentGame()
    {
        finalOne.GetComponent<Text>().text = "Round One: " + roundOneScore.ToString();
        finalTwo.GetComponent<Text>().text = "Round Two: " + roundTwoScore.ToString();
        finalThree.GetComponent<Text>().text = "Round Three: " + roundThreeScore.ToString();
        finalFour.GetComponent<Text>().text = "Round Four: " + roundFourScore.ToString();
        finalFive.GetComponent<Text>().text = "Round Five: " + roundFiveScore.ToString();
        totalScore.GetComponent<Text>().text = "Total: " + currentPoints.ToString();
        finalScore.SetActive(true);
    }

    public void StopBalls()
    {
        for (int i = 0; i < pawnRb.Length; i++)
        {
            pawnRb[i].GetComponent<Rigidbody>().angularDrag = 100;
            pawnRb[i].GetComponent<Rigidbody>().angularDrag = 1;
        }
    }

    #region UI
    public void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inGameMenu.activeInHierarchy == false)
        {
            inGameMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inGameMenu.activeInHierarchy)
        {
            inGameMenu.SetActive(false);
        }
    }

    public void ShotClockCountdown()
    {
        shotClock.GetComponent<Text>().text = "Shot Clock: " + Mathf.RoundToInt(currentRoundTime).ToString();
    }

    public void CloseMenu()
    {

        inGameMenu.SetActive(false);

    }

    public void OpenGuide()
    {
        inGameGuide.SetActive(true);
    }

    public void CloseGuide()
    {
        inGameGuide.SetActive(false);
    }

    public void ShowAbout()
    {
        aboutPanel.SetActive(true);
        howToPanel.SetActive(false);
        rulesPanel.SetActive(false);
    }

    public void ShowRules()
    {
        aboutPanel.SetActive(false);
        howToPanel.SetActive(false);
        rulesPanel.SetActive(true);

    }

    public void ShowHowTo()
    {
        aboutPanel.SetActive(false);
        howToPanel.SetActive(true);
        rulesPanel.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(1);
    }
    #endregion

}
