using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotCaracterWiget : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _emptySlot;
    [SerializeField] private GameObject _infoCharSlot;
    
    [SerializeField] private TMP_Text _nameLabel;
    [SerializeField] private TMP_Text _levelLabel;
    [SerializeField] private TMP_Text _goldLabel;

    public Button SlotButton => _button; 

    public void ShowInfoCharSlot(string name, string level, string gold)
    {
        _nameLabel.text = name;
        _levelLabel.text = level;
        _goldLabel.text = gold;

        _infoCharSlot.SetActive(true);
        _emptySlot.SetActive(false);
    }

    public void ShowEmptySlot()
    {
        _infoCharSlot.SetActive(true);
        _emptySlot.SetActive(false);
    }
}
