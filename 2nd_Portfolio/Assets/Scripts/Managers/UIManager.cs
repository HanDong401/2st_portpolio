using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public delegate void UIManagerSceneEvent(string _name);
    public delegate void UIManagerEvent();
    private UIManagerSceneEvent m_MainStartEvent = null;
    private UIManagerEvent m_MainExitEvent = null;
    [SerializeField] private MainUI m_Main = null;
    [SerializeField] private Inventory m_Inventory = null;
    [SerializeField] private MainMenuButton m_MainMenuButton = null;
    [SerializeField] private MainMenuOption m_MainOption = null;
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
                m_MainMenuButton.AddStartEvent(OnMainStartEvent);
                m_MainMenuButton.AddExitEvent(OnMainExitEvent);
                m_MainMenuButton.MainMenuButtonAwake();
            }
        }
    }

    public void OnMainStartEvent(string _name)
    {
        m_Fade.gameObject.SetActive(true);
        FadeOut(_name);
    }

    private void OnMainExitEvent()
    {
        m_MainExitEvent?.Invoke();
    }

    public void AddMainStartEvent(UIManagerSceneEvent _callback)
    {
        m_MainStartEvent = _callback;
    }

    public void AddMainExitEvent(UIManagerEvent _callback)
    {
        m_MainExitEvent = _callback;
    }

    public void ActiveMainUI(bool _bool)
    {
        m_Main.gameObject.SetActive(_bool);
    }

    public void ActiveInventory(bool _bool)
    {
        m_Inventory.gameObject.SetActive(_bool);
    }

    public void InitUIManager(int _currHp = 100, int _maxHp = 100, int _currSp = 1, int _maxSp = 1)
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
            if (m_MainStartEvent != null)
                m_MainStartEvent(_name);
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
