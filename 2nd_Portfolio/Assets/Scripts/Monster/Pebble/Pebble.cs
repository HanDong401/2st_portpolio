using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pebble : Monster
{
    Coroutine m_Coroutine;

    public override void SubAwake()
    {

    }

    public override void Attack1()
    {
        m_Coroutine = StartCoroutine(Attack1Coroutine());
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            if (m_Coroutine != null)
                StopCoroutine(m_Coroutine);
            Target.OnDamage(Attack1Damage);
            Target.Knockback(transform.position, Attack1Damage);
            StartCoroutine(Attack1DelayCoroutine());
        }
    }

    IEnumerator Attack1Coroutine()
    {
        float count = 0f;
        while (count < 1f)
        {
            transform.position = Vector2.Lerp(transform.position, Target.GetPosition(), Time.deltaTime);
            count += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator Attack1DelayCoroutine()
    {
        Collider.enabled = false;
        yield return new WaitForSeconds(Attack1Delay);
        ChangeState("Idle");
    }

    public override bool SubCheckState()
    {
        return false;
    }

    public override void Attack2()
    {
        
    }

    public override void Attack3()
    {
        
    }

    public override void Ability()
    {
        
    }
}
