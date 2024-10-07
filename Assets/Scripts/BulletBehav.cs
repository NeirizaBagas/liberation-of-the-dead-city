using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehav : MonoBehaviour
{
    public int damage = 25;  // Nilai damage peluru
    public float maxDistance = 10f;  // Jarak maksimum peluru sebelum hilang
    private Vector3 startPosition;  // Posisi awal peluru

    void Start()
    {
        // Simpan posisi awal peluru saat pertama kali ditembakkan
        startPosition = transform.position;
    }

    void Update()
    {
        // Cek apakah peluru sudah melebihi jarak maksimum
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);  // Hancurkan peluru jika melewati jarak maksimum
        }
    }

    // Fungsi untuk mendeteksi collision dengan objek apapun
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek apakah peluru bertabrakan dengan enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Panggil fungsi TakeDamage pada enemy
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

        // Hancurkan peluru setelah menabrak objek apapun
        Destroy(gameObject);
    }
}
