using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionTest : MonoBehaviour
{
    public GameObject floorRef;

    public void DestroyFloor()
    {
        Destroy(floorRef);
    }
}
