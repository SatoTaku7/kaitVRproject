using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public GameObject score_object = null; // Text�I�u�W�F�N�g
    public int score_num = 0; // �X�R�A�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
            Text score_text = score_object.GetComponent<Text>();
            // �e�L�X�g�̕\�������ւ���
            score_text.text = "000000";
        }
}
