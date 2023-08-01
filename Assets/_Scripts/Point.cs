using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        MeshRenderer mr;
        bool success = TryGetComponent<MeshRenderer>(out mr);
        if (success)
        {
            mr.enabled = (Application.isEditor);
        }
    }
}
