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
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 rel_velocity = rb.GetRelativePointVelocity(transform.position);
        float forward = Vector3.Dot(rb.velocity, transform.TransformDirection(Vector3.forward));
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
        float sliding_force = -(horizontal * slidingFactor);
        Debug.DrawLine(transform.position, transform.position - transform.TransformDirection(transform.right) * (5/(1+sliding_force)), Color.red);
        slide.x = -sliding_force;

        if (isSteer) {
            movement.z = 50;
        }

        rb.AddRelativeForce(suspension);
        rb.AddRelativeForce(movement);
        rb.AddRelativeForce(slide, ForceMode.Acceleration);
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
