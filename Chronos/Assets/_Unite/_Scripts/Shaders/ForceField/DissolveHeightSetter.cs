using UnityEngine;

/// <summary>
/// Resource:https://www.youtube.com/watch?v=jdAbVkre8cw
/// Accessed 10/03/24
/// </summary>
public class DissolveHeightSetter : MonoBehaviour
{
    Renderer _rend;
    // Start is called before the first frame update
    void Start()
    {
        _rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _rend.material.SetFloat("_DissolveStartHeight", transform.position.y);
    }
}