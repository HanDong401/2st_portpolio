using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    
    [SerializeField] private TownToGoDungeon Door = null;

    public TownToGoDungeon GetDoor()
    {
        return Door;
    }
}
