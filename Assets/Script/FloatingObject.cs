using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public Material waterMaterial;
    public float buoyancyFactor = 1.0f;
    public float weightThreshold = 5.0f;
    public float rollFactor = 0.5f;
    public float waveFollowSpeed = 2.0f;
    public float bounceStrength = 1.0f;
    public float bounceFrequency = 1.0f;

    private Rigidbody rb;
    private int waveHeightID;
    private int foamDirectionID;
    private int waterDirectionID;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        waveHeightID = Shader.PropertyToID("_WaveHeight");
        foamDirectionID = Shader.PropertyToID("_FoamDirection");
        waterDirectionID = Shader.PropertyToID("_WaterDirection");
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            // Retrieve the current wave height
            float waveHeight = waterMaterial.GetFloat(waveHeightID);
            float adjustedHeight = waveHeight * buoyancyFactor;


            if (rb.mass <= weightThreshold)
            {
                // Apply buoyancy and move the object
                float distanceToWater = adjustedHeight - transform.position.y;
                if (distanceToWater > 0)
                {
                    rb.AddForce(Vector3.up * distanceToWater * buoyancyFactor, ForceMode.Acceleration);
                }

                // Add bounce
                Vector3 newPosition = new Vector3(transform.position.x, adjustedHeight, transform.position.z);
                float bounce = Mathf.Sin(Time.time * bounceFrequency) * bounceStrength;
                newPosition.y += bounce;

                rb.MovePosition(Vector3.Lerp(transform.position, newPosition, Time.fixedDeltaTime * waveFollowSpeed));

                // Apply opposite directions for water and foam movement
                Vector3 foamDirection = new Vector3(0.5f, 0, -0.5f);  // Example foam direction
                Vector3 waterDirection = new Vector3(-0.5f, 0, 0.5f); // Example water direction (opposite)

                // Update the shader with the new directions
                waterMaterial.SetVector(foamDirectionID, foamDirection);
                waterMaterial.SetVector(waterDirectionID, waterDirection);

                // Simulate rolling effect
                float roll = Mathf.Sin(Time.time) * rollFactor;
                rb.MoveRotation(Quaternion.Euler(0, 0, roll * 10));
            }
        }
    }
}
