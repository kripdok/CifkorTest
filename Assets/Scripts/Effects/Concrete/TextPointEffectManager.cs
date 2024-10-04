public class TextPointEffectManager : AbstractEffectManager<TextPointEffect, PointObjectPool>
{
    private GameManager _gameManager;

    public void Init()
    {
        _gameManager = ServiceLocator.Instance.Get<GameManager>();
        ObjectPool = new PointObjectPool(Prefab, 0, Parent);
        _gameManager.Action += ReacToAction;
    }

    private void OnDestroy()
    {
        _gameManager.Action -= ReacToAction;
    }

    private void ReacToAction(float pointNumber)
    {
        var obj = ObjectPool.GetObject();
        obj.SetText(pointNumber);
        obj.SetStartPositionAndStartRun();
    }
}