using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private new string name = "New Skill";
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float activeTime = 3f;
    protected Transform spawnTransform;
    protected Targeting targeting = new Targeting();

    [SerializeField] protected EPoolObjectType poolType;
    [field: SerializeField] public float BaseCoolDown { get; set; } = 1;
    public float NextCoolDown { get; set; } = 0;

    protected virtual void Initialize(Transform holderTrans)
    {
        spawnTransform = holderTrans;
    }

    public virtual void Activate(Transform holderTrans)
    {
        Initialize(holderTrans);
        StartCoroutine(Pattern());
    }
    protected abstract IEnumerator Pattern();
}