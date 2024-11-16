using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField, Tooltip("Material using the GaussianWaveShader.")]
    private Material waveMaterial;

    [SerializeField, Range(0.1f, 5f), Tooltip("Multiplier to slow down the wave speed.")]
    private float waveSpeedMultiplier = 0.5f;

    [SerializeField, Range(1f, 10f), Tooltip("Time interval (in seconds) for updating wave centers.")]
    private float updateInterval = 5f;

    [SerializeField, Range(0f, 1f), Tooltip("Smoothing factor for wave height changes.")]
    private float smoothingFactor = 0.1f;

    [Header("Plane Bounds (Auto-Calculated)")]
    [Tooltip("Automatically calculated minimum bounds for the wave plane (x, z).")]
    private Vector2 planeMinBounds;

    [Tooltip("Automatically calculated maximum bounds for the wave plane (x, z).")]
    private Vector2 planeMaxBounds;

    private float timer;
    private bool isChangingWave = false;
    private Vector2 targetCenter; // Target position for the wave center

    private void Start()
    {
        SetPlaneBounds();

        // Initialize the wave center in the shader material
        waveMaterial.SetFloat("_CenterX", transform.position.x);
        waveMaterial.SetFloat("_CenterZ", transform.position.z);
        targetCenter = new Vector2(transform.position.x, transform.position.z);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval && !isChangingWave)
        {
            UpdateWaveCenter();
            timer = 0f;
        }

        SmoothMoveWaveCenter(); // Smoothly interpolate towards the target center
    }

    private void UpdateWaveCenter()
    {
        StartCoroutine(ChangeWaveCenter());
    }

    private IEnumerator ChangeWaveCenter()
    {
        isChangingWave = true;

        float currentX = waveMaterial.GetFloat("_CenterX");
        float currentZ = waveMaterial.GetFloat("_CenterZ");
        float waveHeight = CalculateWaveHeight(currentX, currentZ);

        if (waveHeight <= 0.01f)
        {
            targetCenter = GetNewWaveCenter();
        }

        yield return new WaitForSeconds(0.1f);
        isChangingWave = false;
    }

    private void SmoothMoveWaveCenter()
    {
        float currentX = waveMaterial.GetFloat("_CenterX");
        float currentZ = waveMaterial.GetFloat("_CenterZ");

        // Interpolate from current position to target position
        float newX = Mathf.Lerp(currentX, targetCenter.x, Time.deltaTime * smoothingFactor);
        float newZ = Mathf.Lerp(currentZ, targetCenter.y, Time.deltaTime * smoothingFactor);

        waveMaterial.SetFloat("_CenterX", newX);
        waveMaterial.SetFloat("_CenterZ", newZ);
    }

    private float CalculateWaveHeight(float centerX, float centerZ)
    {
        float waveHeight = 0f;
        float posX = transform.position.x;
        float posZ = transform.position.z;

        float waveHeightProp = waveMaterial.GetFloat("_WaveHeight");
        float waveSpreadProp = waveMaterial.GetFloat("_WaveSpread");
        float waveSpeedProp = waveMaterial.GetFloat("_WaveSpeed") * waveSpeedMultiplier;

        waveHeight += SmoothWaveHeight(posX, posZ, centerX, centerZ, waveSpreadProp, waveHeightProp, waveSpeedProp);

        return waveHeight;
    }

    private float SmoothWaveHeight(float x, float y, float centerX, float centerY, float spread, float height, float speed)
    {
        float rawHeight = GaussianWave(x, y, centerX, centerY, spread, height, speed);
        return Mathf.Lerp(rawHeight, 0, smoothingFactor);
    }

    private float GaussianWave(float x, float y, float centerX, float centerY, float spread, float height, float speed)
    {
        float exponent = -((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY)) / (spread * spread);
        return height * Mathf.Exp(exponent) * Mathf.Sin(Time.time * speed);
    }

    private Vector2 GetNewWaveCenter()
    {
        float newX = Random.Range(planeMinBounds.x, planeMaxBounds.x);
        float newZ = Random.Range(planeMinBounds.y, planeMaxBounds.y);
        return new Vector2(newX, newZ);
    }

    private void SetPlaneBounds()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            Bounds bounds = renderer.bounds;
            planeMinBounds = new Vector2(bounds.min.x, bounds.min.z);
            planeMaxBounds = new Vector2(bounds.max.x, bounds.max.z);
        }
        else
        {
            planeMinBounds = new Vector2(-5f, -5f);
            planeMaxBounds = new Vector2(5f, 5f);
        }
    }
}
