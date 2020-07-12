using UnityEngine;

public interface ISelectionResponse
{ 
    void OnSelect();
    void OnDeselect();

    void OnPress();
}

public interface ITakeHit
{
    void GetHit();
    Transform GetTransform();

    void Register();
    void DeRegister();
}

public interface IDealDamage
{
    void DealDamage(ITakeHit to);
}