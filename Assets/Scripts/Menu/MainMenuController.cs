using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    Scene gameScene;
    // Start is called before the first frame update
    void Start()
    {
        gameScene = SceneManager.GetSceneByBuildIndex(1);
        print(gameScene.name);
        SceneManager.LoadScene(gameScene.buildIndex, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame() {
        SceneManager.SetActiveScene(gameScene);
    }
}
