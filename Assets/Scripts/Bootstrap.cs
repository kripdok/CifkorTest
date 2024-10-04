using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private PressMeButton _pressMeButton;
    [SerializeField] private Coroutines _coroutines;
    [SerializeField] private WalletManager _walletManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private EnergyManager _energyManager;
    [SerializeField] private PointManager _pointManager;

    private void Awake()
    {
        ServiceLocator.Init();

        ServiceLocator.Instance.Register(_walletManager);
        ServiceLocator.Instance.Register(_gameData);
        ServiceLocator.Instance.Register(_playerManager);
        ServiceLocator.Instance.Register(_energyManager);
        ServiceLocator.Instance.Register(_pointManager);
        ServiceLocator.Instance.Register(_coroutines);

        _walletManager.Init();
        _playerManager.Init();
        _energyManager.Init(200);
        _pointManager.Init();
    }
}
