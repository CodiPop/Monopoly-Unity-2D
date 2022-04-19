using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public Vector2 position;
    private string name;
    private float price;
    private GameCharacter owner;
    private int houses;
    private int hotels;
    private string imgPath;
    private bool hasImage = false;

    public Card(Vector2 position, string name, float price,string imgPath = null)
    {
        this.position = position;
        this.name = name;
        this.price = price;
        this.owner = null;
        if(imgPath != null)
        {
            if (System.IO.File.Exists(Application.dataPath + imgPath))
            {
                this.hasImage = true;
            }
            
        }
        this.imgPath = imgPath;

    }

}
