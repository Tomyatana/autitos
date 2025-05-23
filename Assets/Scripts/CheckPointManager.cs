using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManager : MonoBehaviour
{
    public List<CheckpointController> Checkpoints = new List<CheckpointController>();

    public CheckpointController StartLine;

    [SerializeField]
    GameObject player;

    void Start() {
        Debug.Assert(StartLine.isStartLine);
        player.transform.position = StartLine.transform.position;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }

    public void CheckState() {
        foreach(CheckpointController checkpoint in Checkpoints) {
            if(!checkpoint.passed) return;
        }
        print("Gano!");
    }
}
