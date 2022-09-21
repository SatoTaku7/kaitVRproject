using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    [SerializeField] GameObject UI;
    public void OnClicked()
    {
        UI.SetActive(false);
    }
}
