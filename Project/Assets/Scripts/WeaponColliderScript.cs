using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderScript : MonoBehaviour
{
    public GameObject player;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(1);

        }
    }
}
