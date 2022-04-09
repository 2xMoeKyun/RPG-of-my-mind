using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOn : MonoBehaviour
{
    public GameObject sunLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sunLight.SetActive(true);
    }

}
