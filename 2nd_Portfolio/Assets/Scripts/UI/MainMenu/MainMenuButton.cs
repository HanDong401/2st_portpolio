using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public delegate void MainMenuButtonEvent();
    private MainMenuButtonEvent m_StartEvent = null;
    private MainMenuButtonEvent m_ExitEvent = null;
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
        m_StartEvent?.Invoke();
    }

    private void OnExitButton()
    {
        m_ExitEvent?.Invoke();
    }

    public void AddStartEvent(MainMenuButtonEvent _callback)
    {
        m_StartEvent = _callback;
    }

    public void AddExitEvent(MainMenuButtonEvent _callback)
    {
        m_ExitEvent = _callback;
    }
}
