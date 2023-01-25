using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public delegate void UIManagerSceneEvent(string _name);
    public delegate void UIManagerEvent();
    private UIManagerSceneEvent m_SceneLoadEvent = null;
    private UIManagerEvent m_GameQuitEvent = null;
    [SerializeField] private MainUI m_Main = null;
    [SerializeField] private Option m_Option = null;
    [SerializeField] private Inventory m_Inventory = null;
    [SerializeField] private MainMenuButton m_MainMenuButton = null;
    [SerializeField] private Image m_Fade = null;
    private Coroutine m_FadeCoroutine = null;

    private int m_CurrHp;
    private int m_MaxHp;
    private int m_CurrSp;
    private int m_MaxSp;
    private bool mbIsFadeOut = false;

    public void UIManagerAwake()
    {
        DontDestroyOnLoad(this.gameObject);
        
        if (m_Main == null)
        {
            m_Main = this.GetComponentInChildren<MainUI>();
            if (m_Main != null)
            {
                m_Main.MainUIAwake();
                StartCoroutine(SetMainUI());
                ActiveMainUI(false);
            }
        }
        if (m_Option == null)
        {
            m_Option = this.GetComponentInChildren<Option>();
            if (m_Option != null)
            {
                m_Option.AddBgmEvent(null);
                m_Option.AddEffectEvent(null);
                m_Option.AddGameQuitYesEvent(OnGameQuitEvent);
                m_Option.AddLoadSceneEvent(OnLoadSceneEvent);
                m_Option.OptionAwake();
                ActiveOption();
            }
        }
    }

    public void UIManagerStart()
    {
        if (m_MainMenuButton == null)
        {
            m_MainMenuButton = GameObject.FindObjectOfType<MainMenuButton>();
            if (m_MainMenuButton != null)
            {
                m_MainMenuButton.AddStartEvent(OnLoadSceneEvent);
                m_MainMenuButton.AddOptionEvent(OnMainMenuOptionEvent);
                m_MainMenuButton.AddExitEvent(OnGameQuitEvent);
                m_MainMenuButton.MainMenuButtonAwake();
            }
        }
    }

    public void OnLoadSceneEvent(string _name)
    {
        m_Fade.gameObject.SetActive(true);
        FadeOut(_name);
    }

    private void OnGameQuitEvent()
    {
        m_GameQuitEvent?.Invoke();
    }

    private void OnBgmEvent()
    {

    }

    private void OnEffectEvent()
    {

    }

    private void OnMainMenuOptionEvent()
    {
        m_Option.ActiveGameQuitButton(false);
        m_Option.ActiveToMainMenuButton(false);
        m_Option.ActiveToTownButton(false);
        ActiveOption();
    }

    public void OnTownOptionEvent()
    {
        m_Option.ActiveGameQuitButton(true);
        m_Option.ActiveToMainMenuButton(true);
        m_Option.ActiveToTownButton(false);
        ActiveOption();
    }

    public void OnDugneonOptionEvent()
    {
        m_Option.ActiveGameQuitButton(true);
        m_Option.ActiveToMainMenuButton(false);
        m_Option.ActiveToTownButton(true);
        ActiveOption();
    }

    public void AddSceneLoadEvent(UIManagerSceneEvent _callback)
    {
        m_SceneLoadEvent = _callback;
    }

    public void AddGameQuitEvent(UIManagerEvent _callback)
    {
        m_GameQuitEvent = _callback;
    }

    public void ActiveMainUI(bool _bool)
    {
        m_Main.gameObject.SetActive(_bool);
    }

    public void ActiveOption()
    {
        if (m_Option.gameObject.activeSelf.Equals(true))
            m_Option.gameObject.SetActive(false);
        else
            m_Option.gameObject.SetActive(true);
    }

    public void ActiveInventory(bool _bool)
    {
        m_Inventory.gameObject.SetActive(_bool);
    }

    public void UpdateMainUI(int _currHp = 100, int _maxHp = 100, int _currSp = 1, int _maxSp = 1)
    {
        this.m_CurrHp = _currHp;
        this.m_MaxHp = _maxHp;
        this.m_CurrSp = _currSp;
        this.m_MaxSp = _maxSp;
    }

    public void FadeOut(string _name)
    {
        if (m_FadeCoroutine != null)
        {
            StopCoroutine(m_FadeCoroutine);
        }
        m_FadeCoroutine = StartCoroutine(FadeOutCoroutine(_name));
    }

    public void FadeIn()
    {
        if (m_FadeCoroutine != null)
        {
            StopCoroutine(m_FadeCoroutine);
        }
        m_FadeCoroutine = StartCoroutine(FadeInCoroutine());

    }

    IEnumerator FadeOutCoroutine(string _name)
    {
        if (mbIsFadeOut.Equals(false))
        {
            float count = 0f;
            while(count <= 1f)
            {
                count += Time.deltaTime;
                m_Fade.color = new Color(0f, 0f, 0f, count);
                yield return null;
            }
            mbIsFadeOut = true;
            if (m_SceneLoadEvent != null)
                m_SceneLoadEvent(_name);
            yield return new WaitForSeconds(1f);
            FadeIn();
        }
    }

    IEnumerator FadeInCoroutine()
    {
        if (mbIsFadeOut.Equals(true))
        {
            float count = 1f;
            while(count >= 0f)
            {
                count -= Time.deltaTime;
                m_Fade.color = new Color(0f, 0f, 0f, count);
                yield return null;
            }
            mbIsFadeOut = false;
            m_Fade.gameObject.SetActive(false);
        }
    }

    IEnumerator SetMainUI()
    {
        while(true)
        {
            m_Main.SetHp(m_CurrHp, m_MaxHp);
            m_Main.SetSp(m_CurrSp, m_MaxSp);
            yield return null;
        }
    }
}
