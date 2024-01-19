using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "StringEvent", menuName = "Events/StringEvent", order = 0)]
    public class StringEvent : ParameterisedGameEvent<string>
    {
    }
}