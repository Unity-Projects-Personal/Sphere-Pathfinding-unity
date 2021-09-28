using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -9.81f;
    public void Attract(Transform body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 normalUp = body.up;
        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
        Quaternion targetRotation = Quaternion.FromToRotation(normalUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }

}
