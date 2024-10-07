using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;         // Referensi ke posisi player
    public float moveSpeed = 2f;     // Kecepatan gerak enemy
    public float chaseRange = 10f;   // Jarak dimana enemy mulai mengejar player
    public float attackRange = 1.5f; // Jarak dimana enemy mulai menyerang
    private bool isFacingRight = true; // Cek apakah enemy menghadap ke kanan

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Referensi ke Rigidbody2D
    }

    void Update()
    {
        // Hitung jarak antara enemy dan player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Jika jarak player lebih dekat dari chaseRange tapi lebih jauh dari attackRange, enemy mengejar
        if (distanceToPlayer <= chaseRange && distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        // Jika player berada dalam attackRange, enemy berhenti mengejar dan menyerang
        else if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        // Hanya bergerak di sumbu x (horizontal) untuk tetap di tanah
        Vector2 targetPosition = new Vector2(player.position.x, rb.position.y);

        // Gerak ke arah player
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);

        // Mengecek arah agar enemy selalu menghadap ke player
        if ((player.position.x > transform.position.x && !isFacingRight) || (player.position.x < transform.position.x && isFacingRight))
        {
            Flip();
        }
    }

    void AttackPlayer()
    {
        // Logika serangan, bisa menambahkan animasi atau mekanisme attack di sini
        Debug.Log("Enemy menyerang!");
    }

    // Fungsi untuk membalikkan arah enemy
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Membalikkan skala di sumbu x
        transform.localScale = localScale;
    }
}
