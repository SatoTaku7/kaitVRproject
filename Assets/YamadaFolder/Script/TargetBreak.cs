using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetBreak : MonoBehaviour
{
    private bool Hit = false;
    private TargetManager _manager;
    private TargetInformation _info;

    private void Start()
    {
        //ƒvƒŒƒCƒ„[‚Ì•ûŒü‚ğŒü‚­@Œã‚ÅC³
        _manager = GetComponentInParent<TargetManager>();
        _info = GetComponent<TargetInformation>();
        transform.LookAt(_manager.gameObject.transform.position);
        var rotation = Random.Range(0, 360);
        transform.Rotate(new Vector3(0, 0, rotation));
    }

    public void destroyObject()
    {
        var random = new System.Random();
        var min = -5;
        var max = 5;
        gameObject.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => {
            r.isKinematic = false;
            r.transform.SetParent(null);
            r.gameObject.AddComponent<AutoDestroy>().time = 2f;
            r.gameObject.layer = 6;
            var vect = new Vector3(random.Next(min, max), random.Next(0, max), random.Next(min, max));
            r.AddForce(vect, ForceMode.Impulse);
            r.AddTorque(vect, ForceMode.Impulse);
        });
        if (Hit == false)
        {
            Hit = true;
            Debug.Log("Hit");
            _manager.HitTarget(_info.num, _info.color, _info.size); //“–‚½‚Á‚½‚±‚Æ‚ğ“`‚¦‚é
        }
        Destroy(gameObject);
    }

    //‚Ô‚Â‚©‚Á‚½‚Ée‘¤‚É‚µ‚Ä—~‚µ‚¢‚±‚Æ—á
    void OnCollisionEnter(Collision collision)//ƒŒƒC‚É“–‚½‚Á‚½‚È‚ç
    {
        if (collision.gameObject.layer != 0) return;//“I‚Å‚È‚¢‚È‚çreturn
        TargetBreak tb = collision.gameObject.GetComponentInParent<TargetBreak>();
        tb.destroyObject();//‚±‚±‚ğinterface‚É‚·‚é‚©‚à?
    }

}
