using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour, IService
{
    [SerializeField] private TextPoint _pointPrefab;
    [SerializeField] private Transform _pointParant;

    private PointObjectPool _pointObjectPool;

    public void Init()
    {
        _pointObjectPool = new PointObjectPool(_pointPrefab, 1, _pointParant);
    }

    public void OnClick(float pointNumber)
    {
        var obj = _pointObjectPool.GetObject();
        obj.SetStartPositionAndStartRun(pointNumber);
    }
}
