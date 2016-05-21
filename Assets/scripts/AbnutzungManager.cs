using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AbnutzungManager : MonoBehaviour {

    Text debugFIeld;

    public void CalcDmg(float pssngrs, float zustnd, float maxpssngers)
    {
        debugFIeld.text = "hallo";

        float[] returnArray = new float[2];

        float minRoll = 0;
        float maxRoll = 0;

        if (zustnd <= 10)
        {
            minRoll = 1;
            maxRoll = 30;
        }
        else if (zustnd <= 20 && zustnd > 10)
        {
            minRoll = 1;
            maxRoll = 40;
        }
        else if (zustnd <= 30 && zustnd > 20)
        {
            minRoll = 11;
            maxRoll = 60;
        }
        else if (zustnd <= 40 && zustnd > 30)
        {
            minRoll = 21;
            maxRoll = 75;
        }
        else if (zustnd <= 50 && zustnd > 40)
        {
            minRoll = 41;
            maxRoll = 90;
        }
        else if (zustnd <= 60 && zustnd > 50)
        {
            minRoll = 51;
            maxRoll = 100;

        }
        else if (zustnd > 60)
        {
            minRoll = 71;
            maxRoll = 100;
        }

        if (((pssngrs / maxpssngers) > 1) && ((pssngrs / maxpssngers) <= 1.3f))
        {
            if (minRoll < 11)
            {
                minRoll = 11;
            }
        }
        else if (((pssngrs / maxpssngers) > 1.3f) && ((pssngrs / maxpssngers) <= 1.6f))
        {
            if (minRoll < 31)
            {
                minRoll = 31;
            }
        }
        else if (((pssngrs / maxpssngers) > 1.6f) && ((pssngrs / maxpssngers) <= 2f))
        {
            if (minRoll < 51)
            {
                minRoll = 51;
            }
        }

        float roll = Random.Range(minRoll * 100, maxRoll * 100)/100;

        float minDead = 0;
        float maxDead = 0;

        float zustndLoss = 0;
       

        if (roll > 0 && roll <= 10)
        {
            minDead = 0;
            maxDead = 0;
            zustndLoss = 0;
           
        }
        if (roll > 10 && roll <= 20)
        {
            minDead = 3;
            maxDead = 8;
            zustndLoss = 5;
         
        }

        if (roll >20 && roll <= 50)
        {
            minDead = 5;
            maxDead = 15;
            zustndLoss = 10;
        }

        if (roll > 50 && roll <= 70)
        {
            minDead = 10;
            maxDead = 20;
            zustndLoss = 15;
        }
        if (roll > 70 && roll <= 90)
        {
            minDead = 15;
            maxDead = 30;
            zustndLoss = 25;
        }
        if (roll > 90 && roll <= 100)
        {
            minDead = 100;
            maxDead = 100;
            zustndLoss = 100;
        }

        float lostRoll = Random.Range( minDead*100, maxDead *100)/100;

        float lostPassengersTotal = (pssngrs / 100) * lostRoll;

        returnArray[0] = Mathf.RoundToInt(lostPassengersTotal);
        returnArray[1] = Mathf.RoundToInt(zustndLoss);

      //  return lostPassengersTotal;
      //  return returnArray;
    }

    void Start ()
    {
        debugFIeld = GameObject.Find("DebugBox").GetComponent<Text>();

      //  CalcDmg(50, 40, 100);

      //float[] testfield = new float[2];
      //  testfield = CalcDmg(50, 0, 100);
      //  print(testfield[0] + " lost passengers");
      //  print(testfield[1] + " lost zustnad");

      //  debugFIeld.text = testfield[0].ToString() + "  " + testfield[1].ToString();
    }
	
	
	void Update ()
    {
	
	}
}
