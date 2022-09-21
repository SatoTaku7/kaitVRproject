using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextController : MonoBehaviour
{
    private TextMesh _mesh;

    // Start is called before the first frame update
    private void Start()
    {
        _mesh = GetComponent<TextMesh>();
        StartCoroutine(Fadeout());
    }

    public void Init(int score,int color)
    {
        _mesh.text = "+" + score.ToString();
        switch (color)
        {
            case 0:
                //��
                _mesh.color = new Color(1, 0, 0, 1);
                break;
            case 1:
                //��
                _mesh.color = new Color(0, 0, 1, 1);
                break;
            case 2:
                //�D
                _mesh.color = new Color(0.4f, 0.4f, 0.4f, 1);
                break;
            default:
                Debug.Log(color + "�F�̎w�萔���قȂ�܂�");
                break;
        }
    }

    private IEnumerator Fadeout()
    {
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < 100; i++)
        {
            _mesh.color -= new Color(0, 0, 0, 0.01f);
            transform.localPosition += new Vector3(0, 0.05f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(this.gameObject);
    }
}
