using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfig;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {

        do
        {

            yield return StartCoroutine(AllEnemyWaves());
        }
        while (looping);
    }

    private IEnumerator AllEnemyWaves()
    {
        for (int waveCount = startingWave; waveCount < waveConfig.Count; waveCount++)
        {
            var currentWave = waveConfig[waveCount];
            yield return StartCoroutine(AllSpawnEnemyWave(currentWave));
                
        }
        
    }

    private IEnumerator AllSpawnEnemyWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetnumberEnemy(); enemyCount++ )
 
        {

          var newEnemy =  Instantiate(waveConfig.GetEnemyPrefab(), 
                          waveConfig.GetWaypoints()[0].transform.position,
                          Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GettimeToSpawn());

        }
    }



}
