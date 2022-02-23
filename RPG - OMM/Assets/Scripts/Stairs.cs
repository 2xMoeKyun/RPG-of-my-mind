using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public float speed;


    private void Update()
    {
        CheckLadder();
        
        MoveOnLadder();
    }


    float CheckRadius = 0.01f;
    public Transform LadderCheck;
    public Transform BottomCheck;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(LadderCheck.position, CheckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(BottomCheck.position, CheckRadius);
    }

    public LayerMask Ladder;
    bool checkedLadder;
    bool checkedBottom;
    private void CheckLadder()
    {
        checkedLadder = Physics2D.OverlapPoint(LadderCheck.position, Ladder);
        checkedBottom = Physics2D.OverlapPoint(BottomCheck.position, Ladder);
    }

    float posY;
    private void MoveOnLadder()
    {
        if (checkedLadder || checkedBottom)
        {
            posY = Input.GetAxis("Vertical");
            Move.playerRb.bodyType = RigidbodyType2D.Kinematic;
            if (!checkedLadder && checkedBottom)//сверху лестницы
            {
                if (posY < 0)
                {
                    Move.playerRb.velocity = Vector2.down * speed;
                }
            }
            else if (checkedLadder && checkedBottom)//где-то в лестнице
            {
                Move.playerRb.velocity = new Vector2(0, posY * speed);
            }
            else if (checkedLadder && !checkedBottom)//снизу
            {
                if (posY > 0)
                {
                    Move.playerRb.velocity = Vector2.up * speed;
                }
            }
        }
        else
        {
            Move.playerRb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

}
