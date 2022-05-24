using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlace : MonoBehaviour
{
    public float PushAwayForce;
    private Trap trap;

    private void Start()
    {
        trap = transform.parent.GetComponent<Trap>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        trap.HitScan(collision);
        Move.playerRb.velocity = new Vector2(0, collision.transform.position.y + PushAwayForce);
    }
}
