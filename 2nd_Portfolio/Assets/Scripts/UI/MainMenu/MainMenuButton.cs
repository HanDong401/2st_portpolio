using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public delegate void LoadSceneEvent(string _name);
    public delegate void MainMenuEvent();
    private LoadSceneEvent m_StartEvent = null;
    private MainMenuEvent m_OptionEvent = null;
    private MainMenuEvent m_ExitEvent = null;
    [SerializeField] Button m_StartButton, m_OptionButton, m_ExitButton = null;

    public void MainMenuButtonAwake()
    {
        SetStartButton();
        SetOptionButton();
        SetExitButton();
    }

    private void SetStartButton()
    {
        m_StartButton.onClick.AddListener(OnStartButton);
    }

    private void SetOptionButton()
    {
        m_OptionButton.onClick.AddListener(OnOptionButton);
    }

    private void SetExitButton()
    {
        m_ExitButton.onClick.AddListener(OnExitButton);
    }


    private void OnStartButton()
    {
        if (m_StartEvent != null)
            m_StartEvent("InitScene_MainMenuToTown");
    }

    private void OnOptionButton()
    {
        m_OptionEvent?.Invoke();
    }
    
    private void OnExitButton()
    {
        m_ExitEvent?.Invoke();
    }

    public void AddStartEvent(LoadSceneEvent _callback)
    {
        m_StartEvent = _callback;
    }

    public void AddOptionEvent(MainMenuEvent _callback)
    {
        m_OptionEvent = _callback;
    }

    public void AddExitEvent(MainMenuEvent _callback)
    {
        m_ExitEvent = _callback;
    }
}
