using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float speed = 1.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos2d = Vector2.Lerp(transform.position, target.transform.position, Time.deltaTime * speed);
        transform.position = new Vector3(pos2d.x, pos2d.y, transform.position.z);
    }
}
