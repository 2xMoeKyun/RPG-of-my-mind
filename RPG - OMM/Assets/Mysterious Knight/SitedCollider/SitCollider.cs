using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitCollider : MonoBehaviour
{
    public Health playerHealth;

    private void Update()
    {
        if (Health.HitTaken != 0)
        {
            playerHealth.health -= Health.HitTaken;
            Health.HitTaken = 0;
        }
    }
}
