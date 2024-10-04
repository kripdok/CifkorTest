using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbstractObjectPool<T> where T : AbstractCreatedObject
{
    private T _prefab;
    private List<T> _objects;
    protected Transform Parent;

    public AbstractObjectPool(T prefab, int initialQuantity = 0, Transform parent = null)
    {
        Parent = parent;
        _prefab = prefab;
        _objects = new List<T>(initialQuantity);

        for (int i = 0; i < _objects.Count; i++)
            Create();
    }

    public virtual T GetObject()
    {
         var obj = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);

        if (obj == null)
            obj = Create();

        obj.gameObject.SetActive(true);

        return obj;
    }

    public virtual void Release(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    protected virtual T Create()
    {
        var obj = GameObject.Instantiate(_prefab);
        _objects.Add(obj);
        obj.Init(Parent);
        return obj;
    }
}
