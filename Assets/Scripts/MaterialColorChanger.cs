using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorChanger : MonoBehaviour
{
    public Color currentColor;

    private Renderer currentMaterial;

    // Start is called before the first frame update
    void Start()
    {
        currentColor = Color.white;
        currentMaterial = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentMaterial.material.color = currentColor;
    }
}
