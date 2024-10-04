using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour, IService
{
    [SerializeField] private PressMeButton _pressMeButton;
    [SerializeField] private Autoclicker _autoclickerPrefab;
    [SerializeField] private Transform _autoclickerParant;
    [SerializeField] private EnergyUI _energyUI;
    [SerializeField] private WalletUI _walletUI;

    private AutoclickerObjectPool _autoclickerObjectPool;
    private SettingsData _settingsData;
    private Player _player;

    public UnityAction<float> Action;

    public void Init()
    {
        _settingsData = ServiceLocator.Instance.Get<SettingsData>();
        _player = new Player(_energyUI, _walletUI);
        var amountOfMoneyPerTap = _settingsData.AmountOfMoneyPerTap;
        var requiredEnergyPerPress = _settingsData.RequiredEnergyPerTap;
        _pressMeButton.Init(delegate { ButtonClickedMethod(amountOfMoneyPerTap, requiredEnergyPerPress); });

        CreateAutoclickers();
    }

    private void CreateAutoclickers()
    {
        _autoclickerObjectPool = new AutoclickerObjectPool(_autoclickerPrefab, 0, _autoclickerParant);

        foreach (var autoclicker in _settingsData.Autoclickers)
        {
            var obj = _autoclickerObjectPool.GetObject();
            obj.InitInformation(autoclicker, delegate { ReactToReceivingValues(obj.Profit); });
        }
    }

    private async void ButtonClickedMethod(float moneyAmount, float energyAmount)
    {
        bool isTrue = await _player.TryChangeEnergyValue(energyAmount);
        if (isTrue)
            ReactToReceivingValues(moneyAmount);
    }

    private void ReactToReceivingValues(float amount)
    {
        Action?.Invoke(amount);
    }
}