using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public delegate void SceneLoadEvent(string _name);
    private SceneLoadEvent m_SceneLoadEvent = null;
    public delegate void InitPlayerEvent();
    private InitPlayerEvent m_InitPlayerEvent = null;

    [SerializeField] Button m_ToTownButton = null;

    public void SetToTownButtonEvent()
    {
        m_ToTownButton.onClick.RemoveAllListeners();
        m_ToTownButton.onClick.AddListener(LoadTownScene);
    }

    public void AddSceneLoadEvent(SceneLoadEvent _callback)
    {
        m_SceneLoadEvent = _callback;
    }

    public void AddInitPlayerEvent(InitPlayerEvent _callback)
    {
        m_InitPlayerEvent = _callback;
    }

    private void LoadTownScene()
    {
        m_SceneLoadEvent?.Invoke("TownScene");
        m_InitPlayerEvent?.Invoke();
        this.gameObject.SetActive(false);
    }
}
