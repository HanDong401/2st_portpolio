using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    [SerializeField] float m_AxisZ = 0f;
    [SerializeField] float m_FollowSpeed = 0f;
    [SerializeField] private Vector3 m_TargetPos;

    private void LateUpdate()
    {
        CameraMove();    
    }

    public void GetTarget(Vector3 _targetPos)
    {
        m_TargetPos = _targetPos;
    }

    void CameraMove()
    {
        transform.position = Vector2.Lerp(transform.position, m_TargetPos, m_FollowSpeed * Time.deltaTime);
        transform.Translate(0f, 0f, m_AxisZ);
    }

}
