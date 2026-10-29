using UnityEngine;

public class ClearConditionTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (KillCounter.TotalKills >= 1)
        {
            Debug.Log("LEVEL CLEARED!");
            KillCounter.ResetKills();
        }
    }
}
