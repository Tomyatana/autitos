using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCheckpointGen : MonoBehaviour
{
    [SerializeField]
    string CheckpointName = "Checkpoint";
    [SerializeField]
    GameObject CheckpointPrefab;

    void Awake() {
        foreach (Transform child in transform) {
            if(child.name != CheckpointName) continue;
            Instantiate(CheckpointPrefab, child.position, Quaternion.identity);
            Destroy(child.gameObject);
        }
    }
}
