using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    Text Highscore;
    int Money = 100000;

	void Start ()
    {
        
        Highscore = GameObject.Find("HighScore").GetComponent<Text>();
        Highscore.text = "Money " + Money.ToString();
    }
	
	
	void Update ()
    {
	
	}

    public void spentMoney(int value)
    {
        Money -= value;
        Highscore.text = "Money " + Money.ToString();
    }

    public void earnMoney(int value)
    {
        Money += value;
        Highscore.text = "Money " + Money.ToString();
    }

}
