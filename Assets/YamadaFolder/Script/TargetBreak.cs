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
        //プレイヤーの方向を向く　後で修正
        _manager = GetComponentInParent<TargetManager>();
        _info = GetComponent<TargetInformation>();
        transform.LookAt(_manager.gameObject.transform.position);
        var rotation = Random.Range(0, 360);
        transform.Rotate(new Vector3(0, 0, rotation));
    }

    public void destroyObject(int gunColor)
    {
        var random = new System.Random();
        var min = -5;
        var max = 5;
        gameObject.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => {
            r.isKinematic = false;
            r.AddForce(2 * Physics.gravity, ForceMode.Impulse);
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
            _manager.HitTarget(_info.num, _info.color, _info.size, gunColor); //当たったことを伝える
        }
        Destroy(gameObject);
    }

    //ぶつかった時に銃側にして欲しいこと例
    void OnCollisionEnter(Collision collision)//レイに当たったなら
    {
        if (collision.gameObject.layer != 0) return;//的でないならreturn
        TargetBreak tb = collision.gameObject.GetComponentInParent<TargetBreak>();
        tb.destroyObject(1/*ここにプレイヤーの銃の色情報を入れる*/);//ここをinterfaceにするかも?
    }

}