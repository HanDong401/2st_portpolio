using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtcItem : MonoBehaviour
{
    protected bool mbIsCanPick = false;
    public bool IsCanPick { get { return mbIsCanPick; } set { mbIsCanPick = value; } }
    protected Player m_Player = null;
    public Player Player { get { return m_Player; } set { m_Player = value; } }

    protected virtual void EtcItemEffect() { }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (mbIsCanPick.Equals(true) && coll.gameObject.CompareTag("Player"))
        {
            EtcItemEffect();
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}
