using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingPlatform : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    public float speed;

    int currentIndex;
    Vector2 currentPoint;
   
    private void Update()
    {
        Going();
    }

    bool first = true;
    void Going()
    {

        if(Vector3.Distance(transform.position, currentPoint) < 0.3f)
        {
            NextPoint();
        }
        if (first)
        {
            currentPoint = points[1].position;
            first = false;
        }
        transform.position = Vector2.MoveTowards(transform.position, currentPoint, speed * Time.deltaTime);

    }
    
    void NextPoint()
    {
        if(currentIndex + 1< points.Count)
        {
            currentIndex = currentIndex + 1;
        }
        else
        {
            currentIndex = 0;
        }
        currentPoint = points[currentIndex].position;
    }
}
