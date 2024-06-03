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
    public Vector2 attackDir;
    private float currentMoveSpeed;

    private Rigidbody2D rb;
    private Animator animator;
    public GameObject arrow;
    private GameObject newArrow;
    List<GameObject> arrowList = new List<GameObject>();

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
        attackDir = new Vector2(0,-1);
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
        animator.SetFloat("XAtkDir", attackDir.x);
        animator.SetFloat("YAtkDir", attackDir.y);
        animator.SetFloat("Atk", attack);

        AnimatorStateInfo currentAnimation = animator.GetCurrentAnimatorStateInfo(0);

        Attack();

        if (!currentAnimation.IsName("Attack")) 
        {
          
           if(movement != Vector2.zero)
            {
                attackDir = movement;
            }
            Debug.Log(attackDir);
        }

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
       /* if (arrowList.Count > 0)
        {
            for (int i = 0; i < arrowList.Count; i++)
            {
                arrowList[i].GetComponent<Rigidbody2D>().MovePosition(newArrow.GetComponent<Rigidbody2D>().position + attackDir);
            }
        }*/
    }

    void Attack()
    {
        if (attack > 0.01 && Time.time >= timeToFire)
        {
            newArrow = Instantiate(arrow);
            arrowList.Add(newArrow);
            newArrow.GetComponent<ArrowBehaviour>().direction = attackDir;

            if (attackDir == new Vector2(0, -1))
            {
                newArrow.GetComponentInParent<Rigidbody2D>().position = rb.position + new Vector2(0,-2);
                newArrow.GetComponent<Rigidbody2D>().rotation = 180;
            }
            if (attackDir == new Vector2(-1, 0))
            {
                newArrow.GetComponentInParent<Rigidbody2D>().position = rb.position + new Vector2(-2, 0);
                newArrow.GetComponent<Rigidbody2D>().rotation = 90;
            }
            if (attackDir == new Vector2(0, 1))
            {
                newArrow.GetComponentInParent<Rigidbody2D>().position = rb.position + new Vector2(0, 2);
            }
            if (attackDir == new Vector2(1, 0))
            {
                newArrow.GetComponentInParent<Rigidbody2D>().position = rb.position + new Vector2(2, 0);
                newArrow.GetComponent<Rigidbody2D>().rotation = 270;
            }

            timeToFire = Time.time + fireRate;
        }
        //Debug.Log(timeToFire);
    }
}
