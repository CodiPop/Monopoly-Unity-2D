using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{

    public Vector2 position;
    public string name;
    public GameCharacter owner;

    // General values
    public string imgPath;
    public bool hasImage = false;
    public bool isMortaged = false;
    private bool isCorner;
    public byte row;

    // Other values
    public bool isService = false;
    public bool isTax = false;
    public bool isLuxuryTax = false;
    public bool isTransport = false;
    public bool isLuck = false;
    public bool isCommunity = false;

    // Property values
    public byte houses = 0;
    public byte hotels = 0;
    public float price;
    public float rent;
    public float rent1house;
    public float rent2house;
    public float rent3house;
    public float rent4house;
    public float rentHotel;
    public float mortgageValue;

    public Card(Vector2 position, string name, byte row, float price = 0,string imgPath = null, bool isCorner = false)
    {
        this.position = position;
        this.name = name;
        this.price = price;
        this.owner = null;
        this.row = row;
        if(imgPath != null)
        {
            this.hasImage = true;
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
