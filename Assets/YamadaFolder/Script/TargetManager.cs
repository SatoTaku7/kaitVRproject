using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour,ITargetManager
{
    public int breakTargetCount { get; private set; }

    [SerializeField] GameObject TartgetRed;
    [SerializeField] GameObject TartgetBlue;
    [SerializeField] GameObject TartgetGray;

    public int d = 60;//��������
    public int n = 40;//�����ʒu�̃o�����@�傫���قǒ������

    public int targetSize = 10;//�I�̃T�C�Y
    public float minusSize = 1f;

    public int comboNum = 0;//�R���{��
    private bool comboPlus = false;

    public int level;//�f�o�b�O�p�̃��x��
    public int breakNum = 0;//�f�o�b�O�p�̔j��I��

    private int colorLevel;//�J���[�p�̃��x��->�D�F���󂳂Ȃ��܂܎��̃��x���ɍs�������p
    //���x�����ς���Ă���ŏ��̔j�󎞂ɌĂԗp
    [SerializeField] bool firstBreak1 = true;
    [SerializeField] bool firstBreak2 = true;
    [SerializeField] bool firstBreak3 = true;
    [SerializeField] bool firstBreak4 = true;

    //�|�b�v�A�b�v����X�R�A�e�L�X�g
    [SerializeField] GameObject PopUpScoreText;

    //�|�b�v�A�b�v����X�R�A�e�L�X�g
    [SerializeField] GameObject[] BreakEffectObj = new GameObject[3];

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GenerateTartget(i, 2, targetSize);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var g = GameObject.FindWithTag("Target").GetComponentsInChildren<TargetBreak>();
            int ran = Random.Range(0, g.Length);
            int ranGun = Random.Range(0, 2);
            g[ran].BreakTarget(ranGun);
        }
        level = 1 + breakNum / 10;
    }

    public void HitTarget(int num, int color, float size,int gunColor, Vector3 pos)
    {
        breakNum++;
        GenerateTartget(num, color, size);
        CalculateScore(color, gunColor, pos);
        BreakEffect(pos);
    }

    //���������I�Əe�̏�񂩂�X�R�A�̎Z�o
    private void CalculateScore(int color, int gunColor, Vector3 pos)
    {
        //�R���{����p
        if (color != 2)
        {
            //���F�Ȃ�
            if (color == 0 && gunColor == 1)
            {
                Debug.Log("�R���{�𑝂₵�܂�");
                comboPlus = true;
                comboNum++;
            }
            if (color == 1 && gunColor == 2)
            {
                Debug.Log("�R���{�𑝂₵�܂�");
                comboPlus = true;
                comboNum++;
            }

            //�F���Ⴄ�ꍇ
            if (color == 0 && gunColor == 1)
            {
                Debug.Log("�R���{�������Z�b�g");
                comboPlus = false;
                comboNum = 0;
            }
            if (color == 1 && gunColor == 0)
            {
                Debug.Log("�R���{�������Z�b�g");
                comboPlus = false;
                comboNum = 0;
            }
        }

        //���݂̃R���{�����擾
        int conbo = 0;

        //�X�R�A�̎Z�o
        int score = 100 + (conbo * 30);

        //�X�R�A���|�b�v�A�b�v
        PopUpScore(color, score, pos);
    }

    //�X�R�A���|�b�v�A�b�v������
    private void PopUpScore(int color,int score,Vector3 pos)
    {
        GameObject ins;
        PopUpTextController info;
        ins = Instantiate(PopUpScoreText, pos, new Quaternion(0, 0, 0, 0), this.gameObject.transform);
        info = ins.GetComponent<PopUpTextController>();
        info.Init(score, color);
    }

    //�I����ꂽ�ۂ̃G�t�F�N�g
    private void BreakEffect(Vector3 pos)
    {
        //�R���{�����擾
        int combo = 0;

        //�R���{���ɉ��������o
        if (combo > 4)
        {
            Instantiate(BreakEffectObj[1], pos, new Quaternion(0, 0, 0, 0), this.gameObject.transform);
        }
        else if (combo > 9)
        {
            Instantiate(BreakEffectObj[2], pos, new Quaternion(0, 0, 0, 0), this.gameObject.transform);
        }
        else
        {
            Instantiate(BreakEffectObj[0], pos, new Quaternion(0, 0, 0, 0), this.gameObject.transform);
        }
    }

    //�V���ȓI�̐���
    public void GenerateTartget(int num, int color, float size)
    {
        //���W�̌��� ���󐶐��̏ꍇ�A�����Ɋ���Ă��܂�
        /*
        var theta = Random.Range(-Mathf.PI, Mathf.PI);
        var p = Random.Range(0f, 1f);
        var phi = Mathf.Asin(Mathf.Pow(p, 1f / (n + 1f)));

        var x = Random.Range(-50f, 50f);
        var y = Random.Range(0f, 20f);

        var Transform = new Vector3(
                    Mathf.Cos(phi) * Mathf.Cos(theta) * d + x,
                    Mathf.Cos(phi) * Mathf.Sin(theta) * d + y,
                    Mathf.Sin(phi) * (d + num * 20));
        var Rotation = Quaternion.identity;
        */

        //����ł͂Ȃ����ʏ�ɐ�������p�^�[��
        var x = Random.Range(-100f, 100f);
        var y = Random.Range(-20f, 60f);

        var Transform = new Vector3(x, y, d + num * 20);
        var Rotation = Quaternion.identity;

        //���x���̎擾


        //���x���ɉ������T�C�Y�̕ύX
        size = targetSize + minusSize - Mathf.Min(level, 3) * minusSize;

        //���x���ɉ������F���x���̐ݒ�
        level = Mathf.Min(level, 5);
        if (level == 1 || level == 2)
            colorLevel = level;
        if (level == 3)
        {
            colorLevel = 2;
            if (!firstBreak1)
                colorLevel = level;
        }
        if (level == 4)
        {
            colorLevel = 2;
            if (!firstBreak1)
                colorLevel = 3;
            if (!firstBreak2)
                colorLevel = level;
        }
        if (level == 5)
        {
            colorLevel = 2;
            if (!firstBreak1)
                colorLevel = 3;
            if (!firstBreak2)
                colorLevel = 4;
            if (!firstBreak3)
                colorLevel = level;
        }

        //���x���ɉ������F�̐ݒ�
        switch (colorLevel)
        {
            case 1:
                color = 2;//�D�F
                break;
            case 2:
                if (color == 0)
                    color = 1;
                else if (color == 1)
                    color = 0;
                if (firstBreak1)
                {
                    color = Random.Range(0, 2);//����
                    firstBreak1 = false;
                    Debug.Log("�F�t���I��1�����₵�܂�");
                }
                break;
            case 3:
                var c = GetComponentsInChildren<TargetInformation>();
                if (firstBreak2 && color == 2)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (c[i].color == 0)
                        {
                            color = 1;
                        }
                        if (c[i].color == 1)
                        {
                            color = 0;
                        }
                    }
                    firstBreak2 = false;
                    Debug.Log("�F�t���I��1�����₵�܂�");
                }
                break;
            case 4:
                if (firstBreak3 && color == 2)
                {
                    color = Random.Range(0, 2);//����
                    firstBreak3 = false;
                    Debug.Log("�F�t���I��1�����₵�܂�");
                }
                break;
            case 5:
                c = GetComponentsInChildren<TargetInformation>();
                if (firstBreak4 && color == 2)
                {
                    int red = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (c[i].color == 0)
                        {
                            red++;
                        }
                    }
                    if (red == 1)
                    {
                        color = 0;
                    }
                    if (red == 2)
                    {
                        color = 1;
                    }
                    if (red != 1 && red != 2) return;
                    firstBreak4 = false;
                    Debug.Log("�F�t���I��1�����₵�܂�");
                }
                break;
            default:
                Debug.Log(level+ "->level�̐��������������ł�");
                break;
        }

        //�I�̐����A�ԍ��̐ݒ�
        GameObject ins;
        TargetInformation info;
        switch (color)
        {
            case 0:
                ins = Instantiate(TartgetRed, Transform, Rotation, this.gameObject.transform);
                ins.transform.localScale = new Vector3(size * 100f, size * 100f, size * 100f);
                info = ins.GetComponent<TargetInformation>();
                info.num = num;
                break;
            case 1:
                ins = Instantiate(TartgetBlue, Transform, Rotation, this.gameObject.transform);
                ins.transform.localScale = new Vector3(size * 100f, size * 100f, size * 100f);
                info = ins.GetComponent<TargetInformation>();
                info.num = num;
                break;
            case 2:
                ins = Instantiate(TartgetGray, Transform, Rotation, this.gameObject.transform);
                ins.transform.localScale = new Vector3(size * 100f, size * 100f, size * 100f);
                info = ins.GetComponent<TargetInformation>();
                info.num = num;
                break;
            default:
                Debug.Log("Target��color���߂��o���Ă܂���");
                break;
        }
    }
}
