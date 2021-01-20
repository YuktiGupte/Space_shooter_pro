using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //variable definitions
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] powerup;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawn;
   
    public void StartSpawning()
    {
        //powerups and enemies start spawn call
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    //spawn routine for enemies
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        //while loop
        while (_stopSpawn == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8.0f, 8.0f), 5.5f, 0);
            //instantiate prefab
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            //wait 5 seconds
            yield return new WaitForSeconds(5.0f);
        }
    }
    //powerup spawn routine
    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        while(_stopSpawn == false)
        {
            Vector3 spawnppos = new Vector3(Random.Range(-8.0f, 8.0f), 5.5f, 0);
            int randomPowerUp = Random.Range(0, 3); 
            GameObject newPowerup = Instantiate(powerup[randomPowerUp], spawnppos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));

        }
    }
    //change the "stop spawn" trigger to true to stop spawning when the player dies
    public void OnPlayerDeath()
    {
        _stopSpawn = true;
    }
}


