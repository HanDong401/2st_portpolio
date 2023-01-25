using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public delegate void OptionEvent();
    public delegate void OnLoadSceneEvent(string _name);
    public delegate void OnOffOptionEvent(bool _bool);
    private OnOffOptionEvent m_BgmEvent = null;
    private OnOffOptionEvent m_EffectEvent = null;
    private OnLoadSceneEvent m_LoadSceneEvent = null;
    private OptionEvent m_GameQuitEvent = null;
    [SerializeField] Button m_BgmButton, m_EffectButton, m_ToMainMenuButton, m_ToTownButton, m_GameQuitButton, m_GameQuitYesButton = null;
    private CheckPoint m_BgmCheckPoint, m_EffectCheckPoint = null;

    public void OptionAwake()
    {
        if (m_BgmButton == null)
            m_BgmCheckPoint = m_BgmButton.GetComponent<CheckPoint>();
        if (m_EffectButton == null)
            m_EffectCheckPoint = m_EffectButton.GetComponent<CheckPoint>();
        SetBgmButton();
        SetEffectButton();
        SetToMainMenuButton();
        SetToTownButton();
        SetGameQuitYesButton();
    }

    public void ActiveToMainMenuButton(bool _bool)
    {
        m_ToMainMenuButton.gameObject.SetActive(_bool);
    }

    public void ActiveToTownButton(bool _bool)
    {
        m_ToTownButton.gameObject.SetActive(_bool);
    }

    public void ActiveGameQuitButton(bool _bool)
    {
        m_GameQuitButton.gameObject.SetActive(_bool);
    }

    #region 이벤트 연결 함수

    public void AddBgmEvent(OnOffOptionEvent _callback)
    {
        m_BgmEvent = _callback;
    }

    public void AddEffectEvent(OnOffOptionEvent _callback)
    {
        m_EffectEvent = _callback;
    }

    public void AddLoadSceneEvent(OnLoadSceneEvent _callback)
    {
        m_LoadSceneEvent = _callback;
    }

    public void AddGameQuitYesEvent(OptionEvent _callback)
    {
        m_GameQuitEvent = _callback;
    }

    #endregion

    #region 버튼 연결 함수

    private void SetBgmButton()
    {
        m_BgmButton.onClick.AddListener(BgmOnOff);
    }

    private void SetEffectButton()
    {
        m_EffectButton.onClick.AddListener(EffectOnOff);
    }

    private void SetToMainMenuButton()
    {
        m_ToMainMenuButton.onClick.AddListener(OnToMainMenu);
    }

    private void SetToTownButton()
    {
        m_ToTownButton.onClick.AddListener(OnToTown);
    }

    private void SetGameQuitYesButton()
    {
        m_GameQuitYesButton.onClick.AddListener(OnGameQuit);
    }

    #endregion

    #region 실행 함수

    private void BgmOnOff()
    {
        if (m_BgmEvent != null)
            m_BgmEvent(m_BgmCheckPoint.GetIsClickButton());
    }

    private void EffectOnOff()
    {
        if (m_EffectEvent != null)
            m_EffectEvent(m_EffectCheckPoint.GetIsClickButton());
    }

    private void OnToMainMenu()
    {
        if (m_LoadSceneEvent != null)
            m_LoadSceneEvent("MainMenuScene");
    }

    private void OnToTown()
    {
        if (m_LoadSceneEvent != null)
            m_LoadSceneEvent("TownScene");
    }

    private void OnGameQuit()
    {
        m_GameQuitEvent?.Invoke();
    }

    #endregion
}
