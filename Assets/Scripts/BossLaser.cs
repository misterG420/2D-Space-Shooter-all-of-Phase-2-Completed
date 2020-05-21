using UnityEngine;

public class BossLaser : MonoBehaviour
{
	[SerializeField]
	private float speed = 4.0f;

	void Update()
	{
		MoveDown();
	}

	void MoveDown()
	{
		transform.Translate(Vector3.down * speed * Time.deltaTime);

		if (transform.position.y < -8f)
		{
			if (transform.parent != null)
			{
				Destroy(transform.parent.gameObject);
			}

			Destroy(this.gameObject);
		}
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();

			if (player != null)
			{
				player.Damage();
			}
		}
	}

}

