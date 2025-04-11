using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaa : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 rel_vel = Quaternion.Inverse(transform.rotation) * rb.velocity;
        print(rel_vel);
        rb.velocity = transform.forward;
    }
}
