using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void RotationDelegate(Vector3 rotation);
    public delegate void ButtonPressDelegate();
    
    public RotationDelegate rotationDelegate;
    public ButtonPressDelegate pressDelegate;
    
    // Update is called once per frame
    void Update()
    {
        rotationDelegate?.Invoke(new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0));

        if (Input.GetMouseButtonDown(0))
        {
            pressDelegate?.Invoke();
        }
    }
}
