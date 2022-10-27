using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour, IGunManager
{
    #region �錾�̐܂���
    IStateChanger stateChanger;
    ICombo combo;
    [SerializeField] GameObject LGun, RGun;
    [SerializeField] Transform LGun_trans, LGun_trajectory, LGun_Trigger;//���̏e�̈ʒu�E�O���ʒu�E�g���K�[
    [SerializeField] Transform RGun_trans, RGun_trajectory, RGun_Trigger;//�E�̏e�̈ʒu�E�O���ʒu�E�g���K�[
    LineRenderer lineRenderer_L, lineRenderer_R;//���C�U�[�|�C���^�[���E
    float StartWidth, EndWidth;//���C�U�[�|�C���^�[���E�@����
    private int bullet_countL, bullet_countR;//���E���ꂼ��̎c�e��
    private int layerMask =   1 << 6;//�I�ɂ̂�Ray��������悤��
    private bool InfiniteMode, LongRayMode;//�p���[�A�b�v���̏���
    private float difference;//���C�U�[�̏o��p�x����
    [SerializeField] GameObject GunFire_L, GunFire_R;//�������ۂ̉�
    [SerializeField] GameObject LeftHandAnchor, RightHandAnchor;//���E�̎�̉�]���擾
    [SerializeField] GameObject trajectory_line;//�g���C���̃v���n�u���A�^�b�`
    private GameObject OVRCam;

    //�e�̐F�擾
    [SerializeField] MeshRenderer[] RedGuncolor;
    [SerializeField] MeshRenderer[] BlueGuncolor;
    [SerializeField] Material[] GunColor;
    #endregion
    void Start()
    {
        lineRenderer_L = LGun.GetComponent<LineRenderer>();
        lineRenderer_R = RGun.GetComponent<LineRenderer>();
        bullet_countL = 1;
        bullet_countR = 1;
        InfiniteMode = false;//�e�������[�h�ɂȂ�@�@�@�@�@�J�����͏�ɂ��̃��[�h�ɂ��Ă���
        LongRayMode = false;//���C�U�[�|�C���g�𒷂�����
        stateChanger = GameObject.FindGameObjectWithTag("GameController").GetComponent<IStateChanger>();
        combo = GameObject.FindGameObjectWithTag("GameController").GetComponent<ICombo>();
        GunFire_L.SetActive(false);
        GunFire_R.SetActive(false);
        difference = 17.5f;
        OVRCam = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        //���C�U�[�̏o�������A�p���[�A�b�v��ԂŒe���ƃ��C�U�[�̒������ω����鏈��
        GunMode();
        ///�R���g���[���[�ő��삵���Ƃ��̏���///
        Gun_Controller();//�v���C���[�̉�]�ƃg���K�[���������Ƃ��̏���
    }
    public void GunMode()
    {
        lineRenderer_L.startWidth = StartWidth;
        lineRenderer_L.endWidth = EndWidth;
        lineRenderer_R.startWidth = StartWidth;
        lineRenderer_R.endWidth = EndWidth;
        if (bullet_countL == 1)//���̒e������΃��C�U�[���o��
        {
            lineRenderer_L.SetPosition(0, LGun_trans.position);
            lineRenderer_L.SetPosition(1, LGun_trajectory.position);
            for (int num = 0; num < 5; num++)
            {
                RedGuncolor[num].material = GunColor[0];
            }
        }
        else//���̒e���Ȃ���΃��C�U�[���o��
        {
            lineRenderer_L.SetPosition(0, LGun_trans.position);
            lineRenderer_L.SetPosition(1, LGun_trans.position);
            
            for(int num = 0; num < 5; num++)
            {
                RedGuncolor[num].material = GunColor[2];
            }
        }
        if (bullet_countR == 1)//�E�̒e������΃��C�U�[���o��
        {
            lineRenderer_R.SetPosition(0, RGun_trans.position);
            lineRenderer_R.SetPosition(1, RGun_trajectory.position);
            for (int num = 0; num < 5; num++)
            {
                BlueGuncolor[num].material = GunColor[1];
            }
        }
        else//�E�̒e���Ȃ���΃��C�U�[���o��
        {
            lineRenderer_R.SetPosition(0, RGun_trans.position);
            lineRenderer_R.SetPosition(1, RGun_trans.position);
            for (int num = 0; num < 5; num++)
            {
                BlueGuncolor[num].material = GunColor[2];
            }
        }
        if (InfiniteMode == false)//�e�������[�h����Ȃ��Ƃ��͐�����\�L
        {
            GunFire_L.SetActive(false);
            GunFire_R.SetActive(false);
        }
        else//�e�������[�h�Ŗ����\�L
        {
            GunFire_L.SetActive(true);
            GunFire_R.SetActive(true);
        }
        if (LongRayMode == false)//���C�U�[�������[�h����Ȃ��Ƃ��̓��C�U�[�̒����Ƒ������������Ȃ�
        {
            StartWidth = 0.0025f;
            EndWidth = 0.0001f;
            LGun_trajectory.localPosition = new Vector3(LGun_trajectory.localPosition.x, 342f, LGun_trajectory.localPosition.z);
            RGun_trajectory.localPosition = new Vector3(RGun_trajectory.localPosition.x, 342f, RGun_trajectory.localPosition.z);
        }
        else//���C�U�[�������[�h�̂Ƃ��̓��C�U�[�̒����Ƒ������傫���Ȃ�@
        {
            StartWidth = 0.01f;
            EndWidth = 0.01f;
            LGun_trajectory.localPosition = new Vector3(LGun_trajectory.localPosition.x, 8420f, LGun_trajectory.localPosition.z);
            RGun_trajectory.localPosition = new Vector3(RGun_trajectory.localPosition.x, 8420f, RGun_trajectory.localPosition.z);
        }
    }
    public void Gun_Controller()
    { 
        //�g���K�[���������Ƃ��̏���
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))//���g���K�[���������Ƃ�
        {
            LGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
            if (bullet_countL == 1)
            {
                Instantiate(trajectory_line, LGun_trans.transform.position, LeftHandAnchor.transform.rotation * Quaternion.Euler(difference, 0, 0));
            }
            else
            {
                    SoundManager.Instance.PlaySeByName("se_gun_Dont02", transform.GetChild(0).GetChild(0).GetChild(4).gameObject);
            }
            Ray(0);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))//���g���K�[��߂����Ƃ�
        {
            LGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))//�E�g���K�[���������Ƃ�
        {
            RGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
            if (bullet_countR == 1) { Instantiate(trajectory_line, RGun_trans.transform.position, RightHandAnchor.transform.rotation * Quaternion.Euler(difference, 0, 0)); }
            else
            {
                SoundManager.Instance.PlaySeByName("se_gun_Dont02", transform.GetChild(0).GetChild(0).GetChild(4).gameObject);
            }
            Ray(1);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))//�E�g���K�[��߂����Ƃ�
        {
            RGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }


        if (bullet_countL == 0 && bullet_countR == 0 )//�����̒e��0�ɂȂ����Ƃ�
        {
            if(stateChanger.currentState == IStateChanger.GameState.Game)
            {
                stateChanger.ChangeState(IStateChanger.GameState.Result);
            }
            
        }

        //�E�X�e�B�b�N���񂵂��Ƃ��Ƀv���C���[����]���鏈��
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickLeft))
        {
            OVRCam.transform.Rotate(0, -45f, 0);
            Debug.Log("�E�̃W���C�X�e�B�b�N�����։�");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickRight))
        {
            OVRCam.transform.Rotate(0, 45f, 0);
            Debug.Log("�E�̃W���C�X�e�B�b�N���E�։�");
        }
    }
    IEnumerator Vibration(int LorR)//���Ă����̐U������
    {
        if (LorR == 0)
        {
            OVRInput.SetControllerVibration(1.0f, 0.6f, OVRInput.Controller.LTouch);
            yield return new WaitForSeconds(0.1f);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
        if(LorR == 1)
        {
            OVRInput.SetControllerVibration(1.0f, 0.6f, OVRInput.Controller.RTouch);
            yield return new WaitForSeconds(0.1f);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }


      public void Ray(int LorR)//�g���K�[�������ꂽ�Ƃ� �@�����͍����E��
    {
        //���������I�̎�ނ��m�F����p�̃X�N���v�g
        Ray ray_L = new Ray(LGun_trans.position, LGun_trajectory.position - LGun_trans.position);
        Ray ray_R = new Ray(RGun_trans.position, RGun_trajectory.position - RGun_trans.position);
        RaycastHit hitobj;
        if (LorR == 0)//���g���K�[���������Ƃ�(���e����������)
        {
            if (Physics.Raycast(ray_L, out hitobj, 200, layerMask))//�����I�܂��̓X�^�[�g�{�^����UI�ɓ��������Ƃ�
            {
                Hit(0, hitobj);
            }
            else//�������ɂ��G��Ă��Ȃ��ꍇ�@�e��0&�R���{���Z�b�g
            {
                if (stateChanger.currentState == IStateChanger.GameState.Game)//�v���C���[�h�������Ƃ�
                {
                    if (InfiniteMode) return;
                   SoundManager.Instance.PlaySeByName("se_gun_Dont02", transform.GetChild(0).GetChild(0).GetChild(4).gameObject);
                    bullet_countL = 0;
                    ResetCombo();
                }
            }
        }

        if (LorR == 1)//�E�g���K�[���������Ƃ�(�E�e����������)
        {
            if (Physics.Raycast(ray_R, out hitobj, 200, layerMask))//�E���I�܂��̓X�^�[�g�{�^����UI�ɓ��������Ƃ�
            {
                Hit(1, hitobj);
            }
            else //�E�����ɂ��G��Ă��Ȃ��ꍇ�@�e��0 & �R���{���Z�b�g
            {
                if (stateChanger.currentState == IStateChanger.GameState.Game)//�v���C���[�h�������Ƃ�
                {
                    if (InfiniteMode) return;
                    SoundManager.Instance.PlaySeByName("se_gun_Dont02", transform.GetChild(0).GetChild(0).GetChild(5).gameObject);
                    bullet_countR = 0;
                    ResetCombo();
                }
            }
        }
    }
    public void Hit(int LorR, RaycastHit hitobj)//�ˌ����ɓI�ɓ��������Ƃ��ɌĂ΂��֐�
    {
        if (LorR == 0)
        {
            if (stateChanger.currentState == IStateChanger.GameState.Title)//�v���C���[�h�������Ƃ�
            {
                if (hitobj.collider.gameObject.layer == 6)//���e�ŃX�^�[�g�I�𓖂Ă���
                {
                    StartCoroutine(Vibration(0));
                }
            }
            if (stateChanger.currentState == IStateChanger.GameState.Game)//�v���C���[�h�������Ƃ�
            {
                if (hitobj.collider.gameObject.layer == 6 && bullet_countL == 1)//���e�œI�𓖂Ă���
                {

                    var TargetColor = hitobj.collider.gameObject.GetComponentInParent<TargetInformation>().color;//�^�[�Q�b�g�̐F
                                                                                                                 //�I�̐F�@0���ԁ@1���@2���D�F  3�������I
                    if (TargetColor == 1)
                    {
                        SoundManager.Instance.PlaySeByName("se_nogood11");
                        if (InfiniteMode) return;
                        bullet_countL = 0;
                        ResetCombo();
                    }
                    else
                    {
                        StartCoroutine(Vibration(0));
                    }
                    hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(1);//���̏e�̐F������
                }
                else
                {
                    if (InfiniteMode) return;
                    bullet_countL = 0;
                    ResetCombo();
                }
            }
            else
            {
                Reload();
                if (hitobj.collider.gameObject.layer == 6)
                {
                    hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(1);
                }
            }
        }
        else
        {
            if (stateChanger.currentState == IStateChanger.GameState.Title)//�v���C���[�h�������Ƃ�
            {
                if (hitobj.collider.gameObject.layer == 6)//���e�ŃX�^�[�g�I�𓖂Ă���
                {
                    StartCoroutine(Vibration(1));
                }
            }
            if (stateChanger.currentState == IStateChanger.GameState.Game)//�v���C���[�h�������Ƃ�
            {
                if (hitobj.collider.gameObject.layer == 6 && bullet_countR == 1)//�E�e�œI�𓖂Ă���
                {
                    var TargetColor = hitobj.collider.gameObject.GetComponentInParent<TargetInformation>().color;//�^�[�Q�b�g�̐F
                    if (TargetColor == 0)
                    {
                        SoundManager.Instance.PlaySeByName("se_nogood11", gameObject);
                        if (InfiniteMode) return;
                        bullet_countR = 0;
                        ResetCombo();
                    }
                    else
                    {
                        StartCoroutine(Vibration(1));
                    }
                    hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(2);//���̏e�̐F������
                }
                else//�I�̐F����������e��0�ɂȂ�
                {
                    if (InfiniteMode) return;
                    bullet_countR = 0;
                    ResetCombo();
                }
            }
            else
            {
                Reload();
                if (hitobj.collider.gameObject.layer == 6)
                {
                    hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(1);
                }
            }
        }
       
    }

    public void Reload()
    {
        bullet_countL = 1;
        bullet_countR = 1;
    }
    public void PowerUp()
    {
        InfiniteMode = true;
        LongRayMode = true;
        SoundManager.Instance.PlaySeByName("GunReload", gameObject);
    }
    public void PowerDown()
    {
        InfiniteMode = false;
        LongRayMode = false;
        bullet_countL = 1;
        bullet_countR = 1;

    }
    public void ResetCombo()
    {
        combo.ResetCombo();
    }
    public void GameOver()
    {
    }
}





