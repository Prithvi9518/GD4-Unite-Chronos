using Unite.Interactibles;
using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "InteractibleObjectEvent", menuName = "Events/InteractibleObjectEvent")]
    public class InteractibleObjectEvent : ParameterisedGameEvent<InteractibleObject>
    {
    }
}