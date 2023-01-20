using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private TownToGoDungeon asdf = null;

    private void Awake()
    {
        asdf = GameObject.FindObjectOfType<TownToGoDungeon>();
    }
}
