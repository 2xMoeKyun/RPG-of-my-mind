using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rainbow : MonoBehaviour
{
    public float rainbowSpeed;


    private float hue;
    private float saturation;
    private float brightness;



    private void Update()
    {
        Color.RGBToHSV(GetComponent<Image>().color, out hue, out saturation, out brightness);
        hue += rainbowSpeed / 10000;
        if(hue >= 1)
        {
            hue = 0;
        }
        saturation = 1;
        brightness = 1;

        GetComponent<Image>().color = Color.HSVToRGB(hue, saturation, brightness);
    }
}
