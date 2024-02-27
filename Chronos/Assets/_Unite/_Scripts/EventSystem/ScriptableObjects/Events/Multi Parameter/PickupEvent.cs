using Unite.EventSystem;
using Unite.InteractionSystem;
using UnityEngine;

namespace _Unite._Scripts.EventSystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PickupEvent", menuName = "Events/PickupItemEvent")]
    public class PickupEvent : ParameterisedGameEvent<PickupInfo>
    {
    }
}