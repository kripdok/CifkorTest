using UnityEngine.Events;

public class TextPointEffectManager : AbstractEffectManager<TextPointEffect, PointObjectPool>
{
    private UnityAction<float> _action;

    public void Init(UnityAction<float> action)
    {
        ObjectPool = new PointObjectPool(Prefab, 0, Parent);
        _action = action;
        _action += ReacToAction;
    }

    private void OnDestroy()
    {
        _action -= ReacToAction;
    }

    public void ReacToAction(float pointNumber)
    {
        var obj = ObjectPool.GetObject();
        obj.SetText(pointNumber);
        obj.SetStartPositionAndStartRun();
    }
}