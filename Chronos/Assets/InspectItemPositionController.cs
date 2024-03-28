using Unite.Core.Types;
using UnityEngine;

public class InspectItemPositionController : MonoBehaviour
{
    [SerializeField]
    private Vector3Type position;

    // Update is called once per frame
    void Update()
    {
        position.Value = transform.position;
    }
}
