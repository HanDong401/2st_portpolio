using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProps : MonoBehaviour
{
    [SerializeField] protected Vector2Int propsPosition = Vector2Int.zero;

    protected abstract void SetPropsPosition();
}
