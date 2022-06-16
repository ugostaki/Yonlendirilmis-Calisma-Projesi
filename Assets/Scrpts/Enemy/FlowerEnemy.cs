using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerEnemy : MonoBehaviour
{
    Rigidbody2D enemyBody2D;
    public float enemySpeed;
    EnemyHealth enemyHealth;
    Animator flowerEnemyAnim;
    // Duvarı Bulma

     bool isGrounded;
    Transform groundCheck;
    const float graundCheckRadius = 0.2f;
    [Tooltip("duvarın ne olduğunu belirler")]
    public LayerMask groundLayer;
    public bool moveRight;

    //Ucurum Bulma
    bool onEdge;
    Transform edgeCheck;


    void Start()
    {
        enemyBody2D = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        edgeCheck = transform.Find("EdgeCheck");
        flowerEnemyAnim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
       

        //duvara değiyor muyuz diye bak
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, graundCheckRadius, groundLayer);

        onEdge = Physics2D.OverlapCircle(edgeCheck.position, graundCheckRadius, groundLayer);

        if (isGrounded || !onEdge)
            moveRight = !moveRight;

        enemyBody2D.velocity = (moveRight) ? new Vector2(enemySpeed, 0) : new Vector2(-enemySpeed, 0);
        transform.localScale = (moveRight) ? new Vector2(-1, 1) : new Vector2(1, 1);

       
    }
}
