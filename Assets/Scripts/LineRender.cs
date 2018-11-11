using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{

    public LineRenderer lr;

    public GameObject lrEnd;

    public bool on = true;

    // Start is called before the first frame update
    void Start()
    {
        lr = this.GetComponent<LineRenderer>();
        //lrEnd = GameObject.Find("LineRendEnd");
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, this.gameObject.transform.position);
        lr.SetPosition(1, lrEnd.transform.position);
        OnOff();
    }

    public void OnOff()
    {
        if (on)
        {
            lr.enabled = true;
        }
        else if(!on)
        {
            lr.enabled = false;
        }
    }
}
