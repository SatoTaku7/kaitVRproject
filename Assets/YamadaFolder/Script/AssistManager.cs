using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistManager : MonoBehaviour
{
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
            posX = Random.Range(-70, -35);
        else
            posX = Random.Range(35, 70);
        ran = Random.Range(0, 2);
        if (ran == 1)
            posY = Random.Range(-10, 2.5f);
        else
            posY = Random.Range(27.5f, 40);

        //�������I�̐��� color 3
        GameObject ins;
        TargetInformation info;
        ins = Instantiate(AssistTargetObj, new Vector3(posX, posY, 70), Quaternion.identity, this.gameObject.transform);
        ins.transform.localScale = new Vector3(5 * 100f, 5 * 100f, 5 * 100f);
        info = ins.GetComponent<TargetInformation>();
        Debug.Log("�������I�𐶐��I");
    }

    //�������I���j�󂳂ꂽ
    public void HitTarget()
    {
        
    }
}
