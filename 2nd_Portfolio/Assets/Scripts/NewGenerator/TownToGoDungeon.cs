using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownToGoDungeon : MonoBehaviour, Interaction
{
    public void InteractionExecute()
    {
        Debug.Log("이동성공");
        //SceneManager.LoadScene("");//이동할 씬 이름
    }
}
