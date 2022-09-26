using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public List<Transform> spawnPositionList = new List<Transform>();
    /*
     *  0: bottom1
     *  1: bottom2
     *  2: top1
     *  3: top2
     *  4: left1
     *  5: left2
     *  6: right1
     *  7: right2
     */
    
    private GameObject _player;
    private Camera _mainCamera;
    
    [SerializeField] private float _spawnRate = 4f;
    [SerializeField] private float _enemyAmount = 5f;
    
    private Vector3 _viewportPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _mainCamera = Camera.main;
        _viewportPos = _mainCamera.WorldToViewportPoint(_player.transform.position);

        InvokeRepeating("SpawnEnemies",0, _spawnRate);
    }

    private void Update()
    {
        _viewportPos = _mainCamera.WorldToViewportPoint(_player.transform.position);
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void SpawnEnemies()
    {
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0:
                for (int i = 0; i < _enemyAmount; i++)
                {
                    float xPosition = Random.Range(spawnPositionList[0].position.x, spawnPositionList[1].position.x);
                    float yPosition = spawnPositionList[0].position.y;
                    GameObject currEnemy = Instantiate(enemy, _viewportPos + new Vector3(xPosition, yPosition, 0f), Quaternion.identity);
            
                    // add enemy to list of enemies
                    ScoreCounter.ScoreInstance.AddEnemy(currEnemy);
                }
                break;
            case 1:
                for (int i = 0; i < _enemyAmount; i++)
                {
                    float xPosition = Random.Range(spawnPositionList[2].position.x, spawnPositionList[3].position.x);
                    float yPosition = spawnPositionList[2].position.y;
                    GameObject currEnemy = Instantiate(enemy, _viewportPos + new Vector3(xPosition, yPosition, 0f), Quaternion.identity);
            
                    // add enemy to list of enemies
                    ScoreCounter.ScoreInstance.AddEnemy(currEnemy);
                }
                break;
            case 2:
                for (int i = 0; i < _enemyAmount; i++)
                {
                    float xPosition = spawnPositionList[4].position.x;
                    float yPosition = Random.Range(spawnPositionList[4].position.y, spawnPositionList[5].position.y);
                    GameObject currEnemy = Instantiate(enemy, _viewportPos + new Vector3(xPosition, yPosition, 0f), Quaternion.identity);
            
                    // add enemy to list of enemies
                    ScoreCounter.ScoreInstance.AddEnemy(currEnemy);
                }
                break;
            case 3:
                for (int i = 0; i < _enemyAmount; i++)
                {
                    float xPosition = spawnPositionList[4].position.x;
                    float yPosition = Random.Range(spawnPositionList[4].position.y, spawnPositionList[5].position.y);
                    GameObject currEnemy = Instantiate(enemy, _viewportPos + new Vector3(xPosition, yPosition, 0f), Quaternion.identity);
            
                    // add enemy to list of enemies
                    ScoreCounter.ScoreInstance.AddEnemy(currEnemy);
                }
                break;
        }
    }
}
