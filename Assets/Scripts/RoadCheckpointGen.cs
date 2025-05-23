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

    void Awake() {
        foreach (Transform child in transform) {
            if(child.name == StartLineName) {
                GameObject obj = Instantiate(StartLinePrefab, child.position, Quaternion.identity);
                setTriggerSize(obj, child);
                Destroy(child.gameObject);
            }
            if(child.name.StartsWith(CheckpointName)) {
                GameObject obj = Instantiate(CheckpointPrefab, child.position, Quaternion.identity);
                setTriggerSize(obj, child);
                Destroy(child.gameObject);
            }
        }
    }

    void setTriggerSize(GameObject obj, Transform oldObj) {
        BoxCollider col = obj.GetComponent<BoxCollider>();
        Vector3 newSize = new Vector3(oldObj.localScale.x, oldObj.localScale.z, oldObj.localScale.y);
        col.size = newSize;
    }
}
