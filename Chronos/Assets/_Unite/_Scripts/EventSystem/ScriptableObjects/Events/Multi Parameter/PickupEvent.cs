using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "PickupEvent", menuName = "Events/PickupItemEvent")]
    public class PickupEvent : ParameterisedGameEvent<PickupInfo>
    {
    }
}