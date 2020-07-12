using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRockets : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float force;
    [SerializeField] private float rotForce;
    private Rigidbody _rb;

    [SerializeField] private GameObject particlePrefab;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.damageAbles.Count < 1)
        {
            return;
        }
        var closestBoi = GameManager.Instance.damageAbles[0];
        foreach (var boi in GameManager.Instance.damageAbles)
        {
            if (boi != null)
            {
                var currentDist = (transform.position - boi.GetTransform().position).magnitude;
                var previousDist = (transform.position - closestBoi.GetTransform().position).magnitude;
                if (currentDist < previousDist)
                {
                    closestBoi = boi;
                    _target = closestBoi.GetTransform();
                }
            }
        }

        Vector3 direction = _target.position - transform.position;
        direction.Normalize();
        Vector3 rotAmount = Vector3.Cross(transform.forward, direction);
        _rb.angularVelocity = rotAmount * rotForce;
        _rb.velocity = transform.forward * force;

    }

    public void Explode()
    {
        GameObject go = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Destroy(go, 5f);
        Destroy(gameObject, .1f);
    }
}
