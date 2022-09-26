using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    
    private float _health = 1f;

    private float _speed = 5f;
    
    
    
    void Update()
    {
        Vector3 dir = (gameObject.transform.position - player.transform.position).normalized;
        gameObject.transform.position -= dir * _speed * Time.deltaTime;
        // if the health of the enemy is less than 0 destroy it
        if (_health <= 0)
        {
            // remove enemy from list of enemies (also increment kill counter)
            ScoreCounter.ScoreInstance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    public void DecrementHealth(float health)
    {
        _health -= health;
    }

}
