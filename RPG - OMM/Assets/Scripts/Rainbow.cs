using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rainbow : MonoBehaviour
{
    public float rainbowSpeed;
    public bool Image;
    public bool Text;

    private float hue;
    private float saturation;
    private float brightness;



    private void Update()
    {
        if (Image)
        {
            Color.RGBToHSV(GetComponent<Image>().color, out hue, out saturation, out brightness);
        }
        else if (Text)
        {
            Color.RGBToHSV(GetComponent<Text>().color, out hue, out saturation, out brightness);
        }
        hue += rainbowSpeed / 10000;
        if(hue >= 1)
        {
            hue = 0;
        }
        saturation = 1;
        brightness = 1;
        if (Image)
        {
            GetComponent<Image>().color = Color.HSVToRGB(hue, saturation, brightness);
        }
        else if (Text)
        {
            GetComponent<Text>().color = Color.HSVToRGB(hue, saturation, brightness);
        }
    }
}
