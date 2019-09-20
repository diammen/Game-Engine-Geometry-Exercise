using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitcher : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color color1, color2, color3;

    List<Renderer> lights = new List<Renderer>();
    Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        var lightObjects = GameObject.FindGameObjectsWithTag("Light");
        for (int i = 0; i < lightObjects.Length; i++)
        {
            lights.Add(lightObjects[i].GetComponent<Renderer>());
        }

        colors = new Color[] { color1, color2, color3 };
    }

    public void ChangeLightColor(int c)
    {
        if (c < colors.Length)
        {
            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].material.SetColor("_EmissionColor", colors[c]);
            }
        }
    }
}
