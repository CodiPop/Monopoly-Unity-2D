using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField playerNameInput = null;
    [SerializeField]
    private TMP_Dropdown characterDropdown = null;
    [SerializeField]
    private Image characterIconImage = null;

    public string NameText => playerNameInput.text;
    public Sprite Icon => characterDropdown.options[SelectedCharacter].image;
    public int SelectedCharacter => characterDropdown.value;


    private void Awake()
    {
        characterDropdown.onValueChanged.AddListener(OnCharacterDropdown_ValueChanged);
    }

    private void Start()
    {
        playerNameInput.text = "Player-" + Random.Range(0, 100000);
    }

    public void SetCharacterDropdownOptions(List<TMP_Dropdown.OptionData> options)
    {
        characterDropdown.ClearOptions();
        characterDropdown.AddOptions(options);
        OnCharacterDropdown_ValueChanged(0);
    }

    public void OnCharacterDropdown_ValueChanged(int index)
    {
        characterIconImage.sprite = characterDropdown.options[index].image;
        print(characterDropdown.value);
    }
}
