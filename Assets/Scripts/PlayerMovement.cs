using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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

        if (movement.x == -1)
        {
            attackDir = 1;
        }
        if ( movement.y == 1)
        {
            attackDir = 2;
        }
        if(movement.x == 1)
        {
            attackDir = 3;
        }
        if (movement.y == -1)
        {
            attackDir = 0;
        }

        // Debug.Log(attack + "Attack");
        // Debug.Log(attackDir + "Attack direction");
        // Debug.Log(movement + "Axis");

        Debug.Log(rb.velocity);
       

        if(currentAnimation.IsName("Attack"))
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



}
