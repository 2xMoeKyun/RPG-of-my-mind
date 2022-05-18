using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public void TimeStop()
    {
        Time.timeScale = 0f;
    }
}
