using UnityEngine;
using System.Collections;

public class NP_GhostEye : MonoBehaviour
{
    [SerializeField]
    private Transform lookAtTarget;

    void Start()
    {
        if (lookAtTarget == null)
        {
            Debug.LogError("[Ghost Eye] Look At Target not assigned");
        }
    }

    void Update()
    {
        transform.LookAt(lookAtTarget);
    }
}
