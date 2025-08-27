using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TrackingCamera : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Transform _target;
    public void SetTarget(Transform transform) {  _target = transform; }

    [SerializeField] private float _followSpeed = 5f; // �Ǐ]���x
    [SerializeField] private float _deadZone = 8f;    // �Ǐ]���J�n����臒l

    private void Start()
    {
        if (_target == null) _target = GameObject.FindGameObjectWithTag("Player").transform;
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // ���݃J�����Ƃ̋���
        float distance = Vector3.Distance(transform.position, _target.position);

        // ��苗���ȏ�Ȃ�ǂ�������
        if (distance > _deadZone)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                _target.position,
                Time.deltaTime * _followSpeed
            );
        }

        // Target�̕���������
        transform.LookAt(_target.position);
    }
}
