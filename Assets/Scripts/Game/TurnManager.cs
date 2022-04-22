using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public int playerNumber;
    public int currentTurn;
    public Player[] players;
    private int numberOfDoubles = 0;
    

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerNumber = 1;
        players = new Player[playerNumber];
        players[0] = new Player();
        currentTurn = 1;
        Boton.Instance.turnOn();
    }


    // Helpers
    public void NextPlayer()
    {
        //currentTurn = (currentTurn == playerNumber) ? 1 : currentTurn + 1;
        //Boton.Instance.turnOn();
    }

    public void ManageTurn(int dice1,int dice2)
    {
        //Boton.Instance.turnOff();
        if (dice1 == dice2)
        {
            numberOfDoubles++;
        }
        else
        {
            numberOfDoubles = 0;
        }

        if(numberOfDoubles == 3)
        {
            numberOfDoubles = 0;
            BoardManager.Instance.SendToJail();
            NextPlayer();
            return;
        }

        BoardManager.Instance.Move(players[currentTurn-1], dice1 + dice2);    
    }


}
