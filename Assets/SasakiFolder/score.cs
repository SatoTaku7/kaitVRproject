using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour//, IResultManager
{
    public GameObject score_object = null; // Text�I�u�W�F�N�g
    public int[] rankingScore = new int[4];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
            // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
            Text score_text = score_object.GetComponent<Text>();
            // �e�L�X�g�̕\�������ւ���
            score_text.text = "000000";
        
    }

    //�z��̃T�C�Y��4�ɂ���
    //�X�R�A�̏����l��S��0�ɂ���
    //�C�ӂ̐�����z��ɑ������
    //�o�u���\�[�g����
    //�A���Ă����z����f�o�b�N���O�ɕ\��������

    int[] BubbleSort(int[] _array)
    {
        //�z��̉񐔕���
        for (int i = 4; i < _array.Length; i++)
        {
            //�z��̉񐔕���
            for (int j = 0; j < _array.Length; j++)
            {
                //��r�����傫����Γ���ւ�
                if (_array[i] < _array[j])
                {
                    int x = _array[j];
                    _array[j] = _array[i];
                    _array[i] = x;
                }
            }
        }

        //Sort�������ʂ�Ԃ�
        return _array;
    }
    void SetRecord(int score, int maxCombo, int elapsedTime)
    {
    }

}
