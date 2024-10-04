using System.Threading.Tasks;

public class Player
{
    private WalletManager _walletManager;
    private EnergyManager _energyManager;

    public Player(EnergyUI energyUI, WalletUI walletUI)
    {
        _walletManager = new WalletManager(walletUI);
        _energyManager = new EnergyManager(energyUI);
    }

    public async Task<bool> TryChangeEnergyValue(float value)
    {
        return await _energyManager.TryChangeWallet(value);
    }
}
