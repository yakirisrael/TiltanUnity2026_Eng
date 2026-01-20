using System;
using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;

    public float delta = 1.0f;
    public float deadZone = 0.4f;
    public int damage = 10;

    private Vector3 originalScale;

    private float timeFromLastPunch = 0;
    public float delayBetweenAttacks = 1.5f;
    
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        originalScale = transform.localScale;
        
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 enemyPosition = transform.position;
        Vector3 difference = playerPosition - enemyPosition;
        Vector3 direction = difference.normalized;

        float distance = difference.magnitude;
        //Debug.Log(direction);

        if (distance > deadZone) // enemy chase the player
        {
            transform.position += direction * delta * Time.deltaTime;
            animator.SetBool("Walking", true);
        }
        else // near the player
        {
            animator.SetBool("Walking", false);

            if (IsAnimationFinished("Punching"))
            {
                if (timeFromLastPunch < delayBetweenAttacks)
                {
                    timeFromLastPunch += Time.deltaTime;
                }
                else // had enough pause between the attacks
                {
                    //animator.SetTrigger("Punching");
                    timeFromLastPunch = 0;
                    
                   // player.GetComponent<PlayerMovement>().DealDamage(damage);
                }

                //Debug.Log(timeFromLastPunch);
            }
            else
            {
                animator.SetTrigger("Hurt");
            }
        }

        float x = direction.x > 0 ? originalScale.x * -1.0f : originalScale.x;
           
            transform.localScale = new Vector3(
                x, // x - depending on direction
                originalScale.y, // y
                originalScale.z); //z
    }

    bool IsAnimationFinished(string animationName)
    {
       AnimatorStateInfo info =  animator.GetCurrentAnimatorStateInfo(0);

       if (info.IsName(animationName))
       {
           if (info.normalizedTime >= 0.95f)
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       
       return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerZone"))
        {
            PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
            if (player != null)
            {
                animator.SetTrigger("Punching");
                player.DealDamage(damage);
            }

            Debug.Log("Collided with " + other.name);
        }

       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerZone"))
        {
            Debug.Log("Exit collision with " + other.name);
        }
    }
}
