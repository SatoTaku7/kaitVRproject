using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float time;
    private MeshRenderer _mat;
    // Start is called before the first frame update
    void Start()
    {
        _mat = GetComponent<MeshRenderer>();
        StartCoroutine(DestroyObj());
    }

    private IEnumerator DestroyObj()
    {
        for (int i = 0; i < 100; i++)
        {
            _mat.material.color -= new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(time / 100);
        }
        Destroy(gameObject);
        yield break;
    }
}
