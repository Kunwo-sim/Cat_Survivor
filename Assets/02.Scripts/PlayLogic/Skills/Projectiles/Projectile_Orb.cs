using System.Collections;
using UnityEngine;

public class Projectile_Orb : Projectile
{
    [SerializeField] private GameObject explosion;
    protected override void Awake()
    {
        base.Awake();
        _projectileType = Define.ProjectileType.Range;
    }
    protected override IEnumerator Move()
    {
        speed = 10;
        Vector2 force = transform.right.normalized * speed;
        rigidbody2D.AddForce(force, ForceMode2D.Impulse);

        yield break;
    }
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            if (_projectileType == Define.ProjectileType.Melee)
            {
                damage = GetMeleeDamage(damage);
            }
            if(CriticalCheck())
            {
                bCritical = true;
                damage *= 2;
            }

            // col.GetComponent<Enemy>().ReceiveDamage(damage, transform.right, bCritical);

            if (_projectileType == Define.ProjectileType.Melee)
                return;

            Projectile_Orb_Explosion oe = Instantiate(explosion, transform.position, transform.rotation).GetComponent<Projectile_Orb_Explosion>();
            oe.Damage = damage;
            oe.Critical = bCritical;
            Destroy(oe.gameObject, 0.5f);
            CancelInvoke(nameof(Delete));
            Delete();
        }
    }
}