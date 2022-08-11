using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Sphere : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(Detroy_this), 1f);
    }

    void Update()
    {
        
    }
    public void Detroy_this()
    {
        Destroy(gameObject);
        Debug.Log("Destroy");
    }
}
