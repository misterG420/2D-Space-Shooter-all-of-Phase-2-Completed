using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;

    //0 trippleShot;
    //1 speed;
    //2 shields;
<<<<<<< Updated upstream
=======
    //3 health;
    //4 megaUltraSuperBlaster;
    //5 ammoPickup;
    //6 negativePowerup;
    //7 homingmissile;

>>>>>>> Stashed changes
    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip clip;

    private float speedMoveTowardsPlayer = 5f;
    public Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Player.playerHasPressedC = false;
    }


    void Update()
    {

        if (Player.playerHasPressedC == true)
            PowerupMovingToPlayer();

        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }


        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    void PowerupMovingToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speedMoveTowardsPlayer * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            AudioSource.PlayClipAtPoint(clip, transform.position);

            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TrippleShotActive(); Destroy(this.gameObject);
                        break;

                    case 1:
                        player.SpeedPowerupActive(); Destroy(this.gameObject);
                        break;

                    case 2:
                        player.ShieldPowerupActive(); Destroy(this.gameObject);
                        break;
<<<<<<< Updated upstream
=======

                    case 3:
                        player.HealthPowerupActive(); Destroy(this.gameObject);
                        break;
                    
                    case 4:
                        player.MegaBlaterActive(); Destroy(this.gameObject);
                        break;

                    case 5:
                        player.AmmoPickupActive(); Destroy(this.gameObject);
                        break;

                    case 6:
                        player.NegativePickupActive(); Destroy(this.gameObject);
                        break;

                    case 7: //I never included the homing missile because I never made it work...I identify closest target but the missle does just sit there, not moving...
                        player.HomingMissileActive(); Destroy(this.gameObject);
                        break;
>>>>>>> Stashed changes
                }
            }

            Destroy(this.gameObject);
        }
       
        if (other.tag == "Laser")
            Destroy(this.gameObject);

    }
     
}
