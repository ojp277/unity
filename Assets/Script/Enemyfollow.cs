using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyfollow : MonoBehaviour {
    private Transform doel;
    public float speed;
    Vector2 spawnPoint;
    void Start () {
        doel = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

	void Update () {
        speed = 12;
        transform.position = Vector3.MoveTowards(transform.position, doel.position, speed * Time.deltaTime);
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals ("Player"))
        {
            //transform.position = spawnPoint;
        }
    }
}
