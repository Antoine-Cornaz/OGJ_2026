using UnityEngine;

public class SoftBodyMassPoint : MonoBehaviour
{
    public Vector3 velocity;
    public float mass = 1f;

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    public void ApplyForce(Vector3 force)
    {
        velocity += (force / mass) * Time.deltaTime;
    }
}
