using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    protected int m_ItemID;
    public int ItemID
    {
        get { return m_ItemID; }
    }
    private bool mbIsCanPickUp = false;

    [SerializeField] Text m_PickUpText = null;

    private void Start()
    {
        m_PickUpText.gameObject.SetActive(false);
    }
    public void OnPickUp()
    {
        if (mbIsCanPickUp == true)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            mbIsCanPickUp = true;
            m_PickUpText.gameObject.SetActive(mbIsCanPickUp);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            mbIsCanPickUp = false;
            m_PickUpText.gameObject.SetActive(mbIsCanPickUp);
        }
    }
}
