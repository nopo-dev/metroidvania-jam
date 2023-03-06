using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    public GameObject tPlayer;
    public Transform tFollowTarget;

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

        tPlayer = GameObject.FindWithTag("Player");
        if (tPlayer != null)
        {
            tFollowTarget = tPlayer.transform;
            cam.LookAt = tFollowTarget;
            cam.Follow = tFollowTarget;
        }
    }

    // i.e. can call CinemachineShake.Instance.ShakeCamera(8f, 0.3f); from PlayerController or sth
    public void ShakeCamera(float intensity, float duration)
    {
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