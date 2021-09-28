using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float move_speed;
    private Vector3 move_dir;

    public Rigidbody rb;

    void Update()
    {
        move_dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }
    void FixedUpdate()
    {

        rb.MovePosition(rb.position + transform.TransformDirection(move_dir) * move_speed * Time.deltaTime);

    }
}
