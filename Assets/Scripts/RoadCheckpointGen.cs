using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCheckpointGen : MonoBehaviour
{
    [SerializeField]
    string CheckpointName = "Checkpoint";
    [SerializeField]
    GameObject CheckpointPrefab;
    [SerializeField]
    string StartLineName = "Start";
    [SerializeField]
    GameObject StartLinePrefab;
    [SerializeField]
    CheckPointManager checkPointManager;

    void Awake() {
        foreach (Transform child in transform) {
            if(child.name == StartLineName) {
                GameObject obj = Instantiate(StartLinePrefab, child.position, child.rotation);
                setTriggerSize(obj, child);
                CheckpointController checkpoint = obj.GetComponent<CheckpointController>();
                checkpoint.checkPointManager = checkPointManager;
                checkPointManager.Checkpoints.Add(checkpoint);
                Destroy(child.gameObject);
            }
            if(child.name.StartsWith(CheckpointName)) {
                GameObject obj = Instantiate(CheckpointPrefab, child.position, child.rotation);
                setTriggerSize(obj, child);
                CheckpointController checkpoint = obj.GetComponent<CheckpointController>();
                checkpoint.checkPointManager = checkPointManager;
                checkPointManager.Checkpoints.Add(checkpoint);
                Destroy(child.gameObject);
            }
        }
    }

    void setTriggerSize(GameObject obj, Transform oldObj) {
        BoxCollider col = obj.GetComponent<BoxCollider>();
        Vector3 parentScale = oldObj.transform.parent.localScale;
        if(parentScale == null) parentScale = Vector3.one;
        Vector3 newSize = new Vector3(oldObj.localScale.x * parentScale.x, oldObj.localScale.z * parentScale.y, oldObj.localScale.y * parentScale.z);
        col.size = newSize;
    }
}
