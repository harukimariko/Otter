using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TrackingCamera : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Transform _target;
    public void SetTarget(Transform transform) {  _target = transform; }

    [SerializeField] private float _followSpeed = 5f; // 追従速度
    [SerializeField] private float _deadZone = 8f;    // 追従を開始する閾値

    private void Start()
    {
        if (_target == null) _target = GameObject.FindGameObjectWithTag("Player").transform;
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 現在カメラとの距離
        float distance = Vector3.Distance(transform.position, _target.position);

        // 一定距離以上なら追いかける
        if (distance > _deadZone)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                _target.position,
                Time.deltaTime * _followSpeed
            );
        }

        // Targetの方向を向く
        transform.LookAt(_target.position);
    }
}
