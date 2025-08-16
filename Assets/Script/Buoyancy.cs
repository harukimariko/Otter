using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    [SerializeField] private Transform waterHeight;
    [SerializeField] private float buoyancyForce = 10f;
    [SerializeField] private float damping = 0.5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float depth = waterHeight.position.y - transform.position.y;

        if (depth > 0)
        { }
        Vector3 force = Vector3.up * buoyancyForce * depth;
        rb.AddForce(force, ForceMode.Acceleration);

        rb.AddForce(-rb.linearVelocity * damping, ForceMode.Acceleration);
    }
}
