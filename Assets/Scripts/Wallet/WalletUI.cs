using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private WalletManager _manager;

    public void Init(float startCount)
    {
        _manager = ServiceLocator.Instance.Get<WalletManager>();
        ChangeText(startCount);
        _manager.Test += ChangeText;
    }

    private void OnDestroy()
    {
        _manager.CountChanged -= ChangeText;
    }

    private void ChangeText(float number)
    {
        _text.text = number.ToString();
    }
}
