using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimmer : MonoBehaviour
{
    public bool isOn;
    public float maxIntensity;
    public float duration;

    private Light currentLight;
    private float currentIntensity;
    [SerializeField]
    private float elapsed;

    // Start is called before the first frame update
    void Start()
    {
        currentLight = GetComponent<Light>();

        elapsed = 0;
        currentLight.intensity = 0;
        currentIntensity = currentLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            elapsed += Time.deltaTime;

            if (elapsed >= duration)
            {
                elapsed = duration;
            }

            currentLight.intensity = Mathf.Lerp(0, maxIntensity, (elapsed / duration) >= 1 ? 1 : (elapsed / duration));
        }
        else
        {
            elapsed -= Time.deltaTime;

            if (elapsed <= 0)
            {
                elapsed = 0;
            }

            currentLight.intensity = Mathf.Lerp(0, maxIntensity, (elapsed / duration) >= 1 ? 1 : (elapsed / duration));
        }
    }
}
