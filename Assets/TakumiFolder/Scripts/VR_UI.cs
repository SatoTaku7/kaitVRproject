using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_UI : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private GunManager gunManager;
    [SerializeField] GameObject Button;
    IStateChanger stateChanger;

    void Start()
    {
        gunManager = Player.GetComponent<GunManager>();
        stateChanger = GameObject.FindGameObjectWithTag("GameController").GetComponent<IStateChanger>();
    }


    void Update()
    {
        if (stateChanger.currentState == IStateChanger.GameState.Title)//�Q�[���I�[�o�[���Ă΂ꂽ��X�^�[�g�{�^�����o��
        {
            Button.gameObject.SetActive(true);//�^�C�g���̎��̓{�^����\��
            ButtonClicked();//�{�^���̃N���b�N�����m�����ꍇ�̊֐��c�{�^������\���ɂȂ�Q�[����State��ύX����
        }
    }

    void ButtonClicked()
    {
        //if (gunManager.ButtonClicked==true)//�X�^�[�g�{�^���������ꂽ��{�^����������
        //{
        //    if (Button.gameObject.name == gunManager.ButtonName)//�N���b�N�������̂��X�^�[�g�{�^���������Ƃ��@�{�^���������ăQ�[����State��"Game"�ɂȂ�
        //    {
        //        // Debug.Log("�{�^����:" + gunManager.ButtonClicked);
        //        Button.gameObject.SetActive(false);
        //        stateChanger.ChangeState(IStateChanger.GameState.Game);
        //    }
        //}
    }
}
