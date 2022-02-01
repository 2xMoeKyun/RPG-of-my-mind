using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform followingTarget;
    [Range(0f, 1f)] public float ParallaxStrength = 0.1f;
    public bool disableVerticalParallax;
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
        if (disableVerticalParallax)
        {
            delta.y = 0;
        }
        TargetpreviousPosition = followingTarget.position;
        transform.position += delta * ParallaxStrength;
    }
}
