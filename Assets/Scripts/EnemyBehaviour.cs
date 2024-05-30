using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public Rigidbody2D playerRB;
    Animator animator;

    Vector2 velocity;
    Vector2 lastPos;
    float targetDistance;
    Vector2 direction;

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float sightDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        targetDistance = Vector2.Distance(transform.position, playerRB.transform.position);
        velocity = (rb.position -lastPos) * Time.deltaTime;
        lastPos = rb.position;
        //Debug.Log(velocity);

        animator.SetFloat("xVelocity", velocity.x);
        animator.SetFloat("yVelocity", velocity.y);
        animator.SetFloat("xVelocity", velocity.x);
        animator.SetFloat("xDirection", direction.x);
        animator.SetFloat("yDirection", direction.y);

    }
    void FixedUpdate()
    {
        if (targetDistance < sightDistance)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, playerRB.position, moveSpeed * Time.fixedDeltaTime));
            direction = playerRB.position - rb.position;
        } 
        Debug.Log(direction);
    }
}
