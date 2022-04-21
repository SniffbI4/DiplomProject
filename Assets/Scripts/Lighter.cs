using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    [SerializeField] float nightIntencity;
    [SerializeField] float dayIntencity;
    private float targetIntencity;
    private Light light;

    private void Start()
    {
        light = GetComponent<Light>();

        Transformer.OnElectricityChanged += Transformer_OnElectricityChanged;
        DayNightChanger.OnDayStarted += DayNightChanger_OnDayStarted;
        DayNightChanger.OnDayEnded += DayNightChanger_OnDayEnded;
    }

    private void DayNightChanger_OnDayEnded()
    {
        //light.enabled = true;
        targetIntencity = nightIntencity;
    }

    private void DayNightChanger_OnDayStarted()
    {
        //light.enabled = false;
        targetIntencity = dayIntencity;
    }

    private void Transformer_OnElectricityChanged(bool electricityState)
    {
        if (light.enabled == electricityState)
            return;

        light.enabled = electricityState;

        //if (electricityState)
        //    light.intensity = nightIntencity;
        //else
        //    light.intensity = 0;

        //state = electricityState;
    }

    private void Update()
    {
        if (light.intensity == targetIntencity || !Transformer.Electricity)
            return;

        light.intensity = Mathf.Lerp(light.intensity, targetIntencity, Time.deltaTime);
    }
}
