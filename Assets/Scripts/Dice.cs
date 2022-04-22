using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public int value;

	private void Start () {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
	}

    public void RollTheDice()
    {
        ArrayList values = new ArrayList();
        for (int i = 0; i <= 20; i++)
        {
            values.Add(Random.Range(0, 6));
        }
        StartCoroutine(RollDice(values));
        value = (int) values[20]+1;
    }
   
    IEnumerator RollDice(ArrayList values)
    {
        foreach (int value in values)
        {
            rend.sprite = diceSides[value];
            yield return new WaitForSeconds(0.02f);
        }
    }
}
