using UnityEngine;

public class PlayerManager : MonoBehaviour, IService
{
    [SerializeField] private PressMeButton _pressMeButton;
    [SerializeField] private Autoclicker _autoclickerPrefab;
    [SerializeField] private Transform _autoclickerParant;

    private float _amountOfMoneyPerTap;
    private float _requiredEnergyPerPress;
    private WalletManager _walletManager;
    private EnergyManager _energyManager;
    private TextPointEffectManager _pointManager;
    private AutoclickerObjectPool _autoclickerObjectPool;

    public void Init()
    {
        var gameData = ServiceLocator.Instance.Get<GameData>();
        _walletManager = ServiceLocator.Instance.Get<WalletManager>();
        _energyManager = ServiceLocator.Instance.Get<EnergyManager>();
        _pointManager = ServiceLocator.Instance.Get<TextPointEffectManager>();

        _amountOfMoneyPerTap = gameData.AmountOfMoneyPerTap;
        _requiredEnergyPerPress = gameData.RequiredEnergyPerPress;

        _pressMeButton.Init(delegate { ButtonClickedMethod(_amountOfMoneyPerTap); });


        _autoclickerObjectPool = new AutoclickerObjectPool(_autoclickerPrefab, 0, _autoclickerParant);


        foreach (var autoclicker in gameData.Autoclickers)
        {
            var obj = _autoclickerObjectPool.GetObject();
            obj.InitInformation(autoclicker, delegate { ButtonClicked(obj.Profit); });
        }

    }

    private async void ButtonClickedMethod(float count)
    {
        bool sum = await _energyManager.TryChangeWallet(count);

        if (sum) 
        {
            ButtonClicked(count);
        }
        
    }

    private void ButtonClicked(float amount) 
    {
        _walletManager.AddMoneyBecomClickOrAutoclicker(amount);
        _pointManager.ReacToAction(amount);
    }
}
