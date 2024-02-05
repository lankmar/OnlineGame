using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MySlotCharWiget : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _emptySlot;
    [SerializeField] private GameObject _infoCharSlot;

    [SerializeField] private TMP_Text _damageLabel;
    [SerializeField] private TMP_Text _hpLabel;
    [SerializeField] private TMP_Text _expLabel;

    public Button SlotButton => _button;

    public void ShowInfoCharSlot(string damage, string hp, string expirience)
    {
        _damageLabel.text = $"damage {damage}";
        _hpLabel.text = $"hp {hp}";
        _expLabel.text = $"exp {expirience}";

        _infoCharSlot.SetActive(true);
        _emptySlot.SetActive(false);
    }

    public void ShowEmptySlot()
    {
        _infoCharSlot.SetActive(true);
        _emptySlot.SetActive(false);
    }
}


