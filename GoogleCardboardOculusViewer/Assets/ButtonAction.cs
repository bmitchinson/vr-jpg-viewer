using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    public GameObject objDeleteRef;
    public Material leftMatRef;
    public Material rightMatRef;

    public void DestroyObj()
    {
        Destroy(objDeleteRef);
    }

    public void DownloadImage()
    {
        Debug.Log("Attempting new download");
        string textureURL = "https://firebasestorage.googleapis.com/v0/b/cardboardcameraoculusviewer.appspot.com/o/dock.vr.jpg?alt=media";
        StartCoroutine(DownloadImage(textureURL));
    }

    IEnumerator DownloadImage(string URL)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(URL))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                Debug.Log("Got texture");
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);

                Debug.Log("Setting");
                leftMatRef.mainTexture = texture;
                rightMatRef.mainTexture = texture;
            }
        }
    }
}
