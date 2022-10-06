using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextController : MonoBehaviour
{
    private TextMesh _mesh;

    private GameObject Player;

    public void Init(int score,int color)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(Player.transform.position);
        transform.Rotate(new Vector3(0, 180, 0));
        transform.position -= new Vector3(2.0f, 0, 0);
        _mesh = GetComponent<TextMesh>();
        StartCoroutine(Fadeout());
        _mesh.text = "+" + score.ToString();
        switch (color)
        {
            case 0:
                //ê‘
                _mesh.color = new Color(1, 0, 0, 1);
                break;
            case 1:
                //ê¬
                _mesh.color = new Color(0, 0, 1, 1);
                break;
            case 2:
                //äD
                _mesh.color = new Color(0.4f, 0.4f, 0.4f, 1);
                break;
            default:
                Debug.Log(color + "êFÇÃéwíËêîÇ™àŸÇ»ÇËÇ‹Ç∑");
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
