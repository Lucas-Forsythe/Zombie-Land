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

    [Header("References and Effects")]
    public Transform barrelTip; // Where the bullet visual starts.
    public LineRenderer laserTracer; // The visual bullet trail.
    public float tracerDuration = 0.05f; // How long the bullet's trail stays visible.

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

        StartCoroutine(RenderTracer());

        // Made to hold hit information.
        RaycastHit hit;

        // Placing the bullet at the starting point, which is the tip of the barrel.
        laserTracer.SetPosition(0, barrelTip.position);

        // The raycast calculation
        if (Physics.Raycast(barrelTip.position, barrelTip.forward, out hit, gunRange))
        {

            // What the system will say when we hit a target or an object.
            Debug.Log("You shot" +  hit.collider.name);

            //Tracer ends at the bullet's impact.
            laserTracer.SetPosition(1, hit.point);

        }
        else
        {

            // Calculates a point perfectly straight ahead at max range.
            Vector3 missPoint = barrelTip.position + (barrelTip.forward * gunRange);

            // Tells tracer to extend to the max distance.
            laserTracer.SetPosition(1, missPoint);

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
