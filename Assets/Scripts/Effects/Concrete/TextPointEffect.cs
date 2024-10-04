using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextPointEffect : AbstractPressingEffect
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _animationTime = 1;
    [SerializeField] private float _finalScale = 0.5f;
    [SerializeField] private float _distanceMultiplier = 2;

    public UnityAction<TextPointEffect> Worked;

    public void SetText(float number)
    {
        _text.text = number.ToString();
    }

    protected override async void PlayAnimation()
    {
        Sequence sequence = DOTween.Sequence();

        var vector = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        sequence.Append(transform.DOMove(vector.normalized * _distanceMultiplier, _animationTime))
            .Insert(0, transform.DOScale(_finalScale, _animationTime))
            .Insert(0, _text.DOFade(0, _animationTime));
        await sequence.AsyncWaitForCompletion();

        _text.alpha = 255;
        Worked?.Invoke(this);
    }
}