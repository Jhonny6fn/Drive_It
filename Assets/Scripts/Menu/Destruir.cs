using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruir : MonoBehaviour
{
    public void DestruirCanvas()
    {
        Destroy(this.gameObject);
    }
}