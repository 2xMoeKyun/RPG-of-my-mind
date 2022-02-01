using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    public Transform playerTransform;
    public float movespeed = 5;
    
    void Update()
    {
        if (playerTransform)
        {
            Vector3 target = new Vector3(playerTransform.position.x, playerTransform.position.y+1f, playerTransform.position.z - 10);
            Vector3 pos = Vector3.Lerp(transform.position, target, movespeed * Time.deltaTime);
            transform.position = pos;
        }
    }
}
