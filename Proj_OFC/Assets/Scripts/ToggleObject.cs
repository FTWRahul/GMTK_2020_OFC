using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    [SerializeField] private GameObject target;

    public void Toggle()
    {
        target.SetActive(!target.activeSelf);
    }
}
