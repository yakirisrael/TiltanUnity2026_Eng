using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] EnemyPrefab;

    public int numEnemies = 5;
    public int numBursts = 2;
    public float spaceBetweenEnemies = 0.5f;
    public float delayBetweenEnemies = 2.0f;
    public float delayBetweenBursts = 3.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnBurst());
    }

    IEnumerator SpawnBurst()
    {
        for (int i = 0; i < numBursts; i++)
        {
            yield return StartCoroutine(SpawnEnemies());
            
            yield return new WaitForSeconds(delayBetweenBursts);
        }

    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            int index = Random.Range(0, EnemyPrefab.Length);
            GameObject enemy = Instantiate(EnemyPrefab[index], new Vector3(i * spaceBetweenEnemies, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(delayBetweenEnemies);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
