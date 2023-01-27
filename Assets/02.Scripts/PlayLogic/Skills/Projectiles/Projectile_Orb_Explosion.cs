using System;
using UnityEngine;
public class Projectile_Orb_Explosion : MonoBehaviour
{
    public float Damage { get; set; } = 0;
    public bool Critical { get; set; } = false;
    private void Start()
    {
        SoundManager.Instance.PlaySFXSound("Orb_Explosion");
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
            col.GetComponent<Enemy>().ReceiveDamage(Damage, transform.right, Critical);
    }
}