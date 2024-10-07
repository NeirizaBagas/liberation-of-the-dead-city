using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;  // Darah awal enemy
    public GameObject itemDropPrefab;  // Prefab item yang akan didrop

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
        // Hancurkan enemy
        // Tambahkan logika untuk drop item
        DropItem();

        // Hancurkan gameObject enemy
        Destroy(gameObject);
    }

    // Fungsi untuk mendrop item
    void DropItem()
    {
        if (itemDropPrefab != null)
        {
            // Buat item drop di posisi enemy
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        }
    }
}
