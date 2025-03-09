using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
	public GameObject PinSpawner;
	public GameObject setOfPinsPrefab;
	int firstCountPoints;
	int secondCountPoints;
	bool firstCountDone = false;
	int finalPoints = 0;
	int previousFinalPoints = 0;
	public GameObject scoreBoard;
	public GameObject messageCanvas;
	public VideoPlayer videoPlayer;
	GameObject videoplayerCanvas;

	public VideoClip strike;
	public VideoClip spare;
	public VideoClip miss;

	int totalRounds = 5;
	int round = 0;

	int totalPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        resetPins();
        messageCanvas.SetActive(false);
        videoplayerCanvas = videoPlayer.gameObject.transform.parent.gameObject;
        videoplayerCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.RawButton.A))
        {
        	startNewGame();
        }
    }

    public void CountPoints()
    {
    	if(round<totalRounds)
    	{
    		if(!firstCountDone)
	    	{
	    		firstCountPoints = 10 - PinSpawner.transform.GetChild(0).childCount;
	    		scoreBoard.transform.GetChild(3*round).GetComponent<TMP_Text>().text = firstCountPoints.ToString();
	    		// log.text += "first" + firstCountPoints.ToString() + "\n";
	    		firstCountDone = true;

	    		totalPoints += firstCountPoints;
	    		scoreBoard.transform.GetChild(15).GetComponent<TMP_Text>().text = totalPoints.ToString();

	    		if(firstCountPoints == 10)
	    		{
	    			resetPins();
	    			
		    		scoreBoard.transform.GetChild(3*round).GetComponent<TMP_Text>().text = "x";

		    		finalPoints += firstCountPoints;
	    			scoreBoard.transform.GetChild(3*round + 2).GetComponent<TMP_Text>().text = finalPoints.ToString();

	    			// strike
	    			videoPlayer.clip = strike;
	    			playClip();

	    			round++;
		    		firstCountDone = false;
		    		if(round == totalRounds)
		    		{
		    			messageCanvas.SetActive(true);
		    		}
	    		}
	    		else if(firstCountPoints == 0)
	    		{
	    			//miss
	    			videoPlayer.clip = miss;
	    			playClip();
	    		}

	    	}
	    	else
	    	{
	    		previousFinalPoints = finalPoints;

	    		finalPoints += 10 - PinSpawner.transform.GetChild(0).childCount;
	    		scoreBoard.transform.GetChild(3*round + 2).GetComponent<TMP_Text>().text = finalPoints.ToString();

	    		secondCountPoints = finalPoints - previousFinalPoints - firstCountPoints;
	    		if(secondCountPoints + firstCountPoints == 10)
	    		{
	    			scoreBoard.transform.GetChild(3*round + 1).GetComponent<TMP_Text>().text = "/";
	    			
	    			// spare
	    			videoPlayer.clip = spare;
	    			playClip();
	    		
	    		}		
	    		else
	    		{
	    			scoreBoard.transform.GetChild(3*round + 1).GetComponent<TMP_Text>().text = secondCountPoints.ToString();
	    		}
	    		if(previousFinalPoints == finalPoints)
	    		{
	    			//Gutter
	    			videoPlayer.clip = miss;
	    			playClip();
	    		}
    					
	    		totalPoints += secondCountPoints;
	    		scoreBoard.transform.GetChild(15).GetComponent<TMP_Text>().text = totalPoints.ToString();

	    		round++;
	    		firstCountDone = false;
	    		if(round == totalRounds)
	    		{
	    			messageCanvas.SetActive(true);
	    		}

	    		resetPins();
	    	}
    	}
    }

    void resetPins()
    {
    	Destroy(PinSpawner.transform.GetChild(0).gameObject);
    	GameObject g = Instantiate(setOfPinsPrefab, PinSpawner.transform);
    	g.transform.parent = PinSpawner.transform;
    }

    public void startNewGame()
    {
    	messageCanvas.SetActive(false);
		firstCountDone = false;
		finalPoints = 0;
		previousFinalPoints = 0;
		
		round = 0;

		totalPoints = 0;

		resetScoreBoard();
    }

    void resetScoreBoard()
    {
    	for(int i=0; i<16;i++)
    	{
    		scoreBoard.transform.GetChild(i).GetComponent<TMP_Text>().text = "";
    	}
    }

    void playClip()
    {
    	//disable score board
    	scoreBoard.SetActive(false);

    	// enable score board
    	videoplayerCanvas.SetActive(true);
    	videoPlayer.Play();

    	//Invoke repeating of checkOver method
		InvokeRepeating("checkOver", 2.0f, 1.0f);
    }

    private void checkOver()
	{
		if(!videoPlayer.isPlaying)
		{
			print ("VIDEO IS OVER");
	        //disable score board
	    	scoreBoard.SetActive(true);

	    	// enable score board
	    	videoplayerCanvas.SetActive(false);

	    	CancelInvoke("checkOver");
		}
	}
}
