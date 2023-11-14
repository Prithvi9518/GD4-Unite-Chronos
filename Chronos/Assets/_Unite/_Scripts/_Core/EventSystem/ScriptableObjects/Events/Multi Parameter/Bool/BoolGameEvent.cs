using UnityEngine;

namespace Unite.Core.EventSystem
{
    [CreateAssetMenu(fileName = "BoolGameEvent", menuName = "Unite/Scriptable Objects/Events/BoolGameEvent")]
    public class BoolGameEvent : ParameterisedGameEvent<bool>
    { }
}