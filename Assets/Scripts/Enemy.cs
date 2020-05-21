using System.Collections;
<<<<<<< Updated upstream
using System.Collections.Generic;
using UnityEditor.Rendering;
=======
>>>>>>> Stashed changes
using UnityEngine;


//BUGS: ENEMY WITH SHILED CANNOT BE SHOT WITH LASER ANYMORE?!
//Check if boss does actually move to the target location!

public class Enemy : MonoBehaviour
{

	private Player player;
	public Transform playerTransform;

	private Animator animator;
	private AudioSource audiosource;

	[Header("Enemy-related variables:")]
	[SerializeField]
	private float speed = 3f;
	[SerializeField]
	private GameObject laserPrefab;

	private float fireRate = 3.0f;
	private float canFire = -1f;

	RaycastHit2D hit; //for the powerup to move to player;
	RaycastHit2D hit2; //for dodging the player's laser;
	RaycastHit2D hit3; //for shooting the player from behind;

	[SerializeField]
	private int enemyID;
	private GameObject enemyShield;

	private float speedMoveTowardsPlayer = 4f;
	private bool enemyRammingPlayer = false;



	public Transform targetLocationBoss;

	[Header("Boss Projectile settings:")]
	[SerializeField]
	private int numberOfProjectiles = 10;
	[SerializeField]
	private float projectileSpeed = 4f;
	public GameObject bossProjectilePrefab;

	private Vector3 startPoint;
	private const float radius = 1f;

	//enemyIDs:
	//0: vanilla enemy - no shields
	//1: enemy with 1hit shield
	//2: "aggresive enemy type": enemy with kamikaze (if close to player, it rams player)
	//3: "enemy avoid shot": Enemy that detects player's shot and dodges
	//4: "smart enemy": If enemy is behind player, it shoots backwards
	//5: boss
	//6: sideways-moving enemy;


	void Start()
	{
		enemyRammingPlayer = false;
		audiosource = GetComponent<AudioSource>();
		player = GameObject.Find("Player").GetComponent<Player>();
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

		if (player == null)
		{
			Debug.LogError("Player is null!");
		}

		animator = GetComponent<Animator>();
		if (animator == null)
		{
			Debug.LogError("Animator is null!");
		}
	}


	void Update()
	{
		if (enemyID != 6)
		{
			CalculateMovement();
			hit = Physics2D.Raycast(transform.position, Vector2.down);              //this raycast is needed to identify a powerup which will be shot;

			if (hit.collider != null)
			{
				if (hit.collider.gameObject.tag == "Poweruptag")
				{
					Debug.Log("The enemy targeted a powerup which will now be destroyed!");
					GameObject enemyLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
					Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
					for (int i = 0; i < lasers.Length; i++)
					{
						lasers[i].AssignEnemyLaser();
					}
				}
			}
		}

		if (enemyID == 6)
			CalculateSidewayMovement();


		if (enemyID == 5)
		{
			startPoint = transform.position;
			CalculateBossMovement();
			InitiateBossAttack(numberOfProjectiles);
		}

		if (enemyID == 4)
		{
			hit3 = Physics2D.Raycast(transform.position, Vector2.up);               //this raycast is needed to shoot backwards and shoot at player when it is below the player;

			if (hit3.collider.gameObject.tag == "Player" && enemyID == 4)
			{
				GameObject enemyLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
			}
		}

		if (enemyID == 3)
		{
			hit2 = Physics2D.CircleCast(transform.position, 5f, Vector2.down);      //this raycast is needed to dodge the player's laser;

			if (hit2.collider != null)
			{
				if (hit2.collider.gameObject.tag == "Laser" && enemyID == 3)
				{
					InitiateLaserDodgeSequence();
				}
			}
		}

		if (enemyRammingPlayer == true)
			EnemyRamPlayer();



		else if (Time.time > canFire)
		{
			fireRate = Random.Range(3f, 7f);
			canFire = Time.time + fireRate;
			GameObject enemyLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
			Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
			for (int i = 0; i < lasers.Length; i++)
			{
				lasers[i].AssignEnemyLaser();
			}
		}
	}

