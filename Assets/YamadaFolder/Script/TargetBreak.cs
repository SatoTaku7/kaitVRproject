using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetBreak : MonoBehaviour,IGunBreakTarget
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

    public void BreakTarget(int gunColor)
    {
        //銃の色と同じかどうかの判定
        if (_info.color != 2 && _info.color != (gunColor - 1))
        {
            //色が違う
            Debug.Log("銃の色と的の色が異なります！");
            //コンボをリセット
            var GameManager = GameObject.FindGameObjectWithTag("GameController");
            var _combo = GameManager.GetComponent<ComboManager>();
            _combo.ResetCombo();
            return;
        }

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
            _manager.HitTarget(_info.num, _info.color, _info.size, gunColor, transform.position); //当たったことを伝える
        }
        Destroy(gameObject);
    }

    //ぶつかった時に銃側にして欲しいこと例
    void OnCollisionEnter(Collision collision)//レイに当たったなら
    {
        if (collision.gameObject.layer != 0) return;//的でないならreturn
        TargetBreak tb = collision.gameObject.GetComponentInParent<TargetBreak>();
        tb.BreakTarget(1/*ここにプレイヤーの銃の色情報を入れる*/);//ここをinterfaceにするかも?
    }

}