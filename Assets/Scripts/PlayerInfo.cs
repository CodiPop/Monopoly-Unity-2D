[System.Serializable]
public class PlayerInfo
{
    public PlayerInfo(string nameText, GameCharacter character)
    {
        Name = nameText;
        Character = character;
    }

    public string Name { get; set; } = "No Name";
    public GameCharacter Character { get; set; } = null;
}
