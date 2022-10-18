using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanelCubeCollider : MonoBehaviour
{
    [SerializeField] bool type;
    private TutrialController _tutrial;

    private void Start()
    {
        _tutrial = GetComponentInParent<TutrialController>();
    }

    public void hitCube()
    {
        _tutrial.goNext(type);
    }
}
