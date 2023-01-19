using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestCall : MonoBehaviour
{
    int count = 0;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable");
        SceneManager.sceneLoaded += test;
    }

    private void test(Scene _scene, LoadSceneMode _mod)
    {
        count++;
        Debug.Log(count);
        Debug.Log("Test ·Îµå");
        Debug.Log(_scene.name);
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= test;
    }
}
