using UnityEngine;

public abstract class Command : MonoBehaviour
{
    protected CommandManager commandManager => GameManager.Instance.commandManager;
    protected virtual void OnPress()
    {
        //GameManager.Instance.commandManager
    }
}
