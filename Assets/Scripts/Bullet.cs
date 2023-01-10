using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 direction = Vector3.zero;
    float Speed = 0;
    float Damage = 0;
    public void Tang(float damage, float speed = 15f, float DestroyTime = 5f)//Vector3 dir,
    {
        
        direction = transform.forward;
        Speed = speed;
        Damage = damage;

        GetComponent<Rigidbody>().velocity = new Vector3(direction.x * Speed, 0, direction.z * Speed);

        Destroy(gameObject, DestroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<CubeController>().GetDamage(Damage);
            Destroy(gameObject);
        }
    }
}
