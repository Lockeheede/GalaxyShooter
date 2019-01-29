using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour 
{

//A spawn manager is a game object that holds things to be randomly spawned (or timed spawned)

[SerializeField] private GameObject enemyShipPreFab = null;
[SerializeField] private GameObject[] powerUps = null;

private GameManager _gameManager;

	// Use this for initialization
	void Start () 
	{
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

	}
	//Create a coroutine to make a game loop (which is usually a while loop)
	//As long as the game is running, spawn every 5 seconds
	public void StartSpawnRoutines()
	{
		StartCoroutine(EnemySpawnRoutine());	 
		StartCoroutine(PowerUpSpawnRoutine());
	}

	IEnumerator EnemySpawnRoutine()
		{
			while(_gameManager.gameOver == false)
			{
			{
			Instantiate(enemyShipPreFab, new Vector3(Random.Range(7.0f, -7.0f), 7, 0), Quaternion.identity);
			yield return new WaitForSeconds(1.0f);

			}
		}
	}

	IEnumerator PowerUpSpawnRoutine()
		{
			while(_gameManager.gameOver == false)
			{
			int randomPowerUp = Random.Range(0,2);
			Instantiate(powerUps[randomPowerUp], new Vector3(Random.Range(7.0f, -7.0f), 7, 0), Quaternion.identity);
			yield return new WaitForSeconds(5.0f);
			}
		}
}

