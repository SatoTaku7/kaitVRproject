using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AssistTarget : MonoBehaviour, IGunBreakTarget
{
    private AssistManager _manager;
    private bool Hit = false;
    // Start is called before the first frame update
    void Start()
    {
        _manager = GetComponentInParent<AssistManager>();
    }

    private void Update()
    {
        //é©ï™ÇÃêßå¿éûä‘ÇÃä«óù
    }

    //Ç®èïÇØìIÇÃîjâÛ
    public void BreakTarget(int gunColor)
    {
        var random = new System.Random();
        var min = -5;
        var max = 5;
        gameObject.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => {
            var c = r.gameObject.GetComponent<MeshCollider>();
            c.isTrigger = true;
            r.isKinematic = false;
            r.AddForce(2 * Physics.gravity, ForceMode.Impulse);
            r.transform.SetParent(null);
            r.gameObject.AddComponent<AutoDestroy>().time = 0.2f;
            r.gameObject.layer = 6;
            var vect = new Vector3(random.Next(min, max), random.Next(0, max), random.Next(min, max));
            r.AddForce(vect, ForceMode.Impulse);
            r.AddTorque(vect, ForceMode.Impulse);
        });
        if (Hit == false)
        {
            Hit = true;
            Debug.Log("Hit");
            _manager.HitTarget();
        }
        Destroy(gameObject);
    }
}
