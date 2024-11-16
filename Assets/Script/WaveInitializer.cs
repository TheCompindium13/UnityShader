using UnityEngine;

public class WaveInitializer : MonoBehaviour
{
    public Material waveMaterial;

    void Start()
    {
        // Example of setting up small waves
        for (int i = 0; i < 30; i++)
        {
            waveMaterial.SetVector($"_SmallWaveCenters{i}", new Vector4(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, 0));
            waveMaterial.SetFloat($"_SmallWaveSpeeds{i}", Random.Range(0.5f, 2.0f));
            waveMaterial.SetFloat($"_SmallWaveHeights{i}", 0.1f);
            waveMaterial.SetFloat($"_SmallWaveSpreads{i}", 0.1f);
        }

        // Example of setting up medium waves
        for (int i = 0; i < 10; i++)
        {
            waveMaterial.SetVector($"_MediumWaveCenters{i}", new Vector4(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0, 0));
            waveMaterial.SetFloat($"_MediumWaveSpeeds{i}", Random.Range(1.0f, 3.0f));
            waveMaterial.SetFloat($"_MediumWaveHeights{i}", 0.2f);
            waveMaterial.SetFloat($"_MediumWaveSpreads{i}", 0.3f);
        }

        // Example of setting up giant waves
        for (int i = 0; i < 5; i++)
        {
            waveMaterial.SetVector($"_GiantWaveCenters{i}", new Vector4(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0, 0));
            waveMaterial.SetFloat($"_GiantWaveSpeeds{i}", Random.Range(0.5f, 1.0f));
            waveMaterial.SetFloat($"_GiantWaveHeights{i}", 0.5f);
            waveMaterial.SetFloat($"_GiantWaveSpreads{i}", 0.6f);
        }
    }
}

