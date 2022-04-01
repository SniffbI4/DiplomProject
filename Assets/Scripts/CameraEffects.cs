using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public static CameraEffects instance { get; private set; }

    [SerializeField] private Cinemachine.CinemachineVirtualCamera camera;

    [SerializeField] private float maxExplosionShake;
    [SerializeField] private float interpolationSpeed;

    private CinemachineBasicMultiChannelPerlin cameraNoise;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);


        //MortiraBullet.OnExplosion += ShakeCamera;
        cameraNoise = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }   

    public void ShakeCamera (float amplitude, float duration)
    {
        interpolationSpeed = 1 /duration;
        cameraNoise.m_AmplitudeGain = amplitude > maxExplosionShake? maxExplosionShake : amplitude;
    }

    private void Update()
    {
        if (cameraNoise.m_AmplitudeGain > 0)
            cameraNoise.m_AmplitudeGain = Mathf.Lerp(cameraNoise.m_AmplitudeGain, 0, Time.deltaTime*interpolationSpeed);
    }
}
