using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;  // Jumlah damage yang diberikan
    public float damageInterval = 1f;  // Interval waktu antar damage (contoh: 1 detik)
    private float lastDamageTime;  // Waktu terakhir damage diberikan

    // Ketika terjadi collision awal
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DealDamage(collision.gameObject);
        }
    }

    // Selama collision terus terjadi
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cek apakah sudah cukup waktu berlalu untuk memberikan damage lagi
            if (Time.time - lastDamageTime >= damageInterval)
            {
                DealDamage(collision.gameObject);
            }
        }
    }

    private void DealDamage(GameObject player)
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
        lastDamageTime = Time.time;  // Reset timer
    }
}
