using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroll : MonoBehaviour
{
    private Renderer currentMaterial;

    // Start is called before the first frame update
    void Start()
    {
        currentMaterial = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentMaterial.material.mainTextureOffset += new Vector2(Time.deltaTime, 0);
    }
}
