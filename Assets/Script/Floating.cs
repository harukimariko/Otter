using UnityEngine;

public enum ObjectState
{
    Floating,
    Grounding
}

public class Floating : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected float _floatingPower = 2.0f;
    [SerializeField] protected float _waterDrag = 2.0f;
    [SerializeField] public ObjectState _state = ObjectState.Grounding;

    protected virtual void Start()
    {
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        if (_state == ObjectState.Floating)
        {
            _rigidbody.AddForce(new Vector3(0.0f, _floatingPower, 0.0f), ForceMode.Force);
        }
    }
}
