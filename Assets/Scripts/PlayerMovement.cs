using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMovement : MonoBehaviour
{

    public float initialMoveSpeed;
    public float attackDir;
    private float currentMoveSpeed;

    private Rigidbody2D rb;
    private Animator animator;
    public GameObject arrow;
    List<GameObject> arrows;

    public float fireRate;
    private float timeToFire;

    Vector2 movement;
    float attack;
    

    // Start is called before the first frame update
    void Start()
    {
        currentMoveSpeed = initialMoveSpeed;
        rb = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();
        attackDir = 0;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        attack = Input.GetAxisRaw("Fire1");


        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("AtkDir", attackDir);
        animator.SetFloat("Atk", attack);

        AnimatorStateInfo currentAnimation = animator.GetCurrentAnimatorStateInfo(0);

        Attack();

        if (!currentAnimation.IsName("Attack")) 
        {
            Debug.Log("true");
            if (movement.x == -1)
            {
                attackDir = 1;
            }
            if (movement.y == 1)
            {
                attackDir = 2;
            }
            if (movement.x == 1)
            {
                attackDir = 3;
            }
            if (movement.y == -1)
            {
                attackDir = 0;
            }
        }

        // Debug.Log(attack + "Attack");
        // Debug.Log(attackDir + "Attack direction");
        // Debug.Log(movement + "Axis");
        //Debug.Log(Time.time);
        //arrow.GetComponent<Rigidbody2D>().MovePosition(new Vector2(0, 1 * Time.deltaTime));
        // Debug.Log(rb.velocity);


        if (currentAnimation.IsName("Attack"))
        {
            currentMoveSpeed = 0;
        }
        else
        {
            currentMoveSpeed = initialMoveSpeed;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * currentMoveSpeed * Time.fixedDeltaTime);
    }

    void Attack()
    {   
        if (attack > 0.01 && Time.time >= timeToFire) 
        { 
            Instantiate(arrow);
/*            if (attackDir == 0)
            {
            }*/
            timeToFire = Time.time + fireRate;
        }
           // arrow.GetComponent<Rigidbody2D>().MovePosition(rb.position + new Vector2(0,-1) * 2f * Time.fixedDeltaTime);
    }
}
