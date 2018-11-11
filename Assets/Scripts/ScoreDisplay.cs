using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public GameObject manager;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Score: " +  manager.gameObject.GetComponent<GameKeeper>().currentPoints.ToString();
    }
}
