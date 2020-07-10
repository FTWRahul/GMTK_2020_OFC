using UnityEngine;

public interface ISelectionResponse
{ 
    void OnSelect();
    void OnDeselect();

    void OnPress();
}