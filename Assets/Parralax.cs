using UnityEngine;

public class Parralax : MonoBehaviour
{
    [SerializeField] private float parralaxFactor = 1;
    
    Vector3 prevPosition = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        prevPosition = Camera.main.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = Camera.main.transform.position - prevPosition;
        this.transform.position += new Vector3(deltaMovement.x * parralaxFactor, 0, 0);
        prevPosition = Camera.main.transform.position;
    }
}
