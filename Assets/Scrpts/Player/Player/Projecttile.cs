﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projecttile : MonoBehaviour
{
    Rigidbody2D bulletBody;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        bulletBody = GetComponent<Rigidbody2D>();
        bulletBody.AddForce(new Vector2(bulletSpeed,0),ForceMode2D.Impulse);
        Invoke("SelfDestroy", 5);

    }
    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            Destroy(gameObject);

    }
}
