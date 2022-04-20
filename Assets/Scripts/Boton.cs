using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    public Dice D1;
    public Dice D2;
    
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        D1.RollTheDice();
        D2.RollTheDice();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
