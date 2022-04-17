using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject boardImage;
    public TextAsset cardsInfo;
    public GameObject testCircle;
    public GameObject instance;

    private float width, height,cornerSize, cardWidthSize, cardHeightSize;
    private ArrayList cards;
    private int pointer;

    struct Card
    {
        public Vector2 position;
        string name;
        float price;
        GameCharacter owner;
        
        public Card(Vector2 position,string name, float price)
        {
            this.position = position;
            this.name = name;
            this.price = price;
            this.owner = null;
        }
    }

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
        instance.transform.position = new Vector3(c.position.x, c.position.y, -1);
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
        width = -1*((sprite.bounds.size.x)/2);
        height = -1 * ((sprite.bounds.size.y) / 2);

        cornerSize = (float)(width * 0.13);
        cardHeightSize = cornerSize;
        cardWidthSize = (float)(width * ((1 - (0.13*2))/9));
    }

    private void setRows()
    {
        cards = new ArrayList();
        // Set first row
        float half = height + (cardHeightSize / 2);
        cards.Add(new Card(
            new Vector2(height - half,width),
            "pepita puta",
            500)
        );
        for (int i = 0; i < 9; i++)
        {
            cards.Add(new Card(
               new Vector2(width-cornerSize-(i* cardWidthSize)-half, height-half),
               "pepita puta",
               500)
            );
        }
    }
}
