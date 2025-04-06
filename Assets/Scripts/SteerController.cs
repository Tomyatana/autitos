using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerController : MonoBehaviour
{
    HingeJoint hinge;
    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        bool rotated = false;
        if(Input.GetKey(KeyCode.A)) {
            transform.localRotation = Quaternion.AngleAxis(hinge.limits.min, hinge.axis);
            rotated = true;
        }
        if(Input.GetKey(KeyCode.D)) {
            transform.localRotation = Quaternion.AngleAxis(hinge.limits.max, hinge.axis);
            rotated = true;
        }
        if(!rotated) {
            transform.localRotation = Quaternion.AngleAxis(0, Vector3.one);
        }
    }

}
