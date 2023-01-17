using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private new string name = "New Skill";
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float activeTime = 3f;

    [SerializeField] protected EPoolObjectType poolType;

    [field: SerializeField] public float BaseCoolDown { get; set; } = 1;
    public float NextCoolDown { get; set; } = 0;

    public abstract void Activate(Transform holderTrans);
}