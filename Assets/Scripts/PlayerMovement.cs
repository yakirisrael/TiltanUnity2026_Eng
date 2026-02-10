using System;
using Unity.VisualScripting;
using UnityEngine;

enum PlayerState
{
    Idle,
    Walking,
    Attacking,
    Jumping
}

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public int HP = 100;
    public int MaxHP = 500;

    public Collider2D NavArea;
    public GameObject Feet;
    
    float sum = 0;

    Animator animator;
    
    private Vector3 originalScale;
    
    public UI_Manager UIManager;

    Rigidbody2D rb;
    
    public float jumpForce = 1f;

    private PlayerState state;

    
    [SerializeField]
    private float jumpDistance = 0.1f;
    
    private bool isJumping = false;
    
    [SerializeField]
    private Transform FeetTransform;
    
    [SerializeField]
    private LayerMask GroundMask;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        originalScale = transform.localScale;
        
        state =  PlayerState.Idle;
        UIManager.GetComponent<UI_Manager>().UpdateHealth(HP, MaxHP);
    }

    
    // Update is called once per frame
    void Update()
    {
      //  sum += Time.deltaTime;
     
      int a = 1;
    //  TEST_ASSERT( a == 1 ); //this one will pass
     // TEST_ASSERT( a == 2 ); //this one will fail

          if (hurt)
          {
             
              HP = (int)Mathf.Lerp(HP, target, Time.deltaTime * 0.001f);
              UIManager.GetComponent<UI_Manager>().UpdateHealth(HP, MaxHP);
              
              if (Mathf.Approximately(HP, target))
                  hurt = false;
          }
      
        float horizonatlValue =  Input.GetAxisRaw("Horizontal");
        float verticalValue =  Input.GetAxisRaw("Vertical");

        isJumping = IsJumping();
        
        Jump();
       // Debug.Log(Input.GetAxisRaw("RotatePlayer"));

       Vector3 deltaX =  Vector3.right * horizonatlValue * moveSpeed * Time.deltaTime;
       Vector3 deltaY =  Vector3.up * verticalValue * moveSpeed * Time.deltaTime;
      
       StayInNavArea(deltaX, deltaY);

       if (horizonatlValue != 0 || verticalValue != 0)
       {
           if (state != PlayerState.Jumping)
                state = PlayerState.Walking;
           
            animator.SetBool("Walking", true);
            
            float x =  horizonatlValue < 0 ? originalScale.x * -1.0f : originalScale.x;
           
            transform.localScale = new Vector3(
                x, // x - depending on direction
                originalScale.y, // y
                originalScale.z); //z
        }
        else
        {
            if (state != PlayerState.Jumping)
                state =  PlayerState.Idle;
            
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
       
       
       if (state == PlayerState.Jumping && rb.linearVelocity.y == 0)
           state =  PlayerState.Idle;
       
            //Debug.Log(rb.linearVelocity.y);

    }

    private void StayInNavArea(Vector3 deltaX, Vector3 deltaY)
    {
        Vector3 target = Feet.transform.position + deltaX + deltaY;
        Vector3 NewPosition = transform.position + deltaX + deltaY;
        if (NavArea.OverlapPoint(target))
            transform.position = NewPosition;
    }

    public void DealDamage(int damage)
    {
        HP -= damage;
        HP = Mathf.Clamp(HP, 0, MaxHP);
    
        UIManager.GetComponent<UI_Manager>().UpdateHealth(HP, MaxHP);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("Collided with " + other.name);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      //  Debug.Log("Exit collision with " + other.name);
    }


    bool IsJumping()
    {
        Debug.DrawRay(FeetTransform.position, Vector3.down * jumpDistance, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(FeetTransform.position, Vector2.down, jumpDistance, GroundMask);
        if (hit.collider != null)
        {
           
            return false;
        }
        Debug.Log("in the air");
        return true;
    }

    bool hurt = false;
    private float target;
    void Jump()
     {
         if (Input.GetButtonDown("Jump") &&  state != PlayerState.Jumping)
         {
             hurt = true;
             target = HP - 5;
             

             state = PlayerState.Jumping;
             rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
         }
     }
}
