using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationsManager : MonoBehaviour
{
    public static AnimationsManager Instance;

    public GameObject P1_Card, P2_Card, P3_Card, P4_Card;
    private ArrayList PlayerCards;
    public int playerNumber;

    // Control variables

    private float CARD_SIZE_VALUE;



    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PlayerCards = new ArrayList();
        CARD_SIZE_VALUE = P1_Card.GetComponent<RectTransform>().rect.width;

        PlayerCards.Add(P1_Card);
        PlayerCards.Add(P2_Card);
        PlayerCards.Add(P3_Card);
        PlayerCards.Add(P4_Card);
        foreach (GameObject item in PlayerCards)
        {
            Transform t = item.transform;
            item.transform.DOMoveX(t.position.x+CARD_SIZE_VALUE*0.7f, 0.0f);
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCardsInOriginalPosition()
    {
        for (int i = 0; i < playerNumber; i++)
        {
            GameObject go = (GameObject) PlayerCards[i];
            go.SetActive(true);
            go.transform.DOMoveX(go.transform.position.x - CARD_SIZE_VALUE * 0.7f, 1.0f);

        }
    }
}
