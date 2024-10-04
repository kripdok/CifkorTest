using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextPoint : AbstractCreatedObject
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _tweenDoration;

    public UnityAction<TextPoint> Worked;

    public void SetStartPositionAndStartRun(float pointNumber)
    {
        _text.text = pointNumber.ToString();
        transform.localPosition = Parent.position;
        Moving();
    }

    private async void Moving()
    {
        Sequence sequence = DOTween.Sequence();

        var ver = new Vector3(Random.Range(-10,10), Random.Range(-10, 10), 0);
        sequence.Append(transform.DOMove(ver.normalized * 2, _tweenDoration))
            .Insert(0, transform.DOScale(0.5f, _tweenDoration))
            .Insert(0, _text.DOFade(0, _tweenDoration));
        await sequence.AsyncWaitForCompletion();

        _text.alpha = 255;
        Worked?.Invoke(this);
    }
}
