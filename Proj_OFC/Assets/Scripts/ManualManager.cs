using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ManualManager : MonoBehaviour , ISelectionResponse
{
    [SerializeField] private UnityEvent onPickUp;
    [SerializeField] private UnityEvent onDrop;
    [SerializeField] private string setTagTo;
    [SerializeField] private float launchForce;
    private Rigidbody _rigidbody;

    public Transform targetPosi;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnLockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LaunchManuel()
    {
        //_rigidbody.isKinematic = false;
        //_rigidbody.useGravity = true;
        //_rigidbody.AddForce(Camera.main.transform.forward * launchForce, ForceMode.Impulse);
        var endPosi = new Vector3(transform.localPosition.x, transform.localPosition.y - 10 , transform.localPosition.z + 5);
        transform.DOLocalJump(endPosi,
            launchForce, 1, 3f);
        Destroy(gameObject, 5f);
    }

    public void OnSelect()
    {
        
    }

    public void OnDeselect()
    {
        
    }

    public void OnPress()
    {
        onPickUp?.Invoke();
        transform.localPosition = targetPosi.localPosition;
    }

    public void DropManual()
    {
        onDrop?.Invoke();
    }
}
