using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PressMeButton : MonoBehaviour
{
    private Button _button;

    public void Init(UnityAction action)
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(action);
    }

    public void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
