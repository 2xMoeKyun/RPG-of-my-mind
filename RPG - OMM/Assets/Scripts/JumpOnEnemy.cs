using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnEnemy : MonoBehaviour
{
    private Move m;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !Move.isGrounded)
        {
            Move.playerRb.velocity = new Vector2(Move.playerDirection*2, 5);
        }
    }
}
