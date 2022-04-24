using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 7 * 7;
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     rb.AddForce(Vector3.forward * 21);
    //     //AudioSource source = GetComponent<AudioSource>();
    //     //source.Play();
    // }


}
