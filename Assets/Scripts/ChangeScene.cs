using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene Instance;
    public int sceneIndex;
    void Awake(){
        Instance = this;
    }
    public void GoToGame(){
        sceneIndex ++;
        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitApp(){
        Application.Quit();
    }
}
