using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class OVRCursor : MonoBehaviour
{
     public abstract void SetCursorRay(Transform ray);
     public abstract void SetCursorStartDest(Vector3 start, Vector3 dest, Vector3 normal);
}
