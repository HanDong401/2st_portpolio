using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] Button m_StartButton, m_OptionButton, m_ExitButton;

    public void OnEnable()
    {
        Debug.Log("버튼 OnEnable");
        SceneManager.sceneLoaded += SetButton;
    }

    private void SetButton(Scene scene, LoadSceneMode _mod)
    {
        Debug.Log("버튼 SetButton");
        MainSceneManager sceneManager = GameObject.FindObjectOfType<MainSceneManager>();
        m_StartButton.onClick.AddListener(sceneManager.LoadTestScene);
        m_OptionButton.onClick.AddListener(sceneManager.LoadMainMenuScene);
        m_ExitButton.onClick.AddListener(OnExitUI);
    }

    public void OnOptionUI()
    {
        
    }

    public void OnExitUI()
    {

    }

    private void OnDisable()
    {
        Debug.Log("버튼 DisEisable");
        SceneManager.sceneLoaded -= SetButton;
    }
}
