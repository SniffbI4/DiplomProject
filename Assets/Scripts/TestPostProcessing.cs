using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TestPostProcessing : MonoBehaviour
{
    [SerializeField, Range(0,1)] private float maxVignetteValue;
    [SerializeField, Range(0, 100)] private float maxBlackAndWhiteEffect;

    private PostProcessVolume postProcessVolume;
    private Vignette vignette;
    private ColorGrading colorGrading;
    private Health health;

    private bool isFull = false;

    private void Awake()
    {
        health = GetComponent<Health>();
        postProcessVolume = FindObjectOfType<PostProcessVolume>();
    }

    private void OnEnable()
    {
        health.OnHealthChanged += Health_OnHealthChanged;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= Health_OnHealthChanged;
    }

    private void Start()
    {
        postProcessVolume.profile.TryGetSettings(out vignette);
        postProcessVolume.profile.TryGetSettings(out colorGrading);
    }

    private void Health_OnHealthChanged(float defuse, bool isDamaged)
    {
        if (!isDamaged)
            return;

        float value = 1 - defuse;
        if (value >= 1)
            isFull = true;

        vignette.intensity.value = value*maxVignetteValue;
        colorGrading.saturation.value = value * (-maxBlackAndWhiteEffect);
        
        StopAllCoroutines();
        if (!isFull)
            StartCoroutine(FadingCuro(vignette.intensity.value, colorGrading.saturation.value, 5f));
    }

    /// <summary>
    /// Медленное выключение виньетки
    /// </summary>
    /// <param name="vignetteValue">Значение виньетки</param>
    /// <param name="saturationValue">Значение насыщенности</param>
    /// <param name="time">Время выключения</param>
    /// <returns></returns>
    private IEnumerator FadingCuro (float vignetteValue, float saturationValue, float time)
    {
        float k = 0f;
        float m = 0f;

        while ((k+=Time.deltaTime)<time)
        {
            vignette.intensity.value = vignetteValue - vignetteValue * k / time;

            if ((m += Time.deltaTime * 2) < time)
                colorGrading.saturation.value = saturationValue - saturationValue * m / time;

            yield return null;
        }
    }
}
