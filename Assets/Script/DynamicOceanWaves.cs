using UnityEngine;

public class DynamicOceanWaves : MonoBehaviour
{
    public Material oceanMaterial; // Assign your ocean material here
    public float heightVariation = 0.2f; // Variation in wave height
    public float speedVariation = 0.5f; // Variation in wave speed
    public float spreadVariation = 0.1f; // Variation in wave spread

    void Update()
    {
        // Modify wave properties dynamically
        for (int i = 0; i < 3; i++)
        {
            oceanMaterial.SetVector($"_WaveCenter{i}", new Vector4(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, 0));
            oceanMaterial.SetFloat($"_WaveSpeed{i}", 1f + Random.Range(-speedVariation, speedVariation));
            oceanMaterial.SetFloat($"_WaveHeight{i}", 0.3f + Random.Range(-heightVariation, heightVariation));
            oceanMaterial.SetFloat($"_WaveSpread{i}", 0.5f + Random.Range(-spreadVariation, spreadVariation));
        }
    }
}
