using System.Collections;
<<<<<<< Updated upstream
using System.Collections.Generic;
using UnityEngine;

=======
using UnityEngine;


>>>>>>> Stashed changes
public class Player : MonoBehaviour
{
	[SerializeField]
	private float speed = 3.5f;

	[SerializeField]
	private GameObject laserPrefab;

	[SerializeField]
	private GameObject TrippleLaserPrefab;

	[SerializeField]
	private float fireRate = 0.5f;

	[SerializeField]
	private float canFire = -1f;

	[SerializeField]
	private int lives = 3;

	private float speedBoostMultiplier = 1f;
<<<<<<< Updated upstream
	private SpawnManager spawnManager;
=======

	//THRUSTER STUFF;
	public float maxThrusterAmount = 100f;
	public float ThrusterDecreaseRate;
	public static float lostThruster;
	
	private SpawnManager powerupSpawnManager;
	private EnemyWaveSpawner enemyWaveSpawner;
>>>>>>> Stashed changes

	[SerializeField]
	private bool isTrippleShotActive = false;

	[SerializeField]
	private bool isShieldActive = false;

	[SerializeField]
	private GameObject shield;
	
	[SerializeField]
	private int score;

	private UIManager uiManager;
	
	[SerializeField]
	private GameObject rightEngine, leftEngine;

	[SerializeField]
	private AudioClip laserClip;

	private AudioSource audioSource;

	public CameraShake cameraShake;

<<<<<<< Updated upstream
=======
	[SerializeField]
	private SpriteRenderer shieldRenderer;

	[SerializeField]
	private SpriteRenderer Thruster;

	public static int ammo = 15;
	private bool hasAmmo = true;

	public static bool playerHasPressedC = false;

	private bool isHomingMissileActive = false;

	public GameObject homingMissile;

>>>>>>> Stashed changes
	void Start()
	{
		shield.SetActive(false);
		ammo = 15;
		playerHasPressedC = false;

		transform.position = new Vector3(0, 0, 0);
		powerupSpawnManager = GameObject.Find("PowerupSpawnManager").GetComponent<SpawnManager>();
		enemyWaveSpawner = GameObject.Find("EnemyWaveSpawner").GetComponent<EnemyWaveSpawner>();
		uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		audioSource = GetComponent<AudioSource>();
<<<<<<< Updated upstream
=======
		Thruster.color = new Color(1f, 1f, 1f, .5f);

>>>>>>> Stashed changes

		if (powerupSpawnManager == null)
		{
			Debug.LogError("The spawn manager is null!");
		}

		if(audioSource == null)
		{
			Debug.LogError("Audio source on the player is null!");
		}

		else
		{
			audioSource.clip = laserClip;
		}
	}


	void Update()
<<<<<<< Updated upstream
	{
=======
	{	
		if (Input.GetButton("Fire3"))
		{
			ThrusterCooldownCalculation();
		}

		else
		{
			shiftThrusterMultiplier = 1f;
			Thruster.color = new Color(1f, 1f, 1f, .5f);
		}

>>>>>>> Stashed changes
		CalculateMovement();
		if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
		{
			FireLaser();
		}

<<<<<<< Updated upstream
=======
		if (Input.GetKeyDown(KeyCode.C))
		{
			playerHasPressedC = true;
		}

		else
			return;
>>>>>>> Stashed changes
	}

	void CalculateMovement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
		transform.Translate(direction * speed * speedBoostMultiplier * Time.deltaTime);

		if (transform.position.y >= 0)
		{
			transform.position = new Vector3(transform.position.x, 0, 0);
		}
		
		else if(transform.position.y <= -3.8f)
		{
			transform.position = new Vector3(transform.position.x, -3.8f, 0);
		}

		if (transform.position.x > 11.3f)
		{
			transform.position = new Vector3(-11.3f, transform.position.y, 0);
		}
		
		else if(transform.position.x < -11.3f)
		{
			transform.position = new Vector3(11.3f, transform.position.y, 0);
		}
	}

