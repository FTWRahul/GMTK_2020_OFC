using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinamaticToggle : MonoBehaviour
{
    [SerializeField] private float delay;
    private void Start()
    {
        Invoke(nameof(ToggleWithDelay), delay);
    }

    public void ToggleWithDelay()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
