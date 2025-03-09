using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class Ball : MonoBehaviour
{
	//TMP_Text log;
	float timeToDestroy = 10.0f;   // time to destroy ball
	bool audioPlayed = false;      // flag to know if auido is played
	GameManager gamemanager;       // reference to gamemanager
	bool collisionwithFloor = false;   // flag to know if ball collided with floor

    // Start is called before the first frame update
    void Start()
    {
        // get game manager from the scene
    	gamemanager = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
    	//log = GameObject.FindWithTag("Log").GetComponent<TMP_Text>();
    }

    // if ball collided with any object
    void OnCollisionEnter(Collision collision)
    {
        // if not already collided with floor
    	if(!collisionwithFloor)
    	{
    		if(collision.gameObject.tag == "Environment")
	        {
	        	destroyObject();  //destroy the ball after given time
	        	collisionwithFloor = true;    // mark the flag
	        }
    	}
        

        // if collided with pins
        if((collision.gameObject.tag == "Pin") && (!audioPlayed))
        {
        	//play the audio
        	gameObject.GetComponent<AudioSource>().Play();
        	audioPlayed = true;    // mark the flag that audio is played
        }
    }

    void destroyObject()
    {
        // count points and destroy the ball
    	gamemanager.Invoke("CountPoints", timeToDestroy);
    	Destroy(gameObject, timeToDestroy);
    }

}
