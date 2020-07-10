using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeToSpawn = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.2f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] int numberOfEnemy = 5;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {

            waveWaypoints.Add(child); 

        }

        return waveWaypoints;
    }

    

    public float GettimeToSpawn() { return timeToSpawn; }
    
    public float GetSpawnFactor() { return spawnRandomFactor; }

    public int GetnumberEnemy() { return numberOfEnemy; }

    public float GetmoveSpeed() { return moveSpeed; }
    
}