using System.Collections;
using System.Collections.Generic;
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

[CreateAssetMenu(fileName = "Vector3Type", menuName = "Types/Vector3")]
public class Vector3Type : ScriptableObject
{
    private Vector3 value;

    public Vector3 Value
    {
        get => value;
        set => this.value = value;
    }
}