	void FireLaser()
	{
		canFire = Time.time + fireRate;

		if (isTrippleShotActive == true)
		{
			Instantiate(TrippleLaserPrefab, transform.position, Quaternion.identity);
		}
<<<<<<< Updated upstream
		else
=======

		else if (isMegaBlasterActive == true)
		{
			Instantiate(MegaBlasterPrefab, transform.position, Quaternion.identity);
		}

		else if (isHomingMissileActive == true)
		{
			Instantiate(homingMissile, transform.position, Quaternion.identity);
			HomingMissileActive();
		}

		else if (hasAmmo == true)
>>>>>>> Stashed changes
		{
			Instantiate(laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
		}

		audioSource.Play();
	}

	public void Damage()
	{
		if (isShieldActive == true)
		{
			cameraShake.ShakeIt();
			isShieldActive = false;
			shield.SetActive(false);
			return;
		}
		
		lives -= 1;
		cameraShake.ShakeIt();

		if (lives == 2)
		{
			leftEngine.SetActive(true);
		}

		else if (lives == 1)
		{
			rightEngine.SetActive(true);
		}

		uiManager.UpdateLives(lives);
		
		if (lives < 1)
		{
			powerupSpawnManager.OnPlayerDeath();
			Destroy(this.gameObject);
		}
	}

	public void TrippleShotActive()
	{
		isTrippleShotActive = true;
		StartCoroutine(TrippleShotPowerUpDownRoutine());
	}

	public void SpeedPowerupActive()
	{
		speedBoostMultiplier = 3f;
		StartCoroutine(SpeedPowerUpDownRoutine());
	}

	public void ShieldPowerupActive()
	{
		isShieldActive = true;
		shield.SetActive(true);
	}

	public void AddScore(int points)
	{
		score += points;
		uiManager.UpdateScore(score);
	}

<<<<<<< Updated upstream
=======
	public void HealthPowerupActive()
	{
		if (lives < 3)
		{
			lives++;
			uiManager.UpdateLives(lives);
		}

		if(lives == 3)
		{
			leftEngine.SetActive(false);
			rightEngine.SetActive(false);
		}

		if(lives == 2)
		{
			leftEngine.SetActive(true);
			rightEngine.SetActive(false);
		}

		else
			return;
	}

	public void MegaBlaterActive()
	{
		isMegaBlasterActive = true;
		StartCoroutine(MegaBlasterPowerUpDownRoutine());
	}

	public void AmmoPickupActive()
	{
		ammo = 15;
		hasAmmo = true;
}

	public void NegativePickupActive()
	{
		speed = 0.5f;
		StartCoroutine(RestoreSpeedAfterNegativePickup());
	}

	public void ThrusterCooldownCalculation()
	{
		lostThruster += ThrusterDecreaseRate * Time.deltaTime;

		if (lostThruster < maxThrusterAmount)
		{
			Thruster.color = new Color(1f, 1f, 1f, 1f);
			shiftThrusterMultiplier = 2f;
		}	
			

		else if (lostThruster >= maxThrusterAmount)
		{
			shiftThrusterMultiplier = 1f;
			lostThruster = maxThrusterAmount;
			uiManager.ShowThrusterDepleted();
			StartCoroutine(WaitForThruster());
		}
	}

	public void HomingMissileActive()
	{
		//remember to actually spawn the item and make it a rare item!
		isHomingMissileActive = true;
		StartCoroutine(HomingMissilePowerupDownRoutine());
	}

	


	IEnumerator WaitForThruster()
	{
		yield return new WaitForSeconds(5.0f);
		lostThruster = 0;
		uiManager.HideThrusterText();
	}

>>>>>>> Stashed changes
	IEnumerator TrippleShotPowerUpDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		isTrippleShotActive = false;
	}

	IEnumerator SpeedPowerUpDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		speedBoostMultiplier = 1f;
	}
<<<<<<< Updated upstream
=======

	IEnumerator MegaBlasterPowerUpDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		isMegaBlasterActive = false;
	}

	IEnumerator RestoreSpeedAfterNegativePickup()
	{
		yield return new WaitForSeconds(5.0f);
		speed = 3.5f;
	}

	IEnumerator HomingMissilePowerupDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		isHomingMissileActive = false;
	}
>>>>>>> Stashed changes
}
