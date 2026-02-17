using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

enum EnemyState
{
    Unware,
    ChasePlayer,
    Attack,
    WaitForAttack,
    ReturnToPosition,
    Die
}

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;

    public float delta = 1.0f;
    public float deadZone = 0.4f;
    
    [Min(0)]
    [Tooltip("This is the damage the enemy can make")]
    [SerializeField]
    private int damage = 10;

    private Vector3 originalScale;

    private float timeFromLastPunch = 0;
    public float delayBetweenAttacks = 1.5f;

    private EnemyState state;
    
    Animator animator = null;

    private Sight sight;
    
    [Space(50)]
    [Header("Sound Effects")]
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip hitClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
      //  animator = GetComponent<Animator>();
        sight = GetComponent<Sight>();
        animator = GetComponent<Animator>();
    }

    private void OnValidate()
    {
        if (animator == null) 
        {
            animator = GetComponent<Animator>();
        }
    }

    void Start()
    {
        originalScale = transform.localScale;

        state = EnemyState.Unware;
        
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
        //Debug.Log(distance);

        if (sight.IsSee() && distance > deadZone) // enemy chase the player
        {
            transform.position += direction * delta * Time.deltaTime;
            animator.SetBool("Walking", true);
            
            state = EnemyState.ChasePlayer;
        }
        else   // near the player
        {
            animator.SetBool("Walking", false);

            if (IsAnimationFinished("Punching"))
            {
               
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
            if (player != null && state == EnemyState.ChasePlayer)
            {
                StartCoroutine(WaitThenAttack(player, 2));
            }

          //  Debug.Log("Collided with " + other.name);
        }

       
    }

    IEnumerator WaitThenAttack(PlayerMovement player, float waitTime)
    {
        while (true)
        {
            state = EnemyState.WaitForAttack;
            yield return new WaitForSeconds(waitTime);

            state = EnemyState.Attack;
            Attack(player);
            
            while (!IsAnimationFinished("Punching"))
                yield return null;
        }
        
    }

    void Attack(PlayerMovement player)
    {
        // near the player
        animator.SetTrigger("Punching");
        player.DealDamage(damage);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerZone"))
        {
            //Debug.Log("Exit collision with " + other.name);
        }
    }

    public void PlayAttackSFX(AudioClip clip)
    {
        AudioManager.Instance.PlaySFX(clip);
    }
}
