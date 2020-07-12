using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class HitResponder : MonoBehaviour, ITakeHit
{
    private Rigidbody _rb;
    [SerializeField] private GameObject particleSystem;

    [SerializeField] private UnityEvent onHit;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        //_rb.isKinematic = true;
        _rb.useGravity = false;
    }

    private void Start()
    {
        Register();
    }


    [ContextMenu("Test Hit")]
    public void GetHit()
    {
        StartCoroutine(DeathSequence());
        onHit?.Invoke();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Register()
    {
        GameManager.Instance.damageAbles.Add(this);
    }

    public IEnumerator DeathSequence()
    {
        // rb.isKinematic = false;
        // rb.useGravity = true;
        yield return new WaitForSeconds(2f);
        GameObject g0 = Instantiate(particleSystem, transform.position, Quaternion.identity);
        Destroy(g0, 5f);
        Destroy(gameObject,.2f);
    }

    [ContextMenu("Test Torque")]
    public void AddRandomTorque()
    {
        //rb.AddTorque(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
        _rb.AddForce(transform.up * 100, ForceMode.Impulse);
        _rb.AddTorque(new Vector3(Random.Range(0,1000), Random.Range(0,1000), Random.Range(0,1000)), ForceMode.Impulse);
    }

    public void ShakeCam(float amount)
    {
        GameManager.Instance.CameraShake.Trauma = amount;
    }
}
