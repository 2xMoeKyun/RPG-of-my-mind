using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOff : MonoBehaviour
{
    public GameObject sunLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sunLight.SetActive(false);
    }

}
