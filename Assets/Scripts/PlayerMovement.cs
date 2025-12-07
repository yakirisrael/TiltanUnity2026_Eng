using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizonatlValue =  Input.GetAxis("Horizontal");
        Debug.Log(horizonatlValue);

        int sign = 0;
        if (horizonatlValue > 0)
            sign = 1;
        if (horizonatlValue < 0)
            sign = -1;
         transform.position += new Vector3(sign * 0.01f, 0, 0);

    }
}
