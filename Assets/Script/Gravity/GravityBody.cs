using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    public GravityAttractor planet_attractor;
    Transform my_transform;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
        my_transform = transform;
    }

    void Update()
    {
        planet_attractor.Attract(my_transform);
    }
}
