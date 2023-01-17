using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public bool EffectRange(float _radius, float _deltaTime)
    {
        if (this.transform.localScale.x > _radius * 2f)
        {
            return true;
        }
        transform.localScale += new Vector3(_deltaTime, _deltaTime, 0f);
        return false;
    }
}
