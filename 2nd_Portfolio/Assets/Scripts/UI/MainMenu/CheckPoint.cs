using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Image m_CheckPoint = null;
    [SerializeField] private bool mbIsClickButton = false;

    public bool OnCheckPoint()
    {
        mbIsClickButton = !mbIsClickButton;
        m_CheckPoint.enabled = mbIsClickButton;
        return mbIsClickButton;
    }

}
