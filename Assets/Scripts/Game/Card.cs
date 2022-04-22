using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{

    public Vector2 position;
    private string name;
    private GameCharacter owner;

    // General values
    private string imgPath;
    private bool hasImage = false;
    private bool isMortaged = false;
    private bool isCorner;
    private byte row;

    // Other values
    private bool isService = false;
    private bool isTax = false;
    private bool isLuxuryTax = false;
    private bool isTransport = false;
    private bool isLuck = false;
    private bool isCommunity = false;

    // Property values
    private byte houses = 0;
    private byte hotels = 0;
    private float price;
    private float rent;
    private float rent1house;
    private float rent2house;
    private float rent3house;
    private float rent4house;
    private float rentHotel;
    private float mortgageValue;

    public Card(Vector2 position, string name, byte row, float price = 0,string imgPath = null, bool isCorner = false)
    {
        this.position = position;
        this.name = name;
        this.price = price;
        this.owner = null;
        this.row = row;
        if(imgPath != null)
        {
            if (System.IO.File.Exists(Application.dataPath + imgPath))
            {
                this.hasImage = true;
            }
            
        }
        this.imgPath = imgPath;
        this.isCorner = isCorner;
    }

    public bool IsCorner()
    {
        return isCorner;
    }

    public void SetPropertyFields(float rent, float rent1house,float rent2house,float rent3house, float rent4house, float rentHotel, float mortgageValue)
    {
        this.rent = rent;
        this.rent1house = rent1house;
        this.rent2house = rent2house;
        this.rent3house = rent3house;
        this.rent4house = rent4house;
        this.rentHotel = rentHotel;
        this.mortgageValue = mortgageValue;
    }

    public float GetRentValue()
    {
        if (houses == 0)
        {
            return this.rent;
        }
        return 0;
    }

    public void SetOtherValues()
    {
        if (string.Equals(this.name, "Camara de Comercio"))
        {
            this.isCommunity = true;

        }
        else if(string.Equals(this.name, "Chance"))
        {
            this.isLuck = true;

        }
        else if (string.Equals(this.name, "Impuesto de Ingreso"))
        {
            this.isTax = true;
        }
        else if (string.Equals(this.name, "Aire") || string.Equals(this.name, "Triple A"))
        {
            this.isService = true;
        }
        else if (string.Equals(this.name, "Impuesto de Lujo"))
        {
            this.isLuxuryTax = true;
        }

    }

    public static string GetCardName(string code)
    {
        switch (code)
        {
            case "CAMARA": return "Camara de Comercio";
            case "CHANCE": return "Chance";
            case "IMPUESTOS": return "Impuesto de Ingreso";
            case "AIRE": return "Aire";
            case "TRIPLEA": return "Triple A";
            case "IMPUESTOLUJO": return "Impuesto de Lujo";
            default:
                return "nullcode";
        }
    }
}
