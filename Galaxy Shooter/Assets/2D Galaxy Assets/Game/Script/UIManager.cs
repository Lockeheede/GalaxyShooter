//NOTE TO SELF: NAME THE GAME SWARM, THE SHOOTER IS NAMED GLORIA AND THE CONCEPT IS TO OUTLIVE THE SWARM OF ENEMIES EACH LEVEL UNTIL GAME OVER. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Gives access to all ui elements in the unity editor

public class UIManager : MonoBehaviour
{//Important note, the managers shouldn't communicate with the objects, pcs and npcs, those objects communicate with the manager
//The reason for this is because if an item was to send to the manager and then destroy itself, sending back to an item that doesn't exsist
//would crash the game!!
	public Sprite[] lives;
	public Image livesImageDisplay;
	public Text scoreDisplay;
	public int score;
	public GameObject titleScreen;

	public void UpdateLives(int currentLives)
	{
		Debug.Log("Player Lives: " + currentLives);
		livesImageDisplay.sprite = lives[currentLives];
	}

	public void UpdateScore()
	{
		score += 1;
		scoreDisplay.text = "Score: " + score;
	}
	public void NewScore()
	{
		score = 0;
		scoreDisplay.text = "Score: " + score;
	}
	public void ShowTitleScreen()
	{	
		titleScreen.SetActive(true);
	}
	public void HideTitleScreen()
	{
		titleScreen.SetActive(false);
	}
}
