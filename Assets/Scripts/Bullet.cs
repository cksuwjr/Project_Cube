using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject Fromwho;
    Vector3 direction = Vector3.zero;
    float Speed = 0;
    float Damage = 0;
    public void Tang(GameObject fromwho, float damage, float speed = 15f, float DestroyTime = 5f)//Vector3 dir,
    {
        Fromwho = fromwho;
        direction = transform.forward;
        Speed = speed;
        Damage = damage;

        GetComponent<Rigidbody>().velocity = new Vector3(direction.x * Speed, 0, direction.z * Speed);

        Destroy(gameObject, DestroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Fromwho)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.GetComponent<CubeController>().GetDamage(Damage, Fromwho);
                other.GetComponent<CubeController>().KnockBack(Fromwho, 5f);
                Destroy(gameObject);
            }
        }
    }
}
