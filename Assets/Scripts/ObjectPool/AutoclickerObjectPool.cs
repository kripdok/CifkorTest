using UnityEngine;

public class AutoclickerObjectPool : AbstractObjectPool<Autoclicker>
{
    public AutoclickerObjectPool(Autoclicker prefab, int initialQuantity = 0, Transform parent = null) : base(prefab, initialQuantity, parent)
    {
    }
}
