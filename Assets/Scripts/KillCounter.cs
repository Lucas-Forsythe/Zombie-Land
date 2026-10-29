
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public static int TotalKills { get; private set; }

    public static void RegisterKill()
    {
        TotalKills++;
        Debug.Log("Total Kills: " + TotalKills);
    }

    public static void ResetKills()
    {
        TotalKills = 0;
    }
}
