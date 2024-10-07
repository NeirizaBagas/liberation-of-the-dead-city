using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;       // Darah maksimal
    public int currentHealth;          // Darah saat ini
    public int healAmount = 20;        // Jumlah penyembuhan ketika mengambil item heal

    private void Start()
    {
        currentHealth = maxHealth;     // Set current health ke maxHealth di awal
    }

    // Fungsi untuk menerima damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;        // Kurangi darah dengan nilai damage

        // Jika current health sama dengan atau kurang dari 0, player mati
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Fungsi untuk menghancurkan player
    void Die()
    {
        // Hancurkan gameObject player
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Heal"))
        {
            Heal();
            Destroy(other.gameObject);      // Hancurkan item heal setelah diambil
        }
    }

    // Fungsi untuk menambah health
    void Heal()
    {
        currentHealth += healAmount;      // Tambahkan health
        if (currentHealth > maxHealth)     // Pastikan tidak melebihi max health
        {
            currentHealth = maxHealth;     // Set ke max health jika lebih
        }
        Debug.Log("Health sekarang: " + currentHealth);  // Debug untuk melihat health saat ini
    }
}
