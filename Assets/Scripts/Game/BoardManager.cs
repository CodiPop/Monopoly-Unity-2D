using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public GameObject boardImage;
    public TextAsset cardsInfo;
    public GameObject testCircle;
    public Image CardImage;

    private float width, height,cornerSize, cardWidthSize, cardHeightSize;
    private ArrayList cards;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

        setBoardBounds();
        setRows();
        SetAnimationsAndUI();
        Boton.Instance.turnOn();
    }


    private void SetAnimationsAndUI()
    {
        CardImage.enabled = false;
        AnimationsManager.Instance.SetCardsInOriginalPosition();
    }



    // Board helpers

    private void setBoardBounds()
    {
        SpriteRenderer sprite = boardImage.GetComponent<SpriteRenderer>();
        width = sprite.bounds.size.x;
        height = sprite.bounds.size.y;

        cornerSize = (float)(width * 0.14);
        cardHeightSize = cornerSize;
        cardWidthSize = (float)(width * ((1 - 0.28)/9));
    }

    private void setRows()
    {

        Card SetInfoToCard(Vector2 pos, string[] info, byte row)
        {
            Card c;
            c = new Card(pos, info[0],row,price: float.Parse(info[1]), imgPath: "Images/"+info[2]);
            c.SetPropertyFields(float.Parse(info[3]),
                float.Parse(info[4]),
                float.Parse(info[5]),
                float.Parse(info[6]),
                float.Parse(info[7]),
                float.Parse(info[8]),
                float.Parse(info[9]));
            return c;
        }

        Card SetInfoEspecialCard(Vector2 pos, string[]info, byte row)
        {
            Card c;
            c = new Card(pos, Card.GetCardName(info[0]), row);
            return c;
        }

        string allInfo = cardsInfo.text;
        string[] cardsText = allInfo.Split(';');


        cards = new ArrayList();
        float halfWidth = (cardWidthSize / 2);
        float halfHeight = (cardHeightSize / 2);

        // Set first row
        cards.Add(new Card(new Vector2(width - halfHeight, halfHeight),"Go",1,isCorner: true));
        for (int i = 0; i < 9; i++)
        {
            string[] info = cardsText[i].Split(',');
            Vector2 pos = new Vector2(width - cornerSize - (i * cardWidthSize) - halfWidth, halfHeight);
            Card c = (info.Length == 10) ? SetInfoToCard(pos,info,1) : SetInfoEspecialCard(pos, info, 1);
            cards.Add(c);
        }

        // Set second row
        cards.Add(new Card(new Vector2(halfHeight, halfHeight), "Jail", 2, isCorner: true));
        for (int i = 8; i > 0; i--)
        {
            string[] info = cardsText[9 + (8 - i)].Split(',');
            Vector2 pos = new Vector2(halfHeight, height - (i * cardWidthSize) - cornerSize - halfWidth);
            Card c = (info.Length == 10) ? SetInfoToCard(pos, info, 1) : SetInfoEspecialCard(pos, info, 1);
            cards.Add(c);
        }

        // Set third row
        cards.Add(new Card(new Vector2(halfHeight, height - halfHeight), "Free Parking", 3, isCorner: true));
        for (int i = 8; i > 0; i--)
        {
            {
                string[] info = cardsText[18 + (8 - i)].Split(',');
                Vector2 pos = new Vector2(width - cornerSize - (i * cardWidthSize) - halfWidth, height - halfHeight);
                Card c = (info.Length == 10) ? SetInfoToCard(pos, info, 1) : SetInfoEspecialCard(pos, info, 1);
                cards.Add(c);
            }
        }

        // Set fourth row
        cards.Add(new Card(new Vector2(width - halfHeight, height - halfHeight), "Go to Jail", 3, isCorner: true));

        for (int i = 0; i < 9; i++)
        {
            {
                string[] info = cardsText[27 + (i)].Split(',');
                Vector2 pos = new Vector2(width - halfHeight, height - (i * cardWidthSize) - cornerSize - halfWidth);
                Card c = (info.Length == 10) ? SetInfoToCard(pos, info, 1) : SetInfoEspecialCard(pos, info, 1);
                cards.Add(c);
            }
        }
    }
    
    // Movement to places

    public void SendToJail()
    {

    }

    private ArrayList GetPath(int current, int next)
    {
        int diff = next - current;

        ArrayList mov = new ArrayList();
        if (diff <= 0)
        {
            for (int i = current + 1; i < cards.Count; i++)
            {
                Card c = (Card)cards[i];
                if (c.IsCorner())
                {
                    mov.Add(c);
                }
            }
            current = 0;
        }
        else
        {
            current = current + 1;
        }

        for (int i = current; i <= next; i++)
        {
            Card c = (Card)cards[i];

            if (i == next)
            {
                mov.Add(c);
                continue;
            }else if (c.IsCorner())
            {
                mov.Add(c);
            }
        }

        
        return mov;
    }
    public void Move(Player p,int number)
    {
        int current = p.currentIndex;
        int next = (current + number) % cards.Count;
        AnimationsManager.Instance.MovePiece(p, GetPath(current, next));

        Card nextCard = (Card)cards[next];
        p.currentIndex = next;

        StartCoroutine(SetImageAfterDice(nextCard));
   
        
        

    }

    IEnumerator SetImageAfterDice(Card c)
    {
        yield return new  WaitForSeconds(2.0F);

        if (c.hasImage)
        {
            Sprite img = Resources.Load<Sprite>(c.imgPath);
            CardImage.sprite = img;
        }
        CardImage.enabled = (c.hasImage);

    }

}
