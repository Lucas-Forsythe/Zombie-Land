using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UIElements;

public class Revolver : MonoBehaviour
{
    // Before being fully functional:
    // Need to set up the line renderer component visually in Unity.
    // Optional: Maybe we want ammo and reload time?
    // Optional: Maybe we could have a script that allows weapon switch functionality? Or I can change the melee input to another button so that weapon switching won't be necessary.

    // Automatically searchezs the GameObject this is on for the PlayerInput component.
    private UnityEngine.InputSystem.PlayerInput playerInput;

    // Here to make sure the Line component is turned off when the game launches
    void Start()
    {
        // Automatically searchezs the GameObject this is on for the PlayerInput component.
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();

        // Making sure a Line Renderer component is assigned.
        if (laserTracer != null)
        {
            // Turn the line component completely off on launch
            laserTracer.enabled = false;
        }
    }

    [Header("handgun Settings")]
    public int gunDamage = 20; 
    public float fireRate = 0.25F; // Revolver cooldown between shots fired.
    public float gunRange = 50f; // bullet travel distance.
    private float nextFireTime = 0f; // Cooldown tracking.
    [Header("Raycast Settings")]
    public LayerMask enemyLayer;
    [Header("References and Effects")]
    public Transform barrelTip; // Where the bullet visual starts.
    public LineRenderer laserTracer; // The visual bullet trail.
    public float tracerDuration = 0.05f; // How long the bullet's trail stays visible.

    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;

    [Header("Gun Audio")]
    public AudioSource gunAudioSource;
    public AudioClip gunShotSound;

    private void Update()
    {
        // safety check.
        if (playerInput == null) return;

        // Read the Shoot action from your Inpur Actions window 
        if (playerInput.actions["shoot"].triggered)
        {
           // Weapon cooldown reset.
           if (Time.time >= nextFireTime)
            {
                // Cooldown timestamp for the next shot
                nextFireTime = Time.time + fireRate;

                // Gun fired.
                Shoot();
            }

        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || barrelTip == null)
        {
            Debug.LogWarning("Assign Bullet Prefab and Barrel Tip.");
            return;
        }

        // Plays the gunshot sound.
        if (gunAudioSource != null && gunShotSound != null)
        {
            gunAudioSource.PlayOneShot(gunShotSound);
        }

        GameObject bullet = Instantiate(
            bulletPrefab,
            barrelTip.position,
            barrelTip.rotation
        );

        // Uses the damage value from this gun.
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.damage = gunDamage;
        }

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.linearVelocity = barrelTip.forward * bulletSpeed;
        }
        else
        {
            Debug.LogError("The bullet prefab needs a Rigidbody on its main object.");
            Destroy(bullet);
        }
    }

    // Handless turning tracer on and off.
    System.Collections.IEnumerator RenderTracer()
    {
        // Turns visual line on.
        laserTracer.enabled = true;

        // Tells Unity to pause and wait for the given duration
        yield return new WaitForSeconds(tracerDuration);

        // Turns the visual line off after waiting the period of time.
        laserTracer.enabled = false;
    }

}
