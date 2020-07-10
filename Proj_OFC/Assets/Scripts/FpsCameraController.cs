using UnityEditor;
using UnityEngine;

public class FpsCameraController : MonoBehaviour
{
    [SerializeField] private float xRotMinMax;
    [SerializeField] private float yRotMinMax;
    
    private InputHandler _inputHandler;
    
    private float _xRot = 0;
    private float _yRot = 0;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _inputHandler = GetComponentInParent<InputHandler>();
        _inputHandler.rotationDelegate += Rotate;
    }

    private void Rotate(Vector3 dir)
    {
        _xRot -= dir.y * GameManager.Instance.mouseSensitivity * Time.deltaTime;
        _xRot = Mathf.Clamp(_xRot, -xRotMinMax, xRotMinMax);
        _yRot += dir.x * GameManager.Instance.mouseSensitivity * Time.deltaTime;
        _yRot = Mathf.Clamp(_yRot, -yRotMinMax, yRotMinMax);
        transform.localRotation = Quaternion.Euler(_xRot,_yRot,0);
        
    }
}
