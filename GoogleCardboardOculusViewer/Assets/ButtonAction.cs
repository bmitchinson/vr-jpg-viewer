using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using VrJpeg;
using XmpCore;
using MetadataExtractor;
using MetadataExtractor.Formats.Jpeg;
using MetadataExtractor.Formats.Xmp;
using MetadataExtractor.IO;
using ICSharpCode;

public class ButtonAction : MonoBehaviour
{
    public GameObject objDeleteRef;
    public Material leftMatRef;
    public Material rightMatRef;
    public AudioSource m_MyAudioSource;

    public void DestroyObj()
    {
        Destroy(objDeleteRef);
    }

    public void DownloadImage()
    {
        Debug.Log("Attempting new download");
        string leftURL = "https://firebasestorage.googleapis.com/v0/b/cardboardcameraoculusviewer.appspot.com/o/upload.vr.jpg?alt=media";
        string rightURL = "https://firebasestorage.googleapis.com/v0/b/cardboardcameraoculusviewer.appspot.com/o/core_right.jpg?alt=media";
        string audioURL = "https://firebasestorage.googleapis.com/v0/b/cardboardcameraoculusviewer.appspot.com/o/core_audio.mp3?alt=media";
        StartCoroutine(DownloadRightImage(rightURL));
        StartCoroutine(DownloadLeftImage(leftURL));
        StartCoroutine(DownloadAudio(audioURL));
    }

    IEnumerator DownloadRightImage(string URL)
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

                Debug.Log("Setting Right");
                rightMatRef.mainTexture = texture;
            }
        }
    }
    IEnumerator DownloadLeftImage(string URL)
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

                Debug.Log("Setting Left");
                leftMatRef.mainTexture = texture;
            }
        }
    }

    IEnumerator DownloadAudio(string url)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.Send();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Setting audio");
                m_MyAudioSource.clip = DownloadHandlerAudioClip.GetContent(www);
                m_MyAudioSource.loop = true;
                m_MyAudioSource.Play();
            }
        }
    }
}