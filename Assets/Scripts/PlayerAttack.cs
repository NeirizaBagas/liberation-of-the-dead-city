using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab peluru
    public Transform firePoint;  // Posisi tempat peluru keluar
    public float bulletSpeed = 10f;  // Kecepatan peluru
    private PlayerMovement playerMovement;  // Reference ke PlayerMovement

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();  // Ambil reference ke script PlayerMovement
    }

    void Update()
    {
        // Cek input untuk menembak
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Menentukan offset dari firePoint untuk menghindari tabrakan dengan karakter
        Vector2 spawnPosition = (playerMovement.facingRight) ? firePoint.position + new Vector3(0.5f, 0, 0) : firePoint.position + new Vector3(-0.5f, 0, 0);

        // Membuat peluru baru dari prefab di posisi spawnPosition
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Debugging untuk memeriksa nilai facingRight
        Debug.Log("Menembak ke arah: " + (playerMovement.facingRight ? "kanan" : "kiri"));

        // Menentukan arah peluru berdasarkan arah karakter
        if (playerMovement.facingRight)
        {
            // Peluru ke kanan
            rb.velocity = new Vector2(bulletSpeed, 0f);
        }
        else
        {
            // Peluru ke kiri
            rb.velocity = new Vector2(-bulletSpeed, 0f);
        }
    }
}
