using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	//variable for speed, the enemy is going to move down until unseen from the screen
	[SerializeField] private float speed = 8.0f;
	[SerializeField] private GameObject enemyExplosionPreFab = null;
	private UIManager _uiManager;
	private AudioSource _audiosource;
	[SerializeField] private AudioClip _clip = null;



	void Start()
	{
		_uiManager= GameObject.Find("Canvas").GetComponent<UIManager>();

		_audiosource = GetComponent<AudioSource>();
	}


	void Update () 
	{
		//when the enemy gets to a different point, repurpose it on the game board and randomize its position.
		EnemyMovement();
	}
	public void EnemyMovement()
	{
	transform.Translate(Vector3.down * speed * Time.deltaTime);

		if(transform.position.y < -7)
		{
			Destroy(this.gameObject);
			//float randomX = Random.Range(-8.18f, 8.18f);
			//figured out whole code on my own looking up things in unity api
			//transform.position = new Vector3(randomX, 5.9f, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
	if(other.tag == "PlayerLaser")
	{
	if(other.transform.parent != null)
	{
		Destroy(other.transform.parent.gameObject);
	}
		Destroy(other.gameObject);
		Instantiate(enemyExplosionPreFab, transform.position, Quaternion.identity);
		//use playatclip point to play a sound when something is destroyed. The destroyed item cannot play it itself.
		AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
		_uiManager.UpdateScore();
		Destroy(this.gameObject);
	}
	else if(other.tag == "Player")
	{
	Player player = other.GetComponent<Player>();
	player.PlayerDamaged();
	//Also use the Camera.main to make the clip point the main camera, closest to the player, when using a 2d plane. 
	AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
	Instantiate(enemyExplosionPreFab, transform.position, Quaternion.identity);
	_uiManager.UpdateScore();
	Destroy(this.gameObject);
	}


}
}

