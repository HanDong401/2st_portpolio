using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("���Ŵ��� Start");
        DontDestroyOnLoad(this);
        LoadMainMenuScene();
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void LoadTestScene()
    {
        SceneManager.LoadScene("TestScene");
    }
}
