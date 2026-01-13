using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public int HP = 100;
    public int MaxHP = 500;
    
    
    float sum = 0;

    Animator animator;
    
    private Vector3 originalScale;
    
    public UI_Manager UIManager;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        originalScale = transform.localScale;
        
        UIManager.GetComponent<UI_Manager>().UpdateHealth(HP, MaxHP);
    }

    
    // Update is called once per frame
    void Update()
    {
      //  sum += Time.deltaTime;
     

      float horizonatlValue =  Input.GetAxisRaw("Horizontal");
        float verticalValue =  Input.GetAxisRaw("Vertical");
        
       // Debug.Log(Input.GetAxisRaw("RotatePlayer"));

        transform.position += Vector3.right * horizonatlValue * moveSpeed * Time.deltaTime;
        transform.position += Vector3.up * verticalValue * moveSpeed * Time.deltaTime;

        if (horizonatlValue != 0 || verticalValue != 0)
        {
            animator.SetBool("Walking", true);
            
            float x =  horizonatlValue < 0 ? originalScale.x * -1.0f : originalScale.x;
           
            transform.localScale = new Vector3(
                x, // x - depending on direction
                originalScale.y, // y
                originalScale.z); //z
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        /* if (Input.GetAxisRaw("RotatePlayer") == 1)
         {
             transform.rotation = Quaternion.Euler(new Vector3(
                 0,
                 0,
                 moveSpeed * Time.realtimeSinceStartup));
         }*/
       /*   transform.position += new Vector3(
              horizonatlValue * moveSpeed * Time.deltaTime, // X
              verticalValue * moveSpeed * Time.deltaTime, //Y
              0); // Z */

    }
    
    public void DealDamage(int damage)
    {
        HP -= damage;
        HP = Mathf.Clamp(HP, 0, MaxHP);
    
        UIManager.GetComponent<UI_Manager>().UpdateHealth(HP, MaxHP);
    }
}
