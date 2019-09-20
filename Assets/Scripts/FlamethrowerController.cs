using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerController : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.EmissionModule em;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        em = ps.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            em.enabled = true;
        }
        else
        {
            em.enabled = false;
        }
    }
}
