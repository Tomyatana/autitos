using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCheckpointGen : MonoBehaviour
{
    public string CheckpointName = "Checkpoint";
    public GameObject CheckpointPrefab;
    public string StartLineName = "Start";
    public GameObject StartLinePrefab;
    public CheckPointManager checkPointManager;

    public void Generate() {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform) {
            GameObject obj = null;
            CheckpointController checkpoint = null;
            if(child.name.StartsWith(CheckpointName)) {
                obj = Instantiate(CheckpointPrefab, child.position, child.rotation);
            }
            if(child.name == StartLineName) {
                obj = Instantiate(StartLinePrefab, child.position, child.rotation);
            }
            if(obj == null) continue;
            setTriggerSize(obj, child);
            checkpoint = obj.GetComponent<CheckpointController>();
            checkpoint.checkPointManager = checkPointManager;
            Destroy(child.gameObject);
            children.Add(obj.transform);
            if(child.name.StartsWith(CheckpointName)) {
                checkPointManager.Checkpoints.Add(checkpoint);
            }
            if(child.name == StartLineName) {
                checkPointManager.StartLine = checkpoint;
            }
        }

        foreach(Transform child in children) {
            child.SetParent(transform);
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
