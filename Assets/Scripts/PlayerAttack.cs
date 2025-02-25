using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Range Attack (Peluru)
    public GameObject bulletPrefab;  // Prefab peluru
    public Transform firePoint;  // Posisi tempat peluru keluar
    public float bulletSpeed = 10f;  // Kecepatan peluru

    // Ammo Management
    public int maxAmmoInGun = 8;  // Maksimal peluru di senjata
    public int currentAmmoInGun;  // Peluru yang tersedia di senjata saat ini
    public int totalAmmo = 20;  // Total peluru (magazine)
    public int maxTotalAmmo = 20;  // Batas maksimal peluru (magazine)

    private PlayerMovement playerMovement;  // Reference ke PlayerMovement

    // Melee Attack
    /*public GameObject meleeHitbox;*/  // Collider yang digunakan untuk hitbox serangan melee
    public float meleeAttackCooldown = 1f;  // Cooldown antara serangan melee
    private bool canMelee = true;  // Menentukan apakah player bisa melakukan serangan melee

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();  // Ambil reference ke script PlayerMovement
        currentAmmoInGun = maxAmmoInGun;  // Set peluru awal ke maksimal
        /*meleeHitbox.SetActive(false);*/  // Hitbox melee awalnya dimatikan
    }

    void Update()
    {
        // Cek input untuk menembak (range attack)
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentAmmoInGun > 0)
            {
                Shoot();
            }
            else
            {
                Debug.Log("Out of ammo. Reload needed.");
            }
        }

        // Cek input untuk reload
        if (Input.GetButtonDown("Reload"))
        {
            Reload();
        }

        // Cek input untuk melee attack (Fire2 untuk melee)
        if (Input.GetButtonDown("Fire2") && canMelee)
        {
            StartCoroutine(MeleeAttack());
        }
    }

    void Shoot()
    {
        // Menentukan offset dari firePoint untuk menghindari tabrakan dengan karakter
        Vector2 spawnPosition = (playerMovement.facingRight) ? firePoint.position + new Vector3(0.5f, 0, 0) : firePoint.position + new Vector3(-0.5f, 0, 0);

        // Membuat peluru baru dari prefab di posisi spawnPosition
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        currentAmmoInGun--;  // Kurangi jumlah peluru dalam senjata

        // Menentukan arah peluru berdasarkan arah karakter
        if (playerMovement.facingRight)
        {
            rb.velocity = new Vector2(bulletSpeed, 0f);  // Peluru ke kanan
        }
        else
        {
            rb.velocity = new Vector2(-bulletSpeed, 0f);  // Peluru ke kiri
        }

        Debug.Log("Menembak ke arah: " + (playerMovement.facingRight ? "kanan" : "kiri"));
    }

    void Reload()
    {
        // Hitung berapa peluru yang bisa diambil dari total ammo ke senjata
        int ammoNeeded = maxAmmoInGun - currentAmmoInGun;  // Berapa peluru yang kurang di senjata
        int ammoToReload = Mathf.Min(ammoNeeded, totalAmmo);  // Ambil peluru sesuai yang kurang atau sisa peluru yang ada

        if (ammoToReload > 0)
        {
            currentAmmoInGun += ammoToReload;  // Tambahkan peluru ke senjata
            totalAmmo -= ammoToReload;  // Kurangi peluru di magazine
            Debug.Log("Reloading... " + currentAmmoInGun + "/" + totalAmmo);
        }
        else
        {
            Debug.Log("No ammo left to reload.");
        }
    }

    // Coroutine untuk Melee Attack
    IEnumerator MeleeAttack()
    {
        canMelee = false;  // Mencegah serangan melee berulang-ulang
        /*meleeHitbox.SetActive(true);*/  // Aktifkan hitbox melee

        Debug.Log("Melakukan serangan melee");

        // Tunggu selama animasi serangan (disesuaikan dengan durasi animasi)
        yield return new WaitForSeconds(0.5f);

        /*meleeHitbox.SetActive(false);*/  // Nonaktifkan hitbox melee
        yield return new WaitForSeconds(meleeAttackCooldown);  // Tunggu cooldown sebelum bisa menyerang lagi

        canMelee = true;  // Mengizinkan serangan melee lagi
    }
}
