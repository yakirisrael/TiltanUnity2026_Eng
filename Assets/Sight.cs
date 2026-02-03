using UnityEngine;

public class Sight : MonoBehaviour
{

    [SerializeField]
    private float sightDistance = 5.0f;

    [SerializeField] private LayerMask sightMask;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public bool IsSee()
    {
        Debug.DrawLine((Vector2)transform.position, Vector2.left * sightDistance, Color.red);
        RaycastHit2D hit =  Physics2D.Raycast((Vector2)transform.position, Vector2.left, sightDistance, sightMask);
        if (hit.collider != null)
        {
          //  Debug.Log(hit.collider.name);
            Debug.DrawLine((Vector2)transform.position, Vector2.left * hit.distance, Color.green);
        }

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSee())
            Debug.Log("saw player");
    }
}
