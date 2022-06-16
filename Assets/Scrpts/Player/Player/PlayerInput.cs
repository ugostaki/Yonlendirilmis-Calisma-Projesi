using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = GetComponent<Player>();
    }
    void Update()
    {
        //eğer zıplama tuşuna (space) basarsak zıplama fonksiyonu çalışır.
        if (Input.GetButtonDown("Jump") && player.isGrounded)
        {
            player.jump();
            player.canDoubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && !player.isGrounded && player.canDoubleJump)
        {
            player.DoubleJump();
            player.canDoubleJump = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            player.ShootProjecttile();
        }
    }
}
