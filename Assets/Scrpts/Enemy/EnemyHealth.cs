using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxEnemyHealth;
    public float currentEnemyHealth;
    internal bool gotDamage;
    public float damage;
    public float projecttileDamage;
    public GameObject deathParticle;
    SpriteRenderer graph;
    CircleCollider2D cir2D;
    Player player;
    Rigidbody2D body2D;

    //ses
    AudioSource au_Source;
    AudioClip ac_Dead;
   


    void Start()
    {
        
        currentEnemyHealth = maxEnemyHealth;
        player = FindObjectOfType<Player>();
        graph = GetComponent<SpriteRenderer>();
        cir2D = GetComponent<CircleCollider2D>();
        body2D = GetComponent<Rigidbody2D>();
        au_Source = GetComponent<AudioSource>();
        ac_Dead = Resources.Load("SoundEffects/DeadEnemy") as AudioClip;

    }

    
    void Update()
    {
        if (currentEnemyHealth <= 0)
        {
            graph.enabled = false;
            cir2D.enabled = false;
            deathParticle.SetActive(true);
            body2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            Destroy(gameObject,1);
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerItem" && player.canDamage) 
        {
            currentEnemyHealth -= damage;
            au_Source.PlayOneShot(ac_Dead);
          
        }
        if (collision.tag == "PlayerProjecttile" )
        {
            currentEnemyHealth -= projecttileDamage;
            au_Source.PlayOneShot(ac_Dead);
            Destroy(collision.gameObject);

        }
    }

}
