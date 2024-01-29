using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "TransformEvent", menuName = "Events/Transform")]
    public class TransformEvent : ParameterisedGameEvent<Transform>
    {
    }
}