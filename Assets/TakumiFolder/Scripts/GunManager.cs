using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour, IGunManager
{
    IStateChanger stateChanger;
    ICombo combo;
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

    //�F�̏��
    TargetInformation targetInformation;


    void Start()
    {
        lineRenderer_L = LGun.GetComponent<LineRenderer>();
        lineRenderer_R = RGun.GetComponent<LineRenderer>();
        bullet_countL = 1;
        bullet_countR = 1;
        ButtonClicked = false;
        InfiniteMode = false;//�e�������[�h�ɂȂ�@�@�@�@�@�J�����͏�ɂ��̃��[�h�ɂ��Ă���
        LongRayMode = false;//���C�U�[�|�C���g�𒷂�����
        stateChanger = GameObject.FindGameObjectWithTag("GameController").GetComponent<IStateChanger>();
        combo= GameObject.FindGameObjectWithTag("GameController").GetComponent<ICombo>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer_L.startWidth = StartWidth;
        lineRenderer_L.endWidth = EndWidth;
        lineRenderer_R.startWidth = StartWidth;
        lineRenderer_R.endWidth = EndWidth;
        lineRenderer_L.SetPosition(0, LGun_trans.position);
        lineRenderer_R.SetPosition(0, RGun_trans.position);
        lineRenderer_L.SetPosition(1, LGun_trajectory.position);
        lineRenderer_R.SetPosition(1, RGun_trajectory.position);
        if (InfiniteMode == false)//�e�������[�h����Ȃ��Ƃ��͐�����\�L
        {
            textbullet_countL.text = bullet_countL.ToString();
            textbullet_countR.text = bullet_countR.ToString();
        }
        else//�e�������[�h�Ŗ����\�L
        {
            textbullet_countL.text = "��";
            textbullet_countR.text = "��";
        }
     �@ if (LongRayMode == false)//���C�U�[�������[�h����Ȃ��Ƃ��̓��C�U�[�̒����Ƒ������������Ȃ�
     �@ {
            StartWidth = 0.001f;
            EndWidth = 0.0001f;
            LGun_trajectory.localPosition = new Vector3(LGun_trajectory.localPosition.x, 342f, LGun_trajectory.localPosition.z);
            RGun_trajectory.localPosition = new Vector3(RGun_trajectory.localPosition.x, 342f, RGun_trajectory.localPosition.z);
      �@}
      �@else//���C�U�[�������[�h�̂Ƃ��̓��C�U�[�̒����Ƒ������傫���Ȃ�@
        {
            StartWidth = 0.01f;
            EndWidth = 0.01f;
            LGun_trajectory.localPosition = new Vector3(LGun_trajectory.localPosition.x,8420f, LGun_trajectory.localPosition.z);
            RGun_trajectory.localPosition = new Vector3(RGun_trajectory.localPosition.x, 8420f, RGun_trajectory.localPosition.z);
        }
           
        
        ///�������牺�̓v���C���[���R���g���[���[�ő��삵���Ƃ��̏���///
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
        //FIXME:�������[�h���ɗ����Œe���O���ƓI�����Q�[�����I������
        if (bullet_countL == 0 && bullet_countR == 0&&stateChanger.currentState==IStateChanger.GameState.Game)//�����̒e��0�ɂȂ����Ƃ�
        {
            stateChanger.ChangeState(IStateChanger.GameState.Result);

        }
        PlayerRotate();//�v���C���[�̉�]
    }
    public void PlayerRotate()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft))
        {
            this.gameObject.transform.Rotate(0, -45f, 0);
            Debug.Log("���̃W���C�X�e�B�b�N�����։�");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight))
        {
            this.gameObject.transform.Rotate(0, 45f, 0);
            Debug.Log("���̃W���C�X�e�B�b�N���E�։�");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickLeft))
        {
            this.gameObject.transform.Rotate(0, -45f, 0);
            Debug.Log("�E�̃W���C�X�e�B�b�N�����։�");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickRight))
        {
            this.gameObject.transform.Rotate(0, 45f, 0);
            Debug.Log("�E�̃W���C�X�e�B�b�N���E�։�");
        }
    }
    public void Ray(int LorR)//�g���K�[�������ꂽ�Ƃ� �@�����͍����E��
    {
        //���������I�̎�ނ��m�F����p�̃X�N���v�g
        Ray ray_L = new Ray(LGun_trans.position, LGun_trajectory.position - LGun_trans.position);
        Ray ray_R = new Ray(RGun_trans.position, RGun_trajectory.position - RGun_trans.position);
        RaycastHit hitobj;
        if (LorR == 1)//���g���K�[���������Ƃ�(���e����������)
        {
            if (Physics.Raycast(ray_L, out hitobj, 200, layerMask))//�����I�܂��̓X�^�[�g�{�^����UI�ɓ��������Ƃ�
            {
                if (hitobj.collider.gameObject.layer == 7)//UI�̃{�^���ɓ��������Ƃ�
                {
                    ButtonName = hitobj.collider.gameObject.name;//UI�̖��O���擾
                    bullet_countL = 1;
                    bullet_countR = 1;
                    Debug.Log("UI" + hitobj.collider.gameObject.name + "���m�F�B�e����1�ɂ��܂��B");
                }//CHANGED:bool�ł͂Ȃ�stateChanger��currentState���Q�Ƃ���悤�ɕύX
                if (stateChanger.currentState == IStateChanger.GameState.Game)//�v���C���[�h�������Ƃ�
                {
                    
                    if (hitobj.collider.gameObject.layer == 6 && bullet_countL == 1)//���e�œI�𓖂Ă���
                    {
                        var TargetColor = hitobj.collider.gameObject.GetComponentInParent<TargetInformation>().color;//�^�[�Q�b�g�̐F
                        //�I�̐F�@0���ԁ@1���@2���D�F  3�������I
                        if(TargetColor == 1)
                        {
                            if (InfiniteMode) return;
                            bullet_countL = 0;
                            ResetCombo();
                        }
                        //FIXME:�������}�g���������������������G���[���ł�@�܂��ȉ��̂悤�ȃR�[�h�Ŕ��ʂ���ƃC���^�[�t�F�[�X�𗘗p����Ӗ��������Ȃ邱�Ƃɒ���
                        hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(1);//���̏e�̐F������
                    }
                    else
                    {
                        if (InfiniteMode) return;
                        bullet_countL = 0;
                        ResetCombo();
                    }

                }
            }
            else//�������ɂ��G��Ă��Ȃ��ꍇ�@�e��0&�R���{���Z�b�g
            {
                if (stateChanger.currentState == IStateChanger.GameState.Game)//�v���C���[�h�������Ƃ�
                {
                    if (InfiniteMode) return;
                    bullet_countL = 0;
                    ResetCombo();
                }
            }
        }

        if (LorR == 2)//�E�g���K�[���������Ƃ�(�E�e����������)
        {
            if (Physics.Raycast(ray_R, out hitobj, 200, layerMask))//�E���I�܂��̓X�^�[�g�{�^����UI�ɓ��������Ƃ�
            {
                if (hitobj.collider.gameObject.layer == 7)//UI�̃{�^���ɓ��������Ƃ�
                {
                    ButtonName = hitobj.collider.gameObject.name;
                    bullet_countL = 1;
                    bullet_countR = 1;
                    Debug.Log("UI" + hitobj.collider.gameObject.name + "���m�F�B�e����1�ɂ��܂��B");
                }
                //CHANGED:bool�ł͂Ȃ�stateChanger��currentState���Q�Ƃ���悤�ɕύX
                if (stateChanger.currentState==IStateChanger.GameState.Game)//�v���C���[�h�������Ƃ�
                {
                    if (hitobj.collider.gameObject.layer == 6 && bullet_countR == 1)//�E�e�œI�𓖂Ă���
                    {
                        var TargetColor = hitobj.collider.gameObject.GetComponentInParent<TargetInformation>().color;//�^�[�Q�b�g�̐F
                        if (TargetColor == 0)
                        {
                            if (InfiniteMode) return;
                            bullet_countR = 0;
                            ResetCombo();
                        }
                        //FIXME:�������}�g���������������������G���[���ł�@�܂��ȉ��̂悤�ȃR�[�h�Ŕ��ʂ���ƃC���^�[�t�F�[�X�𗘗p����Ӗ��������Ȃ邱�Ƃɒ���
                        hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(2);//���̏e�̐F������
                    }
                    else//�I�̐F����������e��0�ɂȂ�
                    {
                        if (InfiniteMode) return;
                        bullet_countR = 0;
                        ResetCombo();
                    }
                }
            }
            else //�E�����ɂ��G��Ă��Ȃ��ꍇ�@�e��0 & �R���{���Z�b�g
            {
                if (stateChanger.currentState == IStateChanger.GameState.Game)//�v���C���[�h�������Ƃ�
                {
                    if (InfiniteMode) return;
                    bullet_countR = 0;
                    ResetCombo();
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
   




