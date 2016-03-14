using UnityEngine;
using System.Collections;

public class EnemyPath : MonoBehaviour {

    float usagePoints = 0;
     float timer = 0;
    float reduceThreshold = 1;
    float reducePPintervall = 3;

    public float UsagePoints
    {
        get
        {
            return usagePoints;
        }

        set
        {
            usagePoints = value;
        }
    }

    void Start ()
    {
	
	}
	
	
	void Update ()
    {
        reducePoints();
        print(usagePoints);
    }


    void reducePoints()
    {
        timer += Time.deltaTime;
        if (timer >= reduceThreshold)
        {
            timer = 0;
            usagePoints -= reducePPintervall;
            usagePoints = Mathf.Clamp(usagePoints, 0, 1000);
        }
    }
}
