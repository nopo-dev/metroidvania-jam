using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera cam;
    private float shakeTimer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Can only have one SaveAndLoader!");
            Destroy(this);
            return;
        }
        Instance = this;
        cam = GetComponent<CinemachineVirtualCamera>();
        DontDestroyOnLoad(gameObject);

    }

    public void ShakeCamera(float intensity, float duration)
    {
        Debug.Log("shaking");
        CinemachineBasicMultiChannelPerlin perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = intensity;
        shakeTimer = duration;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                perlin.m_AmplitudeGain = 0f;
            }
        }
    }
}