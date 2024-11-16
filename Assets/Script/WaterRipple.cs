using UnityEngine;

public class WaterRipple : MonoBehaviour
{
    public Material waterMaterial;
    public float rippleRadius = 1.0f;

    public Vector3 windDirection = new Vector3(1, 0, 0); // Direction of the wind
    public float windSpeed = 1.0f; // Speed of the wind

    private void Start()
    {
        // Set initial wind parameters
        waterMaterial.SetVector("_WindDirection", windDirection);
        waterMaterial.SetFloat("_WindSpeed", windSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Set collision ripple center to the collision point
        Vector3 collisionPoint = collision.contacts[0].point;
        waterMaterial.SetVector("_CollisionRippleCenter", collisionPoint);
        waterMaterial.SetFloat("_CollisionRippleRadius", rippleRadius);

        // Optional: If you want to simulate a force based on the collision
        // Vector3 forceDirection = (collisionPoint - transform.position).normalized;
        // collision.rigidbody.AddForce(forceDirection * impactForce, ForceMode.Impulse);
    }

    private void Update()
    {
        // Update wind direction and speed dynamically if needed
        waterMaterial.SetVector("_WindDirection", windDirection);
        waterMaterial.SetFloat("_WindSpeed", windSpeed);
    }
}
