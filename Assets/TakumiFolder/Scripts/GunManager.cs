using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour, IGunManager
{
    [SerializeField] GameObject LGun, RGun;
    [SerializeField] Transform LGun_trans, LGun_trajectory, LGun_Trigger;//���̏e�̈ʒu�E�O���ʒu�E�g���K�[
    [SerializeField] Transform RGun_trans, RGun_trajectory, RGun_Trigger;//�E�̏e�̈ʒu�E�O���ʒu�E�g���K�[
    LineRenderer lineRenderer_L, lineRenderer_R;//���C�U�[�|�C���^�[���E
    [SerializeField] float StartWidth, EndWidth;//���C�U�[�|�C���^�[���E�@����
    [SerializeField] Text textbullet_countL, textbullet_countR;//���̎c�e��UI���E
    private int bullet_countL, bullet_countR;//���E���ꂼ��̎c�e��
    private int layerMask = 1 << 7 | 1 << 6;
    [SerializeField] bool InfiniteMode, LongRayMode;

    public string ButtonName;//�N���b�N���ꂽ�{�^���̖��O���Q�Ɓ@oculus�W���̋@�\���g���Ȃ������̂ŕʂ̂����ő��
    public bool ButtonClicked;//�{�^�����N���b�N���ꂽ���ǂ����@oculus�W���̋@�\���g���Ȃ������̂ŕʂ̂����ő��
    public bool is_playmode, is_game_over;//�Q�[�������ǂ����E�Q�[���I�[�o�[����

    void Start()
    {
        lineRenderer_L = LGun.GetComponent<LineRenderer>();
        lineRenderer_R = RGun.GetComponent<LineRenderer>();
        bullet_countL = 1;
        bullet_countR = 1;
        ButtonClicked = false;
        InfiniteMode = false;//�e�������[�h�ɂȂ�@�@�@�@�@�J�����͏�ɂ��̃��[�h�ɂ��Ă���
        LongRayMode = true;//���C�U�[�|�C���g�𒷂�����
        is_playmode = false;
        is_game_over = false;


    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer_L.startWidth = StartWidth;
        lineRenderer_L.endWidth = EndWidth;
        lineRenderer_R.startWidth = StartWidth;
        lineRenderer_R.endWidth = EndWidth;
        if (InfiniteMode == false)//�e�������[�h����Ȃ��Ƃ��͐�����\�L
        {
            textbullet_countL.text = bullet_countL.ToString();
            textbullet_countR.text = bullet_countR.ToString();
        }
        else
        {
            textbullet_countL.text = "��";
            textbullet_countR.text = "��";
        }
        if (LongRayMode == false)
        {
            StartWidth = 0.001f;
            EndWidth = 0.0001f;
        }
        else
        {
            StartWidth = 0.01f;
            EndWidth = 0.01f;
        }

        lineRenderer_L.SetPosition(0, LGun_trans.position);
        lineRenderer_L.SetPosition(1, LGun_trajectory.position);
        lineRenderer_R.SetPosition(0, RGun_trans.position);
        lineRenderer_R.SetPosition(1, RGun_trajectory.position);
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))//���g���K�[���������Ƃ�
        {

            LGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
            ButtonClicked = true;
            Ray(1);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))//���g���K�[��߂����Ƃ�
        {
            LGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
            ButtonClicked = false;
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))//�E�g���K�[���������Ƃ�
        {

            RGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
            ButtonClicked = true;
            Ray(2);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))//�E�g���K�[��߂����Ƃ�
        {
            RGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
            ButtonClicked = false;
        }

        if (bullet_countL == 0 && bullet_countR == 0)
        {
            GameOver();
        }
        Debug.Log("is_playmode");

    }
    public void Ray(int LorR)//�g���K�[�������ꂽ�Ƃ� �����͍����E��
    {
        //���������I�̎�ނ��m�F����p�̃X�N���v�g
        Ray ray_L = new Ray(LGun_trans.position, LGun_trajectory.position - LGun_trans.position);
        Ray ray_R = new Ray(RGun_trans.position, RGun_trajectory.position - RGun_trans.position);
        RaycastHit hitobj;
        if (LorR == 1)//���g���K�[���������Ƃ�
        {
            if (Physics.Raycast(ray_L, out hitobj, 200, layerMask))//�������������Ƃ�
            {
                if (hitobj.collider.gameObject.layer == 7)//UI�̃{�^���̎�
                {
                    //bullet_countL = 1;//UI���^�b�`�����Ƃ��͒e����1�ɂ���A�m�[�J���̏�����㗝�ł����Ƃ����B�o�O�̌��ɂȂ肻���Ȃ̂ł̂Ō�Œ����܂�
                    ButtonName = hitobj.collider.gameObject.name;
                    bullet_countL = 1;
                    bullet_countR = 1;
                    Debug.Log("UI" + hitobj.collider.gameObject.name + "���m�F�B�e����1�ɂ��܂��B");
                }
                if (is_playmode)//�v���C���[�h�������Ƃ�
                {
                    if (InfiniteMode == true)//�������[�h�������Ƃ�
                    {
                        if (hitobj.collider.gameObject.layer == 6)//�I�ɓ��������Ƃ�
                        {
                            hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(1);
                            Reload();
                            Debug.Log(hitobj.collider.gameObject.name + ":�Փ˂����I�u�W�F�N�g");
                        }
                    }
                    else//�ʏ탂�[�h�������Ƃ�
                    {
                        if (hitobj.collider.gameObject.layer == 6 && bullet_countL == 1)//�I�ɓ��������Ƃ�
                        {
                            hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(1);
                            Reload();
                            Debug.Log(hitobj.collider.gameObject.name + ":�Փ˂����I�u�W�F�N�g");
                        }
                    }
                }
            }
            else bullet_countL = 0;

        }
        if (LorR == 2)//�E�g���K�[���������Ƃ�
        {
            if (Physics.Raycast(ray_R, out hitobj, 200, layerMask))//�E�����������Ƃ�
            {
                if (hitobj.collider.gameObject.layer == 7)//UI�̃{�^���̎�
                {
                    //bullet_countR = 1;//UI���^�b�`�����Ƃ��͒e����1�ɂ���A�m�[�J���̏�����㗝�ł����Ƃ����B�o�O�̌��ɂȂ肻���Ȃ̂ł̂Ō�Œ����܂�
                    ButtonName = hitobj.collider.gameObject.name;
                    bullet_countL = 1;
                    bullet_countR = 1;
                    Debug.Log("UI" + hitobj.collider.gameObject.name + "���m�F�B�e����1�ɂ��܂��B");
                }
                if (is_playmode)//�v���C���[�h�������Ƃ�
                {
                    if (InfiniteMode == true)//�������[�h�������Ƃ�
                    {
                        if (hitobj.collider.gameObject.layer == 6)//�I�ɓ��������Ƃ�
                        {
                            hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(2);
                            Reload();
                            Debug.Log(hitobj.collider.gameObject.name + ":�Փ˂����I�u�W�F�N�g");
                        }
                    }
                    else//�ʏ탂�[�h�������Ƃ�
                    {
                        if (hitobj.collider.gameObject.layer == 6 && bullet_countR == 1)//�I�ɓ��������Ƃ�
                        {
                            hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(2);
                            Reload();
                            Debug.Log(hitobj.collider.gameObject.name + ":�Փ˂����I�u�W�F�N�g");
                        }
                    }
                }
            }
            else bullet_countR = 0;
        }
        Debug.DrawRay(LGun_trans.position, LGun_trajectory.position - LGun_trans.position, Color.red);
        Debug.DrawRay(RGun_trans.position, RGun_trajectory.position - RGun_trans.position, Color.red);
    }

    public void Reload()
    {
        bullet_countL = 1;
        bullet_countR = 1;
    }
    public void PowerUp()
    {

    }
    public void GameOver()
    {
        is_game_over = true;
        is_playmode = false;
    }
}




