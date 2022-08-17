using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Receiver : MonoBehaviour
{
    //private void Start()
    //{
    //    lzip.decompress_File(@"C:\Users\Skipper\Downloads\test.zip", @"C:\Users\Skipper\Downloads\testFolder");
    //}

    IEnumerator DownloadFile(string objectUrl)
    {
        var uwr = new UnityWebRequest(objectUrl, UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(Application.persistentDataPath, "test_project.zip");
        uwr.downloadHandler = new DownloadHandlerFile(path);

        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(uwr.error);
        }
        else
        {
            Debug.Log("File successfully downloaded and saved to " + path);
        }
    }

    public void DownloadProject(string objectUrl)
    {
        StartCoroutine(DownloadFile(objectUrl));
    }
}
