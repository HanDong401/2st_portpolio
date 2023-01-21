using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public void SceneManagerAwake()
    {
        DontDestroyOnLoad(this);
        LoadScene("MainMenuScene");
    }

    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }
}
