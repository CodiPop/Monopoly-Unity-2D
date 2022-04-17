using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeUIController : MonoBehaviour
{
    [SerializeField]
    private GameCharacter[] gameCharacters = null;

    [SerializeField]
    private GameObject homePanel = null;
    [Header("Single Player")]
    [SerializeField]
    private GameObject singlePlayerPanel = null;
    [SerializeField]
    private PlayerInfoUI singlePlayerInfoUI = null;
    [Header("Multiplayer")]
    [SerializeField]
    private GameObject multiplayerPanel = null;
    [SerializeField]
    private PlayerInfoUI multiplayerPlayerInfoUI = null;
    [SerializeField]
    private TMP_InputField multiplayerJoinGameInput = null;
    [Header("MP Lobby")]
    [SerializeField]
    private GameObject multiplayerLobby = null;
    [SerializeField]
    private TMP_Text gameCodeText = null;
    [SerializeField]
    private ReadOnlyPlayerInfoUI readOnlyPlayerInfoUIPrefab = null;
    [SerializeField]
    private ReadOnlyPlayerInfoUI readOnlyLocalPlayerInfo = null;
    [SerializeField]
    private Transform remotePlayersHolder = null;
    [SerializeField]
    private Button startGameMPButton = null;


    private List<string> charactersInUse = new List<string>();

    private List<PlayerInfo> AllPlayers = new List<PlayerInfo>();
    private bool IamHost { get; set; }


    private void Awake()
    {
        ShowHomePanel();
    }


    private IEnumerable<GameCharacter> GetAvailableCharacters()
    {
        return gameCharacters.Where(g => !charactersInUse.Contains(g.CharacterName));
    }

    private List<TMP_Dropdown.OptionData> GetAvailableDropdownOptions()
    {
        return GetAvailableCharacters().Select(g => new TMP_Dropdown.OptionData(g.CharacterName, g.Icon)).ToList();
    }

    public void ShowHomePanel()
    {
        singlePlayerPanel.SetActive(false);
        multiplayerPanel.SetActive(false);
        multiplayerLobby.SetActive(false);
        homePanel.SetActive(true);
    }

    public void OnSinglePlayer_ButtonClick()
    {
        homePanel.SetActive(false);
        multiplayerPanel.SetActive(false);
        multiplayerLobby.SetActive(false);
        singlePlayerInfoUI.SetCharacterDropdownOptions(GetAvailableDropdownOptions());
        singlePlayerPanel.SetActive(true);
    }

    public void OnStartGameSinglePlayer_ButtonClick()
    {
        PlayerInfo player = new PlayerInfo(singlePlayerInfoUI.NameText, gameCharacters[singlePlayerInfoUI.SelectedCharacter]);
        AllPlayers.Add(player);
        //Start Game Logic
    }

    public void OnMultiplayer_ButtonClick()
    {
        singlePlayerPanel.SetActive(false);
        homePanel.SetActive(false);
        multiplayerPlayerInfoUI.SetCharacterDropdownOptions(GetAvailableDropdownOptions());
        multiplayerPanel.SetActive(true);
    }

    public void OnJoinGame_ButtonClick()
    {
        string code = multiplayerJoinGameInput.text.Trim();
        
        if (string.IsNullOrEmpty(code)) return;

        List<PlayerInfo> existingRemotePlayers = new List<PlayerInfo>();

        //Get Data from server
        //Get remote players if exist

        PlayerInfo localPlayerInfo = new PlayerInfo(multiplayerPlayerInfoUI.NameText, gameCharacters[multiplayerPlayerInfoUI.SelectedCharacter]);
        bool isHost = false;
        ShowLobby(isHost, localPlayerInfo, existingRemotePlayers, code);
    }

    public void OnCreateGame_ButtonClick()
    {
        //Generate new code
        string code = Random.Range(1, 1000000).ToString("0000000");

        //Create game instance on server

        PlayerInfo localPlayerInfo = new PlayerInfo(multiplayerPlayerInfoUI.NameText, gameCharacters[multiplayerPlayerInfoUI.SelectedCharacter]);
        bool isHost = true;
        ShowLobby(isHost, localPlayerInfo, new List<PlayerInfo>(), code);
    }

    private void ShowLobby(bool isHost, PlayerInfo localPlayerInfo, IEnumerable<PlayerInfo> remotePlayers, string code)
    {
        IamHost = isHost;
        readOnlyLocalPlayerInfo.SetUp(localPlayerInfo.Name, localPlayerInfo.Character);
        gameCodeText.text = $"Game Code: {code}";


        multiplayerPanel.SetActive(false);
        multiplayerLobby.SetActive(true);

        AllPlayers.Add(localPlayerInfo);
        foreach (PlayerInfo remotePlayerInfo in remotePlayers)
        {
            AddRemotePlayer(remotePlayerInfo);
        }

        startGameMPButton.interactable = IamHost && AllPlayers.Count == 4;
    }

    /// <summary>
    /// Llamar este metodo para agregar instancias de los jugadores remotos al lobby del cliente
    /// </summary>
    /// <param name="playerInfo"></param>
    public void AddRemotePlayer(PlayerInfo playerInfo)
    {
        if (AllPlayers.Count == 4) return;

        AllPlayers.Add(playerInfo);

        ReadOnlyPlayerInfoUI roPlayerInfoUI = Instantiate(readOnlyPlayerInfoUIPrefab, remotePlayersHolder);
        roPlayerInfoUI.SetUp(playerInfo.Name, playerInfo.Character);

        startGameMPButton.interactable = IamHost && AllPlayers.Count == 4;
    }

    [ContextMenu("Add Remote Player")]
    public void UnitTestAddRemotePlayer()
    {
        AddRemotePlayer(new PlayerInfo("Test", gameCharacters[1]));
    }

    public void LeaveLobby_ButtonClick()
    {
        IamHost = false;
        AllPlayers.Clear();
        for (int i = 0; i < remotePlayersHolder.childCount; i++)
        {
            Destroy(remotePlayersHolder.GetChild(i).gameObject);
        }
        multiplayerLobby.SetActive(false);
        multiplayerPanel.SetActive(true);
    }

    public void StartMPGame_ButtonClick()
    {
        print("Start Game");
        //Start game
    }


    public void OnExit_ButtonClick()
    {
        Application.Quit();
    }
}
