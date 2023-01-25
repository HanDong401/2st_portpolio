using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, Interaction
{
    public bool mbIsPickUp = false;
    public bool mbIsPop = false;

    [SerializeField] protected Player m_Player = null;
    [SerializeField] protected Inventory m_Inventory = null;

    protected void InitItem()
    {
        m_Player = FindObjectOfType<Player>().GetComponent<Player>();
    }


    public void SetInven()
    {
        Transform invenTrans = m_Inventory.transform;
        this.transform.SetParent(invenTrans);
        this.transform.position = invenTrans.position;
        m_Inventory.GetItem(this);
    }

    public void SetItemActive(bool _isActive)
    {
        this.gameObject.SetActive(_isActive);
    }

    public void SetPosition(Vector2 _targetPos)
    {
        this.transform.position = _targetPos;
    }

    public void SetInventory(Inventory _inven)
    {
        m_Inventory = _inven;
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
        SetInven();
        InitItem();
        Interaction();
    }

    protected virtual void Interaction()
    {

    }
}
