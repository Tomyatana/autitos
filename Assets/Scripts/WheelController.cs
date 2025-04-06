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
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 rel_velocity = rb.GetRelativePointVelocity(transform.position);
        float forward = Vector3.Dot(rb.velocity, transform.TransformDirection(Vector3.forward));
        float horizontal = Vector3.Dot(rel_velocity, -transform.right);

        float offset = SuspensionOffset();
        Vector3 movement = Vector3.zero;
        // Suspension
        if(offset != 0) {
            Debug.DrawLine(transform.position, transform.position + Vector3.up * (50/((offset * suspensionStrength) - (rel_velocity.y * suspensionDamping))), Color.green);
            movement.y = (offset * suspensionStrength) - (rel_velocity.y * suspensionDamping);
        }

        // Sliding / Driftin' yay
        Vector3 slide = Vector3.zero;
        float sliding_force = -(horizontal*slidingFactor);
        print($"{horizontal}, {rel_velocity.x}, {rb.velocity.x}");
        Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(transform.right) * sliding_force, Color.red);
        if(!isSteer) {
            print(sliding_force);
            print(horizontal);
        }
        movement.x = -sliding_force * 0.9f;

        movement.z = transform.forward.z;

        rb.AddForce(movement);
        rb.AddForce(transform.forward);
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
