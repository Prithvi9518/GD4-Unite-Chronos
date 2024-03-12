using UnityEngine;

/// <summary>
/// Resource:https://www.youtube.com/watch?v=jdAbVkre8cw
/// Accessed 08/03/24
/// </summary>
public class LookAtCamera : MonoBehaviour
{
    Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        transform.forward = _cam.transform.position - transform.position;
    }
}