	void CalculateMovement()
	{
		transform.Translate(Vector3.down * speed * Time.deltaTime);
		if (transform.position.y < -5f)
		{
			transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
		}
	}

	void CalculateSidewayMovement()
	{
		transform.Translate(Vector2.left * speed * Time.deltaTime);
		if (transform.position.x < -10f)
		{
			transform.position = new Vector3(10, Random.Range(3.5f, 5f), 0);
		}
	}

	void CalculateBossMovement()
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards(transform.position, targetLocationBoss.transform.position, step);
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();

			if (player != null)
			{
				player.Damage();
			}

			animator.SetTrigger("OnEnemyDeath");
			speed = 0f;
			audiosource.Play();
			Destroy(this.gameObject, 2);
		}


		if (other.tag == "Laser")
		{
			enemyShield = GameObject.FindGameObjectWithTag("EnemyShield");


			switch (enemyID)

			//enemyID:
			//0: vanilla enemy - no shields
			//1: enemy with 1hit shield
			//2: "aggresive enemy type": enemy with kamikaze (if it was shot by player, it rams player as a revenge:D )
			//3: "enemy avoid shot": Enemy that detects player's shot and dodges it
			//4: "smart enemy": If enemy is behind player, it shoots backwards -> not yet implemented
			//5: boss
			//6: sideways-moving enemy;


			{
				case 0: 
					player.AddScore(10);
					animator.SetTrigger("OnEnemyDeath");
					speed = 0f;
					audiosource.Play();
					Destroy(GetComponent<Collider2D>());
					Destroy(this.gameObject, 2);
					break;

				case 1:
					enemyShield.SetActive(false);
					enemyID = 0;
					break;

				case 2:
					enemyShield.SetActive(false);
					enemyRammingPlayer = true;
					EnemyRamPlayer();
					enemyID = 0;
					break;

				case 3:
					InitiateLaserDodgeSequence();
					enemyID = 0;
					break;

				case 4:
					player.AddScore(10);
					animator.SetTrigger("OnEnemyDeath");
					speed = 0f;
					audiosource.Play();
					Destroy(GetComponent<Collider2D>());
					Destroy(this.gameObject, 2);
					break;

				case 5: 
					player.AddScore(200);
					animator.SetTrigger("OnEnemyDeath");
					speed = 0f;
					audiosource.Play();
					Destroy(GetComponent<Collider2D>());
					Destroy(this.gameObject, 2);
					break;

				case 6:
					player.AddScore(10);
					animator.SetTrigger("OnEnemyDeath");
					speed = 0f;
					audiosource.Play();
					Destroy(GetComponent<Collider2D>());
					Destroy(this.gameObject, 2);
					break;

			}
		}
	}

	private void EnemyRamPlayer()
	{
		transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speedMoveTowardsPlayer * Time.deltaTime);
	}

	private void InitiateLaserDodgeSequence()
	{
		Vector2 direction = new Vector2(transform.position.x + 9f, transform.position.y);
		transform.Translate(direction * 4f * Time.deltaTime);
		//this dodge function is working a bit weird but it kinda gets the job done;
	}

	private void InitiateBossAttack(int numberOfProjectiles)
	{
		float angleStep = 360f / numberOfProjectiles;
		float angle = 0f;


		if (Time.time > canFire)
		{
			fireRate = Random.Range(1f, 3f);
			canFire = Time.time + fireRate;

			for (int i = 0; i <= numberOfProjectiles - 1; i++)
			{
				float projectileDirXPos = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
				float projectileDirYPos = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

				Vector3 projectileVector = new Vector3(projectileDirXPos, projectileDirYPos, 0);
				Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

				GameObject tempObj = Instantiate(bossProjectilePrefab, startPoint, Quaternion.identity);
				tempObj.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.y);

				angle += angleStep;
			}
		}
	}

}
