using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public List<CheckpointController> Checkpoints = new List<CheckpointController>();

    public CheckpointController StartLine;

    public bool finished;

    [SerializeField]
    GameObject player;

    [SerializeField]
    UIManager uiManager;

    float timer = 0;

    void Start() {
        Debug.Assert(StartLine.isStartLine);
        player.transform.position = StartLine.transform.position;
        Quaternion rotation = StartLine.transform.rotation;
        player.transform.rotation.Set(rotation.x, rotation.y, 0, rotation.w);
    }

    void Update() {
        if (finished) return;
        timer += Time.deltaTime;
        uiManager.UpdateTimer(timer);
    }

    public void CheckState() {
        foreach (CheckpointController checkpoint in Checkpoints) {
            if (!checkpoint.passed) return;
        }
        print($"Termino en {timer} segundos!");
        finished = true;
    }

    public float GetTime() {
        return timer;
    }
}
