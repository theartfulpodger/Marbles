
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{

    public GameObject dirtParticle;
    public GameObject glassParticle;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        // spawn particles at the point of contact
        if (collision.gameObject.tag == "Dirt")
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            //will destroy the cloned particles after 2 seconds (to increase performance)
            Destroy(Instantiate(dirtParticle, pos, rot), 2f);
        }
        else if (collision.gameObject.tag == "Marble")
        {

        }
    }
}
