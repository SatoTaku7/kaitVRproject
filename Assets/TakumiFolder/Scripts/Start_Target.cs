using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Start_Target : MonoBehaviour,IGunBreakTarget
{
    private bool Hit = false;
    private TargetManager _manager;
    private TargetInformation _info;
    private GameObject Player;

    IStateChanger stateChanger;
    private void Start()
    {
        //�v���C���[�̕����������@��ŏC��
        _manager = GetComponentInParent<TargetManager>();
        _info = GetComponent<TargetInformation>();
        Player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(Player.transform.position);
        var rotation = Random.Range(0, 360);
        transform.Rotate(new Vector3(0, 0, rotation));
        stateChanger = GameObject.FindGameObjectWithTag("GameController").GetComponent<IStateChanger>();
    }

    public void BreakTarget(int gunColor)
    {
        stateChanger.ChangeState(IStateChanger.GameState.Game);
        //�e�̐F�Ɠ������ǂ����̔���
        if (_info.color != 2 && _info.color != (gunColor - 1))
        {
            //�F���Ⴄ
            Debug.Log("�e�̐F�ƓI�̐F���قȂ�܂��I");
            //�R���{�����Z�b�g
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
            r.AddForce( Physics.gravity, ForceMode.Impulse);
            r.transform.SetParent(null);
            r.gameObject.AddComponent<AutoDestroy>().time = 0.2f;
            r.gameObject.layer = 8;
            var vect = new Vector3(random.Next(min, max), random.Next(0, max), random.Next(min, max));
            r.AddForce(vect, ForceMode.Impulse);
            r.AddTorque(vect, ForceMode.Impulse);
        });
        Destroy(gameObject);
    }
}
