using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardManager : MonoBehaviour
{
    public GameObject boardImage;
    public TextAsset cardsInfo;
    public GameObject testCircle;
    public GameObject instance;

    private float width, height,cornerSize, cardWidthSize, cardHeightSize;
    private ArrayList cards;
    private int pointer;

    void Start()
    {
        setBoardBounds();
        setRows();

        SetAnimationsAndUI();
        pointer = 0;
        testCircle.transform.DOScale(new Vector3())
        instance = Instantiate(testCircle, new Vector3(0, 0, -1), Quaternion.identity);
        
        StartCoroutine(ExampleCoroutine());
    }

    void Update()
    {
       
    }

    


    IEnumerator ExampleCoroutine()
    {
        Card c = getNext();
        instance.transform.DOJump(new Vector3(c.position.x, c.position.y, -1),1.0f,2,duration: 1.0f);
        yield return new WaitForSeconds(3);
        StartCoroutine(ExampleCoroutine());
    }

    private Card getNext()
    {
        Card c = (Card)cards[pointer];
        pointer = (pointer+1) % cards.Count;
        return c;
    }


    private void SetAnimationsAndUI()
    {
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
        cards.Add(new Card(
            new Vector2(halfHeight, halfHeight),
            "pepita puta",
            2,
            500)
        );
        for (int i = 9; i > 0; i--)
        {
            cards.Add(new Card(
               new Vector2(halfHeight,height-(i*cardWidthSize)-cornerSize-halfWidth),
               "pepita puta",
               2,
               500)
            );
        }

        // Set third row
        cards.Add(new Card(
            new Vector2(halfHeight, height - halfHeight),
            "pepita puta",
            3,
            500)
        );
        for (int i = 9; i > 0; i--)
        {
            cards.Add(new Card(
               new Vector2(width - cornerSize - (i * cardWidthSize) - halfWidth, height-halfHeight),
               "pepita puta",
               3,
               500)
            );
        }

        // Set fourth row
        cards.Add(new Card(
            new Vector2(width - halfHeight, height - halfHeight),
            "pepita puta",
            4,
            500)
        );
        for (int i = 0; i < 9; i++)
        {
            cards.Add(new Card(
               new Vector2(width - halfHeight, height - (i * cardWidthSize) - cornerSize - halfWidth),
               "pepita puta",
               4,
               500)
            );
        }
    }
}
