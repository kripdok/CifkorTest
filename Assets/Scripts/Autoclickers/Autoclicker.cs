using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Autoclicker : AbstractCreatedObject
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _priceNumberText;
    [SerializeField] private TMP_Text _countNumberText;
    [SerializeField] private TMP_Text _profitNumberText;
    [SerializeField] private Image _loadBar;
    [SerializeField] private Image _bacgroundImage;
    [SerializeField] private Image _errorImage;
    [SerializeField] private Button _button;
    [SerializeField] private float _errorDuration = 0.5f;

    public float Profit => _count * _amountOfMoney;

    private Tween tween;
    private UnityAction _loaded;
    private WalletManager _walletManager;
    private float _price;
    private float _count;
    private float _rollbackTime;
    private float _costMultiplier;
    private float _amountOfMoney;
    private bool _isStartWork;

    private void OnDestroy()
    {
        if (tween != null)
            tween.Kill();

        _button.onClick.RemoveListener(TryBuyNewCopy);
    }

    public void InitInformation(AutoclickerInfo info,UnityAction action)
    {
        _loaded = action;
        _walletManager = ServiceLocator.Instance.Get<WalletManager>();
        _button.onClick.AddListener(TryBuyNewCopy);
        SetStartInformation(info);
        SetText();
    }

    private void SetStartInformation(AutoclickerInfo info)
    {
        _nameText.text = info.Name;
        _amountOfMoney = info.AmountOfMoney;
        _price = info.CostPerUnit;
        _rollbackTime = info.RollbackTimeInSeconds;
        _costMultiplier = info.CostMultiplier;
        _isStartWork = false;
        _errorImage.gameObject.SetActive(false);
        _count = 0;
        _loadBar.fillAmount = 0;
    }

    private void SetText()
    {
        _priceNumberText.text = _price.ToString();
        _countNumberText.text = _count.ToString();
        _profitNumberText.text = Profit.ToString();
    }

    private IEnumerator StartingWork()
    {
        while (true)
        {
            tween = _loadBar.DOFillAmount(1, _rollbackTime);
            yield return tween.WaitForCompletion();
            _loadBar.fillAmount = 0;
            _loaded?.Invoke();
        }
    }

    private async void TryBuyNewCopy()
    {
        bool isSuccessful = await _walletManager.TryChangeWallet(_price);

        if (!isSuccessful)
        {
            await ReactToPurchaseError();
        }
        else
        {
            Enlarge—opies();
        }
    }

    private async Task ReactToPurchaseError()
    {
        _errorImage.gameObject.SetActive(true);
        _button.interactable = false;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(_errorImage.DOFillAmount(0, _errorDuration))
            .Insert(0, _bacgroundImage.transform.DOPunchPosition(Vector3.one, _errorDuration));

        await sequence.AsyncWaitForCompletion();
        _errorImage.gameObject.SetActive(false);
        _errorImage.fillAmount = 1;
        _button.interactable = true;
    }

    private void Enlarge—opies()
    {
        _count++;
        _price *= _costMultiplier;
        SetText();
        TryStartWork();
    }

    private void TryStartWork()
    {
        if (!_isStartWork)
        {
            _isStartWork = true;
            StartCoroutine(StartingWork());
        }
    }
}
