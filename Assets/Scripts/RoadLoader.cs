using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLoader : MonoBehaviour
{
    [SerializeField]
    CheckPointManager checkPointManager;

    [SerializeField]
    GameObject Floor;

    [SerializeField]
    GameObject Checkpoint;

    [SerializeField]
    GameObject StartLine;

    [SerializeField]
    int RoadLayer;

    [SerializeField]
    GameObject Road;

    [SerializeField]
    float OffsetMult = 0.9f;

    void Awake() {
        float y_offset = Floor.transform.localScale.y - Floor.transform.localScale.y / OffsetMult;
        Vector3 pos = new Vector3(Floor.transform.position.x, Floor.transform.position.y + y_offset, Floor.transform.position.z);
        GameObject newRoad = Instantiate(Road, pos, Quaternion.identity);
        GameObject roadModel = newRoad.transform.GetChild(0).gameObject;
        RoadCheckpointGen checkpointGen = roadModel.AddComponent<RoadCheckpointGen>();
        checkpointGen.checkPointManager = checkPointManager;
        checkpointGen.CheckpointPrefab = Checkpoint;
        checkpointGen.StartLinePrefab = StartLine;
        checkpointGen.Generate();

        foreach(Transform child in roadModel.transform) {
            if(child.GetComponent<MeshRenderer>() == null) {
                continue;
            }
            MeshCollider col = child.gameObject.AddComponent<MeshCollider>();
            child.gameObject.layer = RoadLayer;
        }
    }
}
