using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("¾À¸Å´ÏÀú Start");
        DontDestroyOnLoad(this);
        LoadScene("MainMenuScene");
    }

    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }
}
