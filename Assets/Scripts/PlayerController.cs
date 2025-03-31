using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float Accel = 0.1f;
    public float MaxSpeed;
    public float MaxRotationSpeed;
    public float RotationAccel = 0.1f;
    public float rotationSpeed = 0;
    public float speed = 0;
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
            speed = Mathf.Min(speed + Accel*Time.deltaTime, 1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed = Mathf.Max(speed - Accel*Time.deltaTime, 0f);
        }
        if(Input.GetKey(KeyCode.A))
        {
            rotationSpeed = Mathf.Min(rotationSpeed + RotationAccel*Time.deltaTime, 1f);
        }
        if(Input.GetKey(KeyCode.D))
        {
            rotationSpeed = Mathf.Max(rotationSpeed - RotationAccel*Time.deltaTime, -1f);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed * MaxSpeed;
        rb.AddTorque(Vector3.up * rotationSpeed * MaxRotationSpeed * -1, ForceMode.Acceleration);
        rb.maxAngularVelocity = Mathf.Deg2Rad * 90;
    }
}
