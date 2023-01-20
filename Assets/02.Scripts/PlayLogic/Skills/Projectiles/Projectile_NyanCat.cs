using System.Collections;
using UnityEngine;

public class Projectile_NyanCat : Projectile
{

    
    protected override IEnumerator Move()
    {
        speed = 20;
        transform.rotation *= Quaternion.Euler(new Vector3(0,0,90));
        Vector2 force = transform.right.normalized * speed;
        rigidbody2D.AddForce(force, ForceMode2D.Impulse);

        // Test
        if (transform.eulerAngles.z is > 90 and <= 270)
            GetComponent<SpriteRenderer>().flipY = true;
        else
            GetComponent<SpriteRenderer>().flipY = false;
        
        yield return null;
    }
}