using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class pin : MonoBehaviour
{
	//TMP_Text log;
	bool marked = false;   // flag to maark this pin as hitted
	float timeToDestroy = 3.0f;    // time to destroy the pin

    void OnCollisionEnter(Collision collision)
    {   
        // if collided with oher pin or ball aand not already flagged
    	if(!marked)
    	{
    		if((collision.collider.tag == "Pin") || (collision.collider.tag == "Ball"))
	        {    
                // flaag it as counted and destroy the pin
	        	marked = true;
	        	Destroy(gameObject, timeToDestroy);
	        }
    	}   
    }
}
