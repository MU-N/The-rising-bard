using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavoir : MonoBehaviour
{
    [SerializeField] Transform m_rayCast;
    [SerializeField] LayerMask m_rayCastMask;
    [SerializeField] float m_rayCastLenght;
    [SerializeField] float m_attackDistance; // Minimum distance for attack
    [SerializeField] float m_moveSpeed;
    [SerializeField] float m_timer; // time of cooldown between attacks

    RaycastHit2D m_hit;
    Transform m_target;
    Animator m_animtor;
    float m_distance; // distance between the enemy and player
    bool m_attackMode;
    bool m_inRange;  //check the player in range
    bool m_cooling; // check if the enemy is cooling after attack
    private float m_intTimer;

    Vector2 rayCastDirection = Vector2.left;

    private void Awake()
    {
        m_intTimer = m_timer;
        m_animtor = GetComponent<Animator>();
    }

    void Update()
    {
       // Debug.Log(m_inRange);
        RaycastDebugger();
        if (m_inRange)
        {
            //m_hit = Physics2D.Raycast(m_rayCast.position, rayCastDirection, m_rayCastLenght, m_rayCastMask);
            m_hit = Physics2D.BoxCast(m_rayCast.position, new Vector2(2, 2), 0.0f, rayCastDirection, 5, m_rayCastMask);
            //RaycastDebugger();
        }

        if (m_hit.collider != null)
        {
            EnemyLogic();
        }
        else if (m_hit.collider == null)
        {
            m_inRange = false;
        }

        if (m_inRange == false)
        {
            m_animtor.SetBool("canWalk", false);
            StopAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_target = collision.gameObject.transform;
            m_inRange = true;
            Flip();
        }
    }

    

    void EnemyLogic()
    {
        m_distance = Vector2.Distance(transform.position, m_target.transform.position);

        if (m_distance > m_attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (m_attackDistance >= m_distance && m_cooling == false)
        {
            Attack();
        }

        if (m_cooling)
        {
            Cooldown();
            m_animtor.SetBool("Attack", false);
        }
    }

    void Move()
    {
        m_animtor.SetBool("canWalk", true);
        if (!m_animtor.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            Vector2 targetPosition = new Vector2(m_target.position.x, m_target.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, m_moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        m_timer = m_intTimer;
        m_attackMode = true;

        m_animtor.SetBool("canWalk", false);
        m_animtor.SetBool("Attack", true);
    }

    void Cooldown()
    {
        m_timer -= Time.deltaTime;

        if (m_timer <= 0 && m_cooling && m_attackMode)
        {
            m_cooling = false;
            m_timer = m_intTimer;
        }
    }

    void StopAttack()
    {
        m_cooling = false;
        m_attackMode = false;

        m_animtor.SetBool("Attack", false);
    }


    void RaycastDebugger()
    {
        if (m_distance > m_attackDistance)
        {
            Debug.DrawRay(m_rayCast.position, rayCastDirection * m_rayCastLenght, Color.red);
        }
        else if (m_attackDistance > m_distance)
        {
            Debug.DrawRay(m_rayCast.position, rayCastDirection * m_rayCastLenght, Color.green);
        }
    }

    public void TriggerCooling()
    {
        m_cooling = true;
    }

    void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > m_target.position.x)
        {
            rotation.y = 180.0f;
            rayCastDirection = Vector2.left;
        }
        else
        {
            rotation.y = 0.0f;
            rayCastDirection = Vector2.right;
        }

        transform.eulerAngles = rotation;
    }
}