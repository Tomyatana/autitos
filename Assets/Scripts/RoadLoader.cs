using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLoader : MonoBehaviour
{
    [SerializeField]
    CheckPointManager checkPointManager;

    [SerializeField]
    GameObject Checkpoint;

    [SerializeField]
    GameObject StartLine;

    [SerializeField]
    int RoadLayer;

    public GameObject Road;

    void Start() {
        RoadCheckpointGen checkpointGen = Road.AddComponent<RoadCheckpointGen>();
        checkpointGen.checkPointManager = checkPointManager;
        checkpointGen.CheckpointPrefab = Checkpoint;
        checkpointGen.StartLinePrefab = StartLine;
        checkpointGen.Generate();

        foreach(Transform child in Road.transform) {
            if(child.GetComponent<MeshRenderer>() == null) {
                continue;
            }
            MeshCollider col = child.gameObject.AddComponent<MeshCollider>();
            child.gameObject.layer = RoadLayer;
        }
    }
}
