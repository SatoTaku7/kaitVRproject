using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Release_Sphere : MonoBehaviour
{
    [SerializeField] GameObject Green_Sphere;
    [SerializeField] GameObject Right_Hand;
    void Start()
    {
        
    }
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Instantiate(Green_Sphere, new Vector3(Right_Hand.transform.position.x, Right_Hand.transform.position.y, Right_Hand.transform.position.z), Quaternion.identity);
        }
    }
}
