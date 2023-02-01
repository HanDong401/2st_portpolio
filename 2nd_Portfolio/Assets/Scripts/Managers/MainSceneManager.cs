using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public void SceneManagerAwake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string _name)
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(_name);
        float time = 0f;

        while(true)
        {
            time += Time.unscaledDeltaTime;
            if (time > 1f)
            {
                Time.timeScale = 1f;
                return;
            }
        }
    }
}
