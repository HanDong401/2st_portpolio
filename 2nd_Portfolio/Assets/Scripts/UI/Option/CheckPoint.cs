using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Image m_CheckPointImage = null;
    [SerializeField] private bool mbIsClickButton = true;

    public void OnCheckPoint()
    {
        mbIsClickButton = !mbIsClickButton;
        m_CheckPointImage.enabled = mbIsClickButton;
    }

    public bool GetIsClickButton()
    {
        return mbIsClickButton;
    }
}
