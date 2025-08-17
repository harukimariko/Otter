using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterState
{
    Emerge,
    Diving,
}

public class CharacterStatus : MonoBehaviour
{
    public float _Oxygen = 0.0f;
    [SerializeField] protected Collider _faceCollider;
    [SerializeField] protected float _OxygenMax = 100.0f;
    [SerializeField] protected float _useOxygen = 1.0f;
    [SerializeField] protected float _healOxygen = 1.0f;
    [SerializeField] protected Slider _slider;
    [SerializeField] protected CharacterState _state;

    private void Start()
    {
        _Oxygen = _OxygenMax;
    }

    protected virtual void Update()
    {
        if (_state == CharacterState.Diving)
        {
            UpdateSlider(UseOxygen());
        }
        if (_state == CharacterState.Emerge)
        {
            UpdateSlider(HealOxygen());
        }
    }

    private float HealOxygen()
    {
        if (_Oxygen < _OxygenMax) _Oxygen += Time.deltaTime * _healOxygen;
        return _Oxygen / _OxygenMax;
    }

    private float UseOxygen()
    {
        if (_Oxygen > 0.0f) _Oxygen -= Time.deltaTime * _useOxygen;
        return _Oxygen / _OxygenMax;
    }

    private void UpdateSlider(float value)
    {
        _slider.value = value;
    }

    protected virtual void Dead() { }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other == _faceCollider && other.gameObject.CompareTag("Sea"))
        {
            _state = CharacterState.Diving;
        }
        if (other.gameObject.CompareTag("Sea"))
        {
            _state = CharacterState.Diving;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other == _faceCollider && other.gameObject.CompareTag("Sea"))
        {
            _state = CharacterState.Emerge;
        }
        if (other.gameObject.CompareTag("Sea"))
        {
            _state = CharacterState.Emerge;
        }
    }
}