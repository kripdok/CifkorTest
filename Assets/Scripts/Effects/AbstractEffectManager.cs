using UnityEngine;

public abstract class AbstractEffectManager<T, Y> : MonoBehaviour, IService where T : AbstractPressingEffect where Y : AbstractObjectPool<T>
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected Transform Parent;

    protected Y ObjectPool;
}