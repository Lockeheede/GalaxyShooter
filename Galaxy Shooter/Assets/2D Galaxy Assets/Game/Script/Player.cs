//NOTE TO SELF: NAME THE GAME SWARM, THE SHOOTER IS NAMED GLORIA AND THE CONCEPT IS TO OUTLIVE THE SWARM OF ENEMIES EACH LEVEL UNTIL GAME OVER. 
//ANOTHER NOTE: The manager stuff was confusing, but remember to make a handler, which is the private variaible with the manager name
//Then you find it with the find("manager").getcomponent<manager>(); to make sure it exists.
//Remember to use handler variables to call other methods from other scripts 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	//A note you never noticed. The class name doesn't update in the code if you change the name of the script

	public bool canTripleShot = false;
	public bool canSpeedBoost = false;
	public bool canShield = false;
	//GLOBAL VARIABLES
	[SerializeField] private GameObject laserPrefab = null; 
	//remember to set serializefield private variables to null to get through the error of not doing so
	[SerializeField] private GameObject tripleshotPrefab = null;
	[SerializeField] private float speed = 5.0f; 
	private float fireRate = 0.25f; 
	private float canFire;
	[SerializeField] private int playerLives = 3;
	[SerializeField] private GameObject explosionPreFab = null;
	[SerializeField] private GameObject shieldPreFab = null;
	[SerializeField] private GameObject[] enginesPreFab = null;
	private UIManager _uiManager;
	private GameManager _gameManager;
	private SpawnManager _spawnManager;
	private AudioSource _audiosource; 
	private int hitCount = 0;


		//Start() Method is when the game starts
		void Start()
		{
			//current pos is now new pos
			transform.position = new Vector3(0,-4.0f,0);
			_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

			if(_uiManager != null)
			{
			_uiManager.UpdateLives(playerLives);
			}
			_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

			if(_spawnManager != null)
			{
			_spawnManager.StartSpawnRoutines();
			}
			_audiosource = GetComponent<AudioSource>();

			hitCount = 0;

		}
		//Update() Method is every frame. You test game logic here, testing conditionals and other effects
		void Update()
		{
			//In the update, you check for movement and shoot methods
			Movement();
			if(Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
			{
				Shoot();
			}
	

		}


	//A function used to calculate movement of the ship
	private void Movement()
		{
			//f suffix is used to label a decimal pointed value as a float
			if(canSpeedBoost == true)
			{
				float horizontalInput = Input.GetAxis("Horizontal");
				transform.Translate(Vector3.right * speed * 1.3f * horizontalInput * Time.deltaTime);
				float verticalInput = Input.GetAxis("Vertical");
				transform.Translate(Vector3.up * speed * 1.3f * verticalInput * Time.deltaTime);
			}else if(canSpeedBoost == false)
				{//a frame is 60 per second
					float horizontalInput = Input.GetAxis("Horizontal");
					transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
					float verticalInput = Input.GetAxis("Vertical");
					transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
				}
			//in order to restrict movement on a gameboard, you use an if conditional
			//this is what you'll use throughout all of gaming to create its logic
			if (transform.position.y > 0)
			{
				transform.position = new Vector3(transform.position.x, 0, 0);
			}
			if (transform.position.y < -4.2f)
			{
				transform.position = new Vector3(transform.position.x, -4.2f, 0);
			}

			//This is to make a board wrap, making your character appear on the other side of the board
			if (transform.position.x >= 9.5f)
			{
				transform.position = new Vector3(-9.5f, transform.position.y, 0);
			}else if (transform.position.x <= -9.5f)
				{
					transform.position = new Vector3(9.5f, transform.position.y, 0);
				}
			}

		//A function used to calculate the ships shooting 
		private void Shoot()
			{
			//playing from audio source pplays whatever is assigned to this object in the inspector mode.
			_audiosource.Play();
				//Time.time and Laser() custom method. Use time.time to determine how long the game has been running.
				if(Time.time > canFire)
				{
					if(canTripleShot == true)
					{
						canFire = Time.time + fireRate;
						Instantiate(tripleshotPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);	
					}else if(canTripleShot == false)
						{
							Instantiate(laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);	
							//Quanternion handles rotation in unity.
						}
						canFire = Time.time + fireRate;
				}

			}

		//A few functions for how each power up works on the player
		public void TripleShotPowerUpOn()
			{
				canTripleShot = true;
				StartCoroutine(TripleShotPowerDownRoutine());//use Coroutines and ienumerators to create cool down options in a game
			}
				public IEnumerator TripleShotPowerDownRoutine()
				{
					yield return new WaitForSeconds(5.0f);
					canTripleShot = false;
				}

		public void SpeedBoostPowerUpOn()
			{
				canSpeedBoost = true;
				StartCoroutine(SpeedBoostPowerDownRoutine());
			}
				public IEnumerator SpeedBoostPowerDownRoutine()
				{
					yield return new WaitForSeconds(5.0f);
					canSpeedBoost = false;
				}

		public void ShieldPowerUpOn()
			{
				canShield = true;
				shieldPreFab.SetActive(true);
			}


		//this function is code called from the enemy ai script to calculate player damage
	public void PlayerDamaged()
	{
		if(canShield == true)
		{
			shieldPreFab.SetActive(false);
			canShield = false;	
		}

		hitCount++;
		if(hitCount == 1)
		{
		enginesPreFab[0].SetActive(true);
		}
		else if(hitCount == 2)
		{
		enginesPreFab[1].SetActive(true);
		}

		if(canShield == false)
		{
				playerLives--;
				_uiManager.UpdateLives(playerLives);

				if(playerLives < 1)
				{
					Instantiate(explosionPreFab, transform.position, Quaternion.identity);
					Destroy(this.gameObject);
					Debug.Log("Game Over!");

					_gameManager.gameOver = true;
					_uiManager.ShowTitleScreen();
				}
		}
					


	}
}
	