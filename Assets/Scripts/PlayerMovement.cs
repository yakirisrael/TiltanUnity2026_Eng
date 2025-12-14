using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    float sum = 0;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  sum += Time.deltaTime;

        
        float horizonatlValue =  Input.GetAxisRaw("Horizontal");
        float verticalValue =  Input.GetAxisRaw("Vertical");
        
        Debug.Log(Input.GetAxisRaw("RotatePlayer"));

       // transform.position += Vector3.up * moveSpeed * Time.deltaTime;

       if (Input.GetAxisRaw("RotatePlayer") == 1)
       {
           transform.rotation = Quaternion.Euler(new Vector3(
               0,
               0,
               moveSpeed * Time.realtimeSinceStartup));
       }
       /*   transform.position += new Vector3(
              horizonatlValue * moveSpeed * Time.deltaTime, // X
              verticalValue * moveSpeed * Time.deltaTime, //Y
              0); // Z */

    }
}
