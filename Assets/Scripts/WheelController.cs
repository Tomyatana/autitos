using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public float suspensionStrength = 5f;
    public float suspensionRest = 5f;
    public float suspensionMax = 7f;
    public float suspensionDamping = 0.9f;
    public float slidingFactor = 1f;
    public bool isSteer = false;

    Rigidbody rb;
    Vector3 prev_vel = Vector3.zero;
    [SerializeField] float maxRotationVel = 90;
    PID slidePid = new PID();
    public float der = 0.8f;
    public float integ = 0.8f;
    public float prop = 0.8f;
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = Mathf.Deg2Rad * maxRotationVel;
        slidePid.derivative = der;
        slidePid.integral = integ;
        slidePid.proportional = prop;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        slidePid.derivative = der;
        slidePid.integral = integ;
        slidePid.proportional = prop;
        Vector3 rel_velocity = Quaternion.Inverse(transform.rotation) * rb.velocity;
        float forward = Vector3.Dot(rel_velocity, transform.TransformDirection(Vector3.forward));
        float horizontal = Vector3.Dot(rel_velocity, transform.TransformDirection(Vector3.right));

        float offset = SuspensionOffset();
        Vector3 suspension = Vector3.zero;
        Vector3 movement = Vector3.zero;
        // Suspension
        if (offset == 0) {
            return;
        }
        Debug.DrawLine(transform.position, transform.position + Vector3.up * (50 / ((offset * suspensionStrength) - (rb.velocity.y * suspensionDamping))), Color.green);
        suspension.y = (offset * suspensionStrength) - (rb.velocity.y * suspensionDamping);

        // Sliding / Driftin' yay
        Vector3 slide = Vector3.zero;
        float sliding_force = -slidePid.Update(Time.deltaTime, rel_velocity.x, 0);
        Debug.DrawLine(transform.position, transform.position - transform.TransformDirection(transform.right) * (1/(1+sliding_force)), Color.red);
        slide.x = sliding_force;
        print(sliding_force);

        if (isSteer) {
            movement.z = 50;
        }

        rb.AddRelativeForce(suspension);
        rb.AddRelativeForce(movement);
        rb.AddRelativeForce(slide);
    }

    float SuspensionOffset() {
        LayerMask mask = LayerMask.GetMask("World");
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, suspensionMax, mask)) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }
        if(hit.distance < Mathf.Infinity && hit.distance != 0) {
            return -(hit.distance - suspensionRest);
        } else {
            return 0;
        }
    }
}

class PID {
    public float proportional;
    public float integral;
    public float derivative;

    float val_last = 0;
    float stored = 0;
    public float Update(float dt, float val, float target) {
        float error = val - target;
        float val_rate = (val - val_last) / dt;
        val_last = val;
        stored += (error * dt);

        float prop = proportional * error;
        float integ = integral * stored;
        float der = val_rate * derivative;

        return prop + integ + der;
    }
}
