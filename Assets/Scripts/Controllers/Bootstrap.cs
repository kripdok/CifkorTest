using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SettingsData _settingsData;
    [SerializeField] private Coroutines _coroutines;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextPointEffectManager _pointManager;


    private void Awake()
    {
        SetServiceLocalor();
        _gameManager.Init();
        _pointManager.Init();
    }

    private void SetServiceLocalor()
    {
        ServiceLocator.Init();
        ServiceLocator.Instance.Register(_settingsData);
        ServiceLocator.Instance.Register(_gameManager);
        ServiceLocator.Instance.Register(_pointManager);
        ServiceLocator.Instance.Register(_coroutines);
    }
}
