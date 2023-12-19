using UnityEngine;
using Unity.Services.Core;

public class CustomUserID : MonoBehaviour
{
    public string[] userIDs; // Array of predefined user IDs

    private int currentIndex = 0;

    void Start()
    {
        SwitchToNextUser();
    }

    void SwitchToNextUser()
    {
        if (userIDs.Length == 0)
        {
            Debug.Log("No user ID provided");
            return;
        }

        UnityServices.ExternalUserId = userIDs[currentIndex];
        Debug.Log("Switched to user ID: " + userIDs[currentIndex]);

        // Move to the next user ID in the array
        currentIndex = (currentIndex + 1) % userIDs.Length;
    }
}
