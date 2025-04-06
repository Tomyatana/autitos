using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float Accel;
    public float MaxSpeed;
    public float MaxRotationSpeed;
    public float RotationAccel = 0.1f;
    public float rotationSpeed = 0;

    float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            speed = Mathf.Min(speed + Accel, MaxSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed = Mathf.Max(speed - Accel, -MaxSpeed);
        }
    }
}
