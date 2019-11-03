using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    public GameObject objRef;

    public void DestroyObj()
    {
        Destroy(objRef);
    }
}
