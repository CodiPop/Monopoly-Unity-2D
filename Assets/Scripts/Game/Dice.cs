using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    public Rigidbody Rigidbody;
    void Start()
    {
        Rigidbody.AddForce(new Vector3(0.0f,0.0f, 100.0f));
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody.AddForce(new Vector3(0.0f,0.0f, 100.0f));

    }
}
