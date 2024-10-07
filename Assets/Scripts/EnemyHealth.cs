using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;  // Darah awal enemy

    // Fungsi untuk menerima damage
    public void TakeDamage(int damage)
    {
        health -= damage;  // Kurangi darah dengan nilai damage

        // Jika darah sama dengan atau kurang dari 0, enemy mati
        if (health <= 0)
        {
            Die();
        }
    }

    // Fungsi untuk menghancurkan enemy
    void Die()
    {
        // Hancurkan gameObject enemy
        Destroy(gameObject);
    }
}
