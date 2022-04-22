using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    public static Boton Instance;
    public Dice D1;
    public Dice D2;
    private bool BLOCKED;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    private void OnMouseDown()
    {
        if (!BLOCKED)
        {
            D1.RollTheDice();
            D2.RollTheDice();

            TurnManager.Instance.ManageTurn(D1.value, D2.value);
        }
    }

    public void turnOn()
    {
        BLOCKED = false;
    }

    public void turnOff()
    {
        BLOCKED = true;

    }

}
