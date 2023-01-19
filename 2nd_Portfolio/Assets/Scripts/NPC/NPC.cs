using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, Interaction
{
    


    public void InteractionExecute()
    {
        //여기에다가 상호작용하면 대사창 출력되게 해야함

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //여기다가 박스콜라이더 안에 들어왔을 때 Press 'E' to Interact 대사 활성화되게 해줘야함

    }
}
