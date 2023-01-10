using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, Interaction
{
    public bool mbIsPickUp = false;
    public bool mbIsPop = false;

    public void SetInven(Inventory _inven)
    {
        Transform invenTrans = _inven.transform;
        this.transform.SetParent(invenTrans);
        this.transform.position = invenTrans.position;
        _inven.GetItem(this);
    }

    public void SetItemActive(bool _isActive)
    {
        this.gameObject.SetActive(_isActive);
    }

    public void SetPosition(Vector2 _targetPos)
    {
        this.transform.position = _targetPos;
    }

    private void PickUp()
    {
        mbIsPickUp = true;
        this.GetComponent<Renderer>().enabled = false;
        this.GetComponent<Collider2D>().enabled = false;
    }

    public void InteractionExecute()
    {
        PickUp();
        SetInven(Inventory.Instance);
    }

    protected virtual void Interaction()
    {

    }
}
