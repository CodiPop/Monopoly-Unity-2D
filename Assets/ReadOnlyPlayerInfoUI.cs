using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReadOnlyPlayerInfoUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text playerNameText = null;
    [SerializeField]
    private TMP_Text characterNameText = null;
    [SerializeField]
    private Image characterIconImage = null;

    public string PlayerName
    {
        get => playerNameText.text;
    }

    public string CharacterName => characterNameText.text;
    public Sprite CharacterSprite => characterIconImage.sprite;

    public GameCharacter Character { get; private set; }


    public void SetUp(string playerName, GameCharacter character)
    {
        playerNameText.text = playerName;
        characterNameText.text = character.CharacterName;
        characterIconImage.sprite = character.Icon;

        Character = character;
    }
}
