using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWaveIndex = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);

    }

    private IEnumerator SpawnAllWaves()
    {
       
        for (int i = startingWaveIndex; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            yield return new WaitForSeconds(3f);
        }
        
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
       
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position, 
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
