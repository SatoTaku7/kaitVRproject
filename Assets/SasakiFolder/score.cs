using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour, IResultManager
{
    public GameObject score_object = null; // Text�I�u�W�F�N�g
    public int score_num = 0; // �X�R�A�ϐ�
    public int score { get; private set; }
    public int maxCombo { get; private set; }
    public int elapsedTime { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        score_object = GameObject.FindWithTag("scoreUI");
        //�X�R�A�����炤
        int mysc;
        mysc = 99;//playerScript.BulletCount;
        Debug.Log("�X�R�A"+mysc+"�_");
    }

    /*public void rankscore()
    {
        int[] higher = { 0, 0, 0, 0 };
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

        Debug.Log("1��"+higher[0]);
        Debug.Log("�Q��"+higher[1]);
        Debug.Log("�R��"+higher[2]);

        //�z��̃T�C�Y��4�ɂ���
        //�X�R�A�̏����l��S��0�ɂ���
        //�C�ӂ̐�����z��ɑ������
        //�o�u���\�[�g����
        //�A���Ă����z����f�o�b�N���O�ɕ\��������
    }*/

    // Update is called once per frame
    void Update()
    {
        //SetRecord(100,00,1);
    }

    public void SetRecord(int score, int maxCombo, int elapsedTime)
    {
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        TextMeshProUGUI score_text = score_object.GetComponent<TextMeshProUGUI>();
        // �e�L�X�g�̕\�������ւ���
        score_text.text = "Score:" + score;
    }
}