using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistManager : MonoBehaviour
{
    IBreakTargetChecker breakTargetChecker;
    [SerializeField] GameObject AssistTargetObj;
    // Start is called before the first frame update
    void Start()
    {

    }

    //�������I�̐���
    public void GenerateAssistTarget()
    {
        //���ɂ������I������Ȃ�return
        var children = new Transform[this.transform.childCount];
        Debug.Log(children.Length);
        if (children.Length != 0) return;

        //�������I�̐����ʒu������
        int ran = Random.Range(0, 2);
        float posX, posY;
        if (ran == 1)
            posX = Random.Range(-10, -5);
        else
            posX = Random.Range(5, 10);
        ran = Random.Range(0, 2);
        if (ran == 1)
            posY = Random.Range(-1, 1);
        else
            posY = Random.Range(5f, 7.5f);

        //�������I�̐��� color 3
        GameObject ins;
        TargetInformation info;
        ins = Instantiate(AssistTargetObj, new Vector3(posX, posY, 10), Quaternion.identity, this.gameObject.transform);
        ins.transform.localScale = new Vector3(100f,100f,100f);
        info = ins.GetComponent<TargetInformation>();
        Debug.Log("�������I�𐶐��I");
    }

    //�������I���j�󂳂ꂽ
    public void HitTarget()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<IBreakTargetChecker>().BreakAssistTarget();
    }

    //�������I�̑����j��
    public void Break()
    {
        if(transform.childCount>0)
            Destroy(transform.GetChild(0).gameObject);

    }
}
