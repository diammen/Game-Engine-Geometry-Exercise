using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BleedOut : MonoBehaviour
{
    public float health;

    PostProcessVolume m_Volume;
    Vignette m_Vignette;
    ColorGrading m_ColorGrading;
    float maxHealth;

    void Start()
    {
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(1f);
        m_Vignette.smoothness.Override(0.5f);
        m_Vignette.color.Override(Color.red);

        //m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);

        m_ColorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        m_ColorGrading.enabled.Override(true);
        m_ColorGrading.gradingMode.Override(GradingMode.LowDefinitionRange);
        m_ColorGrading.colorFilter.Override(Color.red);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, new PostProcessEffectSettings[2] { m_Vignette, m_ColorGrading });

        maxHealth = health;
    }

    void Update()
    {
        m_Vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup * 5) * (Mathf.Abs(health - maxHealth) / maxHealth);
        m_ColorGrading.colorFilter.value = new Color(1, health / maxHealth, health / maxHealth);

        if (health > maxHealth)
            health = maxHealth;
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}