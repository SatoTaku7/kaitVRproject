using UnityEngine;

public class UI_Pointer : OVRCursor//OVRGazePointer�����A�p���͎���̃X�N���v�gOVRCursor
{
    [SerializeField] GameObject Pointer;
    // Start is called before the first frame update
    void Start()
    {
        //Pointer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SetCursorRay(Transform ray)
    {

    }
    public override void SetCursorStartDest(Vector3 start, Vector3 dest, Vector3 normal)
    {
        Pointer.SetActive(true);
        Pointer.transform.position = dest;
    }
}
