using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_UI : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private GunManager gunManager;
    [SerializeField] GameObject Button;

    void Start()
    {
        gunManager = Player.GetComponent<GunManager>();
    }


    void Update()
    {
        if (gunManager.ButtonClicked)//�X�^�[�g�{�^���������ꂽ��{�^����������
        {
            Debug.Log("�{�^���N���b�N:"+gunManager.ButtonClicked);
            if (Button.gameObject.name == gunManager.ButtonName)
            {
                Debug.Log("�{�^����:" + gunManager.ButtonClicked);
                Button.gameObject.SetActive(false);
                gunManager.is_playmode = true;
                gunManager.is_game_over = false;
            }
        }

        if (gunManager.is_game_over)//�Q�[���I�[�o�[���Ă΂ꂽ��X�^�[�g�{�^�����o��
        {
            Button.gameObject.SetActive(true);
            gunManager.is_playmode = false;
        }
    }
}
