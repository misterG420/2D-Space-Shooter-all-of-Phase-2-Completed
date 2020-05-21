using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileScript : MonoBehaviour
{

	private float speed = 9f;
	private Player player;
	private GameObject closestEnemy;

	void Start()
    {
		FindClosestEnemy();
	}

    void Update()
    {
		FlyTowardsClosestEnemy();
	}


	public void FindClosestEnemy()
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closestEnemy = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		foreach (GameObject go in gos)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closestEnemy = go;
				distance = curDistance;
			}
		}
		closestEnemy.gameObject.GetComponent<SpriteRenderer>().material.color = Color.red; //just to visually show closest enemy;

		//reference to the prefab is not the same as the instance of that prefab
		//so ideally I  should pass the "target" gameobject to the instance of the missle and have it movetowards in update but no idea how;
	}

	public void FlyTowardsClosestEnemy()
	{
		transform.position = Vector2.MoveTowards(transform.position, closestEnemy.gameObject.transform.position, speed * Time.deltaTime);
		//does not work; homing missile sits still
	}
}
