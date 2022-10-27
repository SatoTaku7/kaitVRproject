using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AssistTarget : MonoBehaviour, IGunBreakTarget
{
    private Image _image;

    private AssistManager _manager;
    private bool Hit = false;

    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponentInChildren<Image>();
        _manager = GetComponentInParent<AssistManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(Player.transform.position);

        StartCoroutine(limitTime());
    }

    //é©ï™ÇÃêßå¿éûä‘ÇÃä«óù
    private IEnumerator limitTime()
    {
        _image.fillAmount = 1f;
        while (_image.fillAmount != 0)
        {
            _image.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
        yield break;
    }

    //Ç®èïÇØìIÇÃîjâÛ
    public void BreakTarget(int gunColor)
    {
        var random = new System.Random();
        var min = -5;
        var max = 5;
        gameObject.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => {
            var c = r.gameObject.GetComponent<MeshCollider>();
            c.isTrigger = true;
            r.isKinematic = false;
            r.AddForce(2 * Physics.gravity, ForceMode.Impulse);
            r.transform.SetParent(null);
            r.gameObject.AddComponent<AutoDestroy>().time = 0.2f;
            r.gameObject.layer = 6;
            var vect = new Vector3(random.Next(min, max), random.Next(0, max), random.Next(min, max));
            r.AddForce(vect, ForceMode.Impulse);
            r.AddTorque(vect, ForceMode.Impulse);
        });
        if (Hit == false)
        {
            Hit = true;
            Debug.Log("AssistTargetHit");
            _manager.HitTarget(transform.position);
        }
        Destroy(gameObject);
    }
}
