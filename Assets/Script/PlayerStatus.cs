using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    protected override void Update()
    {
        Debug.Log("��������");
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
