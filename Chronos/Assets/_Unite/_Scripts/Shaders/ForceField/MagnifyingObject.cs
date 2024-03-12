using UnityEngine;

/// <summary>
/// Resource:https://www.youtube.com/watch?v=jdAbVkre8cw
/// Accessed 09/03/24
/// </summary>
public class MagnifyingObject : MonoBehaviour
{
    Renderer _renderer;
    Camera _cam;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = _cam.WorldToScreenPoint(transform.position);
        screenPoint.x = screenPoint.x / Screen.width;
        screenPoint.y = screenPoint.y / Screen.height;
        _renderer.material.SetVector("_ObjectScreenPos", screenPoint);
    }
}
