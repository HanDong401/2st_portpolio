using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public delegate void MainMenuStartEvent(string _name);
    public delegate void MainMenuExitEvent();
    private MainMenuStartEvent m_StartEvent = null;
    private MainMenuExitEvent m_ExitEvent = null;
    [SerializeField] Button m_StartButton, m_OptionButton, m_ExitButton = null;

    public void MainMenuButtonAwake()
    {
        SetStartButton();
        SetExitButton();
    }

    private void SetStartButton()
    {
        m_StartButton.onClick.AddListener(OnStartButton);
    }

    private void SetExitButton()
    {
        m_ExitButton.onClick.AddListener(OnExitButton);
    }


    private void OnStartButton()
    {
        if (m_StartEvent != null)
            m_StartEvent("TownScene");
    }

    private void OnExitButton()
    {
        m_ExitEvent?.Invoke();
    }

    public void AddStartEvent(MainMenuStartEvent _callback)
    {
        m_StartEvent = _callback;
    }

    public void AddExitEvent(MainMenuExitEvent _callback)
    {
        m_ExitEvent = _callback;
    }
}
