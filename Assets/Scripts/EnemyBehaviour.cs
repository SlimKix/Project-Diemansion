using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public Rigidbody2D playerRB;

    Vector2 velocity;

    float targetDistance;

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float sightDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        targetDistance = Vector2.Distance(transform.position, playerRB.transform.position);
        
    }
    void FixedUpdate()
    {
        if (targetDistance < sightDistance)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, playerRB.position, moveSpeed * Time.fixedDeltaTime));

        }   
    }
}
