using UnityEngine;

public abstract class AbstractCreatedObject : MonoBehaviour
{
    protected Transform Parent;

    public void Init(Transform parent)
    {
        Parent = parent;
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
    }
}
