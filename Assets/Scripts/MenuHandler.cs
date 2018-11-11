using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    public GameObject singlePlayer;
    public GameObject options;
    public GameObject exitGame;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSinglePlayer()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
