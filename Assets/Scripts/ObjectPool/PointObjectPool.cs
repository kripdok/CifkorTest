using UnityEngine;

public class PointObjectPool : AbstractObjectPool<TextPointEffect>
{
    public PointObjectPool(TextPointEffect prefab, int initialQuantity = 0, Transform parent = null) : base(prefab, initialQuantity, parent)
    {
    }

    protected override TextPointEffect Create()
    {
        var obj = base.Create();
        obj.Worked += Release;
        return obj;
    }
}
