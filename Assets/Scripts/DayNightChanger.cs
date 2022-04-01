using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightChanger : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float TimeOfDay;
    [SerializeField] float DayDuration=30f;

    [SerializeField] private AnimationCurve SunCurve;
    [SerializeField] private AnimationCurve MoonCurve;
    [SerializeField] private AnimationCurve SkyboxCurve;
    [SerializeField] private Material DaySkybox;
    [SerializeField] private Material NightSkybox;

    [SerializeField] private Light sun;
    [SerializeField] private Light moon;

    public delegate void ChangeDayToNight();
    public static event ChangeDayToNight OnDayStarted;
    public static event ChangeDayToNight OnDayEnded;

    private float sunIntencity;
    private float moonIntencity;

    private void Start()
    {
        sunIntencity = sun.intensity;
        moonIntencity = moon.intensity;
    }

    private void Update()
    {
        TimeOfDay += Time.deltaTime / DayDuration;
        if (TimeOfDay >= 1) TimeOfDay -= 1;

        RenderSettings.skybox.Lerp(NightSkybox, DaySkybox, SkyboxCurve.Evaluate(TimeOfDay));

        if (SkyboxCurve.Evaluate(TimeOfDay) > 0.1f)
        {
            RenderSettings.sun = sun;
            OnDayStarted();
        }
        else
        {
            RenderSettings.sun = moon;
            OnDayEnded();
        }
        DynamicGI.UpdateEnvironment();

        sun.intensity = sunIntencity * SunCurve.Evaluate(TimeOfDay);
        moon.intensity = moonIntencity * MoonCurve.Evaluate(TimeOfDay);
    }
}
