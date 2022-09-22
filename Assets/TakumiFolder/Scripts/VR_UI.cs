using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_UI : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private  GunManager gunManager;
    [SerializeField] GameObject Button;

    void Start()
    {
        gunManager = Player.GetComponent<GunManager>();
    }


    void Update()
    {
        if (gunManager.ButtonClicked)//�X�^�[�g�{�^���������ꂽ��Canvas��������
        {
            if (Button.gameObject.name == gunManager.ButtonName)
            {
                Button.gameObject.SetActive(false);
            }
        }
        if (gunManager.is_game_over)//�Q�[���I�[�o�[���Ă΂ꂽ��X�^�[�g�{�^�����o��
        {
            Button.gameObject.SetActive(true);
            gunManager.ButtonClicked = false;
        }
    }
}
