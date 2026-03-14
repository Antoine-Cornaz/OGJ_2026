using UnityEngine;

public abstract class SoftBody : MonoBehaviour
{
    public float damping = 0.9f;
    public float springStiffness = 0.5f;
    public GameObject massPointPrefab;
    public GameObject springPrefab;

    public SoftBodyMassPoint[,] massPoints;

    private void Start()
    {
        InitMassPoints();
        CreateSprings();
    }

    public abstract void InitMassPoints();
    public abstract void CreateSprings();

    public void CreateSpring(SoftBodyMassPoint p1, SoftBodyMassPoint p2)
    {
        SpringJoint2D spring = p1.gameObject.AddComponent<SpringJoint2D>();
        spring.connectedBody = p2.gameObject.GetComponent<Rigidbody2D>();
        spring.distance = Vector3.Distance(p1.transform.position, p2.transform.position);
        spring.dampingRatio = damping;
    }
}
