using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HoverSine : MonoBehaviour
{
    private Vector3 _originalPosition;
    [SerializeField] private float amplitude = .5f;
    [SerializeField] private float frequency = 1f;

    private float _rand;


    private void Awake()
    {
        _originalPosition = transform.position;
        _rand = Random.Range(0.5f, 1.2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position += Vector3.up * Random.Range(-.5f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = _originalPosition + Vector3.up * Mathf.Pow(Mathf.Sin(Time.time * frequency * _rand) * 
                                                                             amplitude , 2);
    }
}
