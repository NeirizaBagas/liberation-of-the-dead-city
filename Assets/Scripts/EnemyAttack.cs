using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek apakah peluru bertabrakan dengan enemy
        if (collision.gameObject.CompareTag("Player"))
        {
            // Panggil fungsi TakeDamage pada enemy
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
