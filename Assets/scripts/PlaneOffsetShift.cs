using UnityEngine;
using System.Collections;

public class PlaneOffsetShift : MonoBehaviour {

    Material myMat;
    float a = 0;
    float b = 0;

   
   
    public bool isScrolling = true;

    public float offSetSPeedA = 2;
    public float offSetSPeedB = 2;

    public bool isRotating = false;
   
    public float rotY = 0;

    public bool isOscillateScrolling = false;

    float oszA = 0;
    float oszB = 0;
    float shiftTimer  = 0;
    public float shiftIntervall = 1;
    public float oszilateSPeedA = 2;
    public float oszilateSPeedB = 2;

    bool oszPhase = false;

    void Start ()
    {
        myMat = GetComponent<Renderer>().material;
      
    }
	
	
	void Update ()
    {
        if (isScrolling)
        {
            a += offSetSPeedA * Time.deltaTime;
            b += offSetSPeedB * Time.deltaTime;

            myMat.SetTextureOffset("_MainTex", new Vector2(a, b));
        }

        if (isRotating)
        {
            transform.Rotate(0, rotY, 0);
        }

        if (isOscillateScrolling)
        {
            if (oszPhase)
            {
                oszA += oszilateSPeedA * Time.deltaTime;
                oszB += oszilateSPeedB * Time.deltaTime;

                myMat.SetTextureOffset("_MainTex", new Vector2(oszA, oszB));

                shiftTimer += Time.deltaTime;
                if (shiftTimer >= shiftIntervall)
                {
                    shiftTimer = 0;
                    oszPhase = false;
                }
            }
           else if (!oszPhase)
            {
                oszA += (oszilateSPeedA * (-1)) * Time.deltaTime;
                oszB += (oszilateSPeedB * (-1)) * Time.deltaTime;

                myMat.SetTextureOffset("_MainTex", new Vector2(oszA, oszB));

                shiftTimer += Time.deltaTime;
                if (shiftTimer >= shiftIntervall)
                {
                    shiftTimer = 0;
                    oszPhase = true;


                }
            }
        }
    }
}
