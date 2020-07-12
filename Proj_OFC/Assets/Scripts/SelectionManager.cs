using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    [SerializeField] private string selectableTag;

    private ISelectionResponse _selectionResponse;

    private InputHandler _inputHandler;

    private Camera _camera;

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        _selectionResponse = GetComponent<ISelectionResponse>();
        _inputHandler.pressDelegate += ButtonPressed;
        _camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Deselection Response
        if (_selectionResponse != null)
        {
            _selectionResponse.OnDeselect();
        }

        #region Selection Determination

        //Cast ray
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        //Selection determination
        _selectionResponse = null;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                _selectionResponse = selection.GetComponent<ISelectionResponse>();
            }
        }

        #endregion
        
        //Selection Response
        if (_selectionResponse != null)
        {
            _selectionResponse.OnSelect();
        }
        
    }

    void ButtonPressed()
    {
        if (_selectionResponse != null)
        {
            _selectionResponse.OnPress();
        }
    }

    public void SetTagString(string to)
    {
        selectableTag = to;
    }
}