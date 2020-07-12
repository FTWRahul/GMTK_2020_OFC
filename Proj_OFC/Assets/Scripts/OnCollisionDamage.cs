using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionDamage : MonoBehaviour, IDealDamage
{
    [SerializeField] private UnityEvent onCollision;
    public void DealDamage(ITakeHit to)
    {
        to.GetHit();
        onCollision?.Invoke();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.GetComponent<ITakeHit>() != null)
        {
            DealDamage(other.transform.GetComponent<ITakeHit>());
        }
    }
}
