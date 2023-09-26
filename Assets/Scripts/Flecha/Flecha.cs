using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    Vector3 rot;
    
    void Update()
    {
        transform.LookAt(target);
        rot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(rot.x + 90, rot.y + 90, rot.z);
    }
}