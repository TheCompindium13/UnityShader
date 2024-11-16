using UnityEngine;

public class WaveSourceController : MonoBehaviour
{
    public Material waveMaterial;
    private void Start()
    {
        waveMaterial.SetVector("_WaveDir1", new Vector3(1, 0, 1).normalized);
        waveMaterial.SetVector("_WaveDir2", new Vector3(1, 0, -1).normalized);
        waveMaterial.SetVector("_WaveDir3", new Vector3(-1, 0, 1).normalized);
        waveMaterial.SetVector("_WaveDir4", new Vector3(-2, 0, 1).normalized);
        waveMaterial.SetVector("_WaveDir5", new Vector3(-3, 0, 1).normalized);
        waveMaterial.SetVector("_WaveDir6", new Vector3(-4, 0, 1).normalized);
    }
}
