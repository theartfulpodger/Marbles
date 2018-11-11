using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotDisplay : MonoBehaviour
{

    public GameObject manager;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Power: " + Mathf.RoundToInt(manager.gameObject.GetComponent<Shooting>().shotPower).ToString();
    }
}
