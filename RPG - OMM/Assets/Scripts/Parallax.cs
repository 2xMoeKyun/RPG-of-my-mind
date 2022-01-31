using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform followingTarget;
    [Range(0f, 1f)] public float ParallaxStrength = 0.1f;
    Vector3 TargetpreviousPosition;
    void Start()
    {
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;   
        }
        TargetpreviousPosition = followingTarget.position;
    }

    void Update()
    {
        Vector3 delta = followingTarget.position - TargetpreviousPosition;
        TargetpreviousPosition = followingTarget.position;
        transform.position += delta * ParallaxStrength;
    }
}
