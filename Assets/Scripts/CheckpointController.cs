using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public bool passed = false;
    public bool isStartLine = false;
    public CheckPointManager checkPointManager;

    void OnTriggerEnter(Collider other) {
        if(other.tag != "Player") return;
        passed = true;
        checkPointManager.CheckState();
    }
}
