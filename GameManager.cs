using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Unity recognized C# Script that is meant to manage aspects of the game, like the title screen
public class GameManager : MonoBehaviour 
{
	public bool gameOver = true;
	public GameObject Player;

	private UIManager _uiManager;

	void Start()
	{
	_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}

	void Update()
	{
		if (gameOver == true)
		{
		_uiManager.NewScore();
			if(Input.GetKeyDown(KeyCode.Return))
			{
				Instantiate(Player, Vector3.zero, Quaternion.identity);
				gameOver = false;
				_uiManager.HideTitleScreen();
			}
		}	
	}

	}
