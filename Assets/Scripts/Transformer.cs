using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{
    [SerializeField] private GameObject PressEPanel;
    [SerializeField] private GameObject HoldEPanel;

    [SerializeField] private float repairTime;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem[] longParticles;
    [SerializeField] private AudioSource fireAudio;

    public static bool Electricity = true;
    private static int HowMuchTransformersIsBroken = 0;
    private static int HowMuchTransformersIsOff = 0;

    public bool isTurnOff;
    public bool isBroken;
    private bool playerIsNear = false;
    private float TimeToOff;

    public delegate void ElectricityChange(bool electricityState);
    public static event ElectricityChange OnElectricityChanged;

    public delegate void TransformerChange(Vector3 position);
    public static event TransformerChange OnTransformerOff;

    private void Start()
    {
        TimeToOff = Random.Range(20, 40);
    }

    private void Update()
    {
        if (Electricity)
        {
            TimeToOff -= Time.deltaTime;
            if (TimeToOff <= 0)
            {
                TurnOFF();
            }
        }
        else TimeToOff = Random.Range(30, 60);

        if (playerIsNear)
        {
            if (isBroken)
                HoldEPanel.SetActive(true);
            else if (isTurnOff)
                PressEPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isBroken)
                {
                    Repair();
                }
                else if (isTurnOff)
                        TurnON();
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (isBroken)
                    StopAllCoroutines();
            }
        }
    }

    private void SwitchElectricity ()
    {
        if (HowMuchTransformersIsBroken == 0 && HowMuchTransformersIsOff == 0)
        {
            Electricity = true;
        }
        else
            Electricity = false;

        if (OnElectricityChanged != null)
            OnElectricityChanged(Electricity);
    }

    public void TurnON ()
    {
        if (!isTurnOff || isBroken)
            return;

        isTurnOff = false;
        HowMuchTransformersIsOff--;
        PressEPanel.SetActive(false);

        SwitchElectricity();
    }

    public void TurnOFF ()
    {
        if (isTurnOff)
            return;

        isTurnOff = true;
        HowMuchTransformersIsOff++;

        if (OnTransformerOff!=null)
            OnTransformerOff(transform.position);

        SwitchElectricity();
        Debug.Log($"Transformer {gameObject.name} is OFF");
    }

    public void Break()
    {
        if (isBroken)
            return;

        explosion.gameObject.SetActive(true);

        PlayLongParticles();

        isBroken = true;
        TurnOFF();
        HowMuchTransformersIsBroken++;
        Debug.Log($"Transformer {gameObject.name} is BROKEN");

    }

    public void Repair ()
    {
        if (!isBroken)
            return;

        StartCoroutine(RepairCuro(repairTime));
    }

    private IEnumerator RepairCuro (float time)
    {
        yield return new WaitForSeconds(time);

        isBroken = false;
        HowMuchTransformersIsBroken--;
        explosion.gameObject.SetActive(false);

        StopLongParticles();

        HoldEPanel.SetActive(false);
    }

    private void PlayLongParticles ()
    {
        foreach (ParticleSystem ps in longParticles)
            ps.Play();
        fireAudio.Play();
    }

    private void StopLongParticles ()
    {
        foreach (ParticleSystem ps in longParticles)
            ps.Stop();
        fireAudio.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            HoldEPanel.SetActive(false);
            PressEPanel.SetActive(false);
        }
    }
}
