using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public GameObject bulletPrefab; // Assign the bullet prefab in the inspector
    public Transform bulletSpawnPoint; // Assign a point at the front of the player where bullets should spawn
    public float runSpeed = 40f;
    public bool isPlayer1;
    public LayerMask floorLayer;
    public float bulletSpeed = 10f; // Adjust as needed for projectile speed
    public float fireRate = 1f; // Fire a bullet every second

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    private Collider2D playerCollider;
    private bool isPhasing = false;
    private float nextFireTime = 0f;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isPlayer1)
        {
            horizontalMove = Input.GetKey(KeyCode.LeftArrow) ? -runSpeed : Input.GetKey(KeyCode.RightArrow) ? runSpeed : 0;
            if (Input.GetKeyDown(KeyCode.UpArrow)) jump = true;
            crouch = Input.GetKey(KeyCode.DownArrow);
        }
        else
        {
            horizontalMove = Input.GetKey(KeyCode.A) ? -runSpeed : Input.GetKey(KeyCode.D) ? runSpeed : 0;
            if (Input.GetKeyDown(KeyCode.W)) jump = true;
            crouch = Input.GetKey(KeyCode.S);
        }

/*        if (crouch && !isPhasing)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, floorLayerIndex, true);
            Debug.Log("Phasing enabled: Ignoring floor collisions.");
            isPhasing = true;
        }
        else if (!crouch && isPhasing)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, floorLayerIndex, false);
            Debug.Log("Phasing disabled: Floor collisions re-enabled.");
            isPhasing = false;
        }*/

        // Check if it's time to fire a bullet
        if (Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate; // Reset the timer for the next shot
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void FireBullet()
    {
        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Get Rigidbody2D of the bullet to apply force
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Determine the direction based on player facing direction
        float direction = transform.localScale.x > 0 ? 1 : -1; // Assuming localScale.x indicates facing direction

        // Apply force to the bullet to create a parabolic arc
        Vector2 launchVelocity = new Vector2(direction * bulletSpeed, bulletSpeed / 2); // Adjust the y-component for arc height
        bulletRb.linearVelocity = launchVelocity;
    }
}
