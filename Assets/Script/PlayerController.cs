using UnityEngine;

public class PlayerController : Floating
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _swimmingForce = 8.0f;
    [SerializeField] private float _rotateSpeed = 5.0f;
    private float _pitch = 0f; // キャラクター上下角度

    protected override void Start()
    {
        base.Start();
        if (_camera == null) _camera = Camera.main;
    }

    void Update()
    {
        RotateDirection();
        if (Input.GetButtonDown("Fire1") && _state == ObjectState.Floating) _rigidbody.AddForce(ForceSwimming(), ForceMode.Impulse);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private Vector3 ForceSwimming()
    {
        Vector3 force = transform.forward * _swimmingForce;
        Debug.Log("浮き上がれ！");
        return force;
    }

    private void RotateDirection()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A,D
        float v = -Input.GetAxisRaw("Vertical");   // W,S

        // --- 左右旋回（Y軸、カメラ基準） ---
        if (Mathf.Abs(h) > 0.01f)
        {
            Vector3 camUp = Vector3.up; // Y軸回転
            float yRotation = h * _rotateSpeed * Time.deltaTime;
            transform.Rotate(camUp, yRotation, Space.World);
        }

        // --- 前後回転（X軸、キャラクター自身） ---
        if (Mathf.Abs(v) > 0.01f)
        {
            _pitch -= v * _rotateSpeed * Time.deltaTime; // 上方向が負、下方向が正
            _pitch = Mathf.Clamp(_pitch, -60f, 60f);    // 上下制限

            Vector3 currentEuler = transform.localEulerAngles;
            transform.localRotation = Quaternion.Euler(_pitch, currentEuler.y, currentEuler.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sea"))
        {
            _state = ObjectState.Floating;
            _rigidbody.useGravity = false;
            _rigidbody.linearDamping = _waterDrag;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sea"))
        {
            _state = ObjectState.Grounding;
            _rigidbody.useGravity = true;
            _rigidbody.linearDamping = 0.0f;
        }
    }
}