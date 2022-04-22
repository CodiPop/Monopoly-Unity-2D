using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public byte value;

	// Use this for initialization
	private void Start () {

        // Assign Renderer component
        rend = GetComponent<SpriteRenderer>();

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
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
        value = (byte) values[20];
    }
	
   
    // Coroutine that rolls the dice
    IEnumerator RollDice(ArrayList values)
    {
        foreach (int value in values)
        {
            rend.sprite = diceSides[value];
            yield return new WaitForSeconds(0.02f);
        }
    }
}
