using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	[SerializeField] private float speed = 3.0f;
	[SerializeField] private int powerUpId = 0; //0 = tripleshot, 1 = speedboost, 2 = shield
	private UIManager _uiManager;
	[SerializeField] private AudioClip _clip = null;


	void Start()
	{
		_uiManager= GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	

	void Update()
	{
	transform.Translate(Vector3.down * speed * Time.deltaTime);

		if(transform.position.y < -7.0f)
		{
		Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
	Debug.Log("Collided with " + other.name);

	if(other.tag == "Player")
	{
	//access the player class and turn tripleshot bool to true and destroy this game object
	Player player = other.GetComponent<Player>();
	if(player != null)//work with null checking with getcomponent
	{
		if(powerUpId == 0)
		{
		//enable tripleshot
		player.TripleShotPowerUpOn();
		}
		else if (powerUpId == 1)
		{
		//enable speedboost
		player.SpeedBoostPowerUpOn();
		}
		else if (powerUpId == 2)
		{
		//enable shield
		player.ShieldPowerUpOn();
		}

	}

	_uiManager.UpdateScore();
	AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
	Destroy(this.gameObject);
	}
	}
	}
