using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Confirm the object entering the phantom zone has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Search through the player's children to find the script on your SledgeHammer object
            HeavyWeapon heavyWeaponScript = other.GetComponentInChildren<HeavyWeapon>();

            if (heavyWeaponScript != null)
            {
                // Unlock the weapon permanently
                heavyWeaponScript.isUnlocked = true;

                // Print a clear message to your Unity Console to verify it worked during playtests
                Debug.Log("SledgeHammer unlocked! Press R to unleash it's shockwave.");

                // Remove the physical ground mesh from the scene
                Destroy(gameObject);
            }
        }
    }
}
