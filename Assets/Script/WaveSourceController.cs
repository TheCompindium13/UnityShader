using UnityEngine;

public class WaveSourceController : MonoBehaviour
{
    public Material waveMaterial;
    private void Start()
    {
        waveMaterial.SetVector("_WaveDir1", new Vector3(1, 0, 1).normalized);
        waveMaterial.SetVector("_WaveDir2", new Vector3(1, 0, -1).normalized);
        waveMaterial.SetVector("_WaveDir3", new Vector3(-1, 0, 1).normalized);
    }
}
