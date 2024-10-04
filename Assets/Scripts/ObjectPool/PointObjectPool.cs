using UnityEngine;

public class PointObjectPool : AbstractObjectPool<TextPoint>
{
    public PointObjectPool(TextPoint prefab, int initialQuantity = 0, Transform parent = null) : base(prefab, initialQuantity, parent)
    {
    }

    protected override TextPoint Create()
    {
        var obj = base.Create();
        obj.Worked += Release;
        return obj;
    }
}
