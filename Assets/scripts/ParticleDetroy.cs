using UnityEngine;
using System.Collections;

public class ParticleDetroy : MonoBehaviour {


	void Start ()
    {
        Invoke("DestroyMe",6);
	}
	
	
	void Update ()
    {
	
	}

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
