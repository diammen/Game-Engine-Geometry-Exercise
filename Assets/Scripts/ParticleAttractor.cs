using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttractor : MonoBehaviour
{
    public float radius;
    public float attractStrength;

    List<ParticleSystem> ps;
    ParticleSystem.Particle[] particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AttractParticles()
    {
        for (int i = 0; i < ps.Count; i++)
        {
            particles = new ParticleSystem.Particle[ps[i].particleCount];
            ps[i].GetParticles(particles);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ps.Add(other.GetComponent<ParticleSystem>());
    }
}
