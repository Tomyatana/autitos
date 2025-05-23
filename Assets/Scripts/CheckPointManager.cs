using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public List<CheckpointController> Checkpoints = new List<CheckpointController>();

    public void CheckState()
    {
        foreach(CheckpointController checkpoint in Checkpoints) {
            if(!checkpoint.passed) return;
        }
        print("Gano!");
    }
}
