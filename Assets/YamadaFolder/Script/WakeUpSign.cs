using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpSign : MonoBehaviour
{
    private float time;
    private float waitTime = 3f;
    void Update()
    {
        if (transform.eulerAngles.x <= 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            time += Time.deltaTime;
            if (time > waitTime)
                Destroy(gameObject);
        }
        else
            transform.Rotate(-0.3f, 0, 0);
    }
}
