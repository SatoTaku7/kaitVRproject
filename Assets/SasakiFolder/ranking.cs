using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ranking : MonoBehaviour
{
    public GameObject rank_object = null; // Text�I�u�W�F�N�g

    public int[] higher = { 0, 0, 0, 0 };
    // Start is called before the first frame update
    void Start()
    {
        rankscore();
    }

    public void uketori(ref int sco)
    {
        //�X�R�A���󂯎��
    }

    public void rankscore()
    {
        
        //�X�R�A�����炤
        //higher[3]=playerScript.BulletCount;

        if (higher[3] >= higher[2])
        {
            higher[2] = higher[3];
        }

        else if (higher[3] >= higher[1])
        {
            higher[2] = higher[1];
            higher[1] = higher[3];
        }

        else if (higher[3] >= higher[0])
        {
            higher[2] = higher[1];
            higher[1] = higher[0];
            higher[0] = higher[3];
        }
        else { };

        Debug.Log("1��" + higher[0]);
        Debug.Log("�Q��" + higher[1]);
        Debug.Log("�R��" + higher[2]);

        //�z��̃T�C�Y��4�ɂ���
        //�X�R�A�̏����l��S��0�ɂ���
        //�C�ӂ̐�����z��ɑ������
        //�o�u���\�[�g����
        //�A���Ă����z����f�o�b�N���O�ɕ\��������
    }

    // Update is called once per frame
    void Update()
    {
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        Text rank_text = rank_object.GetComponent<Text>();
        // �e�L�X�g�̕\�������ւ���
        rank_text.text = "1��" + higher[0] + "\n�Q��" + higher[1] + "\n�R��" + higher[2];
    }
}
