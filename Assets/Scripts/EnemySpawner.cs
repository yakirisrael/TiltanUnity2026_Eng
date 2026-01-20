using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public int numEnemies = 5;
    public float spaceBetweenEnemies = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab, new Vector3(i * spaceBetweenEnemies, 0, 0), Quaternion.identity);
            Destroy(enemy, 5.0f);
        }
        
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
