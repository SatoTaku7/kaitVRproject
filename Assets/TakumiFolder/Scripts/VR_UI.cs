using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_UI : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private  GunManager gunManager;
    [SerializeField] GameObject Button;

    void Start()
    {
        gunManager = Player.GetComponent<GunManager>();
    }


    void Update()
    {
        if (gunManager.ButtonClicked)
        {
            if (Button.gameObject.name == gunManager.ButtonName)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
