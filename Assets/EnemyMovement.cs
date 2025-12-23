using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;

    public float delta = 1.0f;
    public float deadZone = 0.4f;

    private Vector3 originalScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 enemyPosition = transform.position;
        Vector3 difference = playerPosition - enemyPosition;
        Vector3 direction = difference.normalized;

        float distance = difference.magnitude;
        Debug.Log(direction);
        
        if (distance > deadZone)
            transform.position += direction * delta * Time.deltaTime;

           float x = direction.x > 0 ? originalScale.x * -1.0f : originalScale.x;
           
            transform.localScale = new Vector3(
                x, // x - depending on direction
                originalScale.y, // y
                originalScale.z); //z
        


    }
}
