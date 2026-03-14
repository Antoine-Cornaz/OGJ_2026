using UnityEngine;

public class Spring : MonoBehaviour
{
    private SoftBodyMassPoint p1, p2;

    private float restLength;
    private float stiffness;
    private float damping;
    private LineRenderer lineRenderer;

    public void Initialize(SoftBodyMassPoint p1, SoftBodyMassPoint p2, float stiffness, float damping)
    {
        this.p1 = p1;
        this.p2 = p2;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        updateEndPoint();
        restLength = Vector3.Distance(p1.transform.position, p2.transform.position);
        this.stiffness = stiffness;
        this.damping = damping;
    }

    public void UpdateString()
    {
        Vector3 springVec = p2.transform.position - p1.transform.position;
        float currLen = springVec.magnitude;
        Vector3 force = - stiffness * Mathf.Abs(currLen - restLength) * springVec.normalized;
        Debug.Log("spring vec dir is " + springVec.normalized);
        p1.ApplyForce(force);
        p2.ApplyForce(-force);

        p1.velocity *= damping;
        p2.velocity *= damping;
    }

    private void Update()
    {
        updateEndPoint();
    }

    private void updateEndPoint()
    {
        lineRenderer.SetPositions(new Vector3[] { p1.transform.position, p2.transform.position });
    }
}
