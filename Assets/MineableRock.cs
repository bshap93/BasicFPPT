using UnityEngine;

public class MineableRock : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        Destroy(gameObject);
        Debug.Log("Rock mined");
    }
}

public interface IInteractable
{
    void Interact();
}
