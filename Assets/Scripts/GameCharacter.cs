using UnityEngine;

[CreateAssetMenu(fileName = "New Game Characer", menuName = "Monopoly/Game Character")]
public class GameCharacter : ScriptableObject
{
    [SerializeField]
    private string characterName = string.Empty;
    [SerializeField]
    private Sprite icon = null;


    public string CharacterName => characterName;
    public Sprite Icon => icon;
}
