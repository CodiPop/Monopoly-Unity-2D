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
        pointer = 0;
        instance = Instantiate(testCircle, new Vector3(0, 0, -1), Quaternion.identity);
        
        StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    


    IEnumerator ExampleCoroutine()
    {
        Card c = getNext();
        instance.transform.DOJump(new Vector3(c.position.x, c.position.y, -1),1.0f,2,duration: 1.0f);
        yield return new WaitForSeconds(1);
        StartCoroutine(ExampleCoroutine());
    }

    private Card getNext()
    {
        Card c = (Card)cards[pointer];
        pointer = (pointer+1) % cards.Count;
        return c;
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
        cards = new ArrayList();
        // Set first row
        float halfWidth = (cardWidthSize / 2);
        float halfHeight = (cardHeightSize / 2);
        cards.Add(new Card(
            new Vector2(width - halfHeight, halfHeight),
            "pepita puta",
            500)
        );
        for (int i = 0; i < 9; i++)
        {
            cards.Add(new Card(
               new Vector2(width-cornerSize-(i* cardWidthSize)-halfWidth, halfHeight),
               "pepita puta",
               500, $"/Images/Row1/{i+1}.png")
            );
        }

        // Set second row
        cards.Add(new Card(
            new Vector2(halfHeight, halfHeight),
            "pepita puta",
            500)
        );
        for (int i = 9; i > 0; i--)
        {
            cards.Add(new Card(
               new Vector2(halfHeight,height-(i*cardWidthSize)-cornerSize-halfWidth),
               "pepita puta",
               500)
            );
        }

        // Set third row
        cards.Add(new Card(
            new Vector2(halfHeight, height - halfHeight),
            "pepita puta",
            500)
        );
        for (int i = 9; i > 0; i--)
        {
            cards.Add(new Card(
               new Vector2(width - cornerSize - (i * cardWidthSize) - halfWidth, height-halfHeight),
               "pepita puta",
               500)
            );
        }

        // Set fourth row
        cards.Add(new Card(
            new Vector2(width - halfHeight, height - halfHeight),
            "pepita puta",
            500)
        );
        for (int i = 0; i < 9; i++)
        {
            cards.Add(new Card(
               new Vector2(width - halfHeight, height - (i * cardWidthSize) - cornerSize - halfWidth),
               "pepita puta",
               500)
            );
        }
    }
}
