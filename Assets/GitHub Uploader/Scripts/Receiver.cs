using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Receiver : MonoBehaviour
{
    byte[] zipwww = null;

    IEnumerator DownloadFile(string objectUrl)
    {
        var uwr = new UnityWebRequest(objectUrl, UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(Application.persistentDataPath, "test.zip");
        uwr.downloadHandler = new DownloadHandlerFile(path);

        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(uwr.error);
        }
        else
        {
            Debug.Log("File successfully downloaded and saved to " + path);

            //zipwww = new byte[uwr.downloadHandler.data.Length]; Array.Copy(uwr.downloadHandler.data, 0, zipwww, 0, uwr.downloadHandler.data.Length);
            //lzip.getFileInfo(null, zipwww);

            //if (lzip.ninfo != null && lzip.ninfo.Count > 0)
            //{
            //    for (int i = 0; i < lzip.ninfo.Count; i++)
            //    {
            //        Debug.LogFormat("Entry no: {0}:", (i + 1), lzip.ninfo[i]);
            //    }
            //}

            lzip.decompress_File(path, Path.Combine(path, "testFolder", null, null, null));
            Debug.Log("decompressed!!!!!!!!!!!");
        }
    }

    void UnZipping()
    {
        var ob = lzip.entry2Buffer(null, "dir1/dir2/dir3/Unity_1.jpg", zipwww);

        //create an inMemory zip file.
        if (ob != null)
        {
            lzip.inMemory t = new lzip.inMemory();
           
            lzip.compress_Buf2Mem(t, 9, ob, "inmem/test.jpg", null, "1234");
            lzip.free_inmemory(t);

          
            lzip.inMemory t2 = new lzip.inMemory();
           
            lzip.inMemoryZipStart(t2);
            lzip.inMemoryZipAdd(t2, 9, ob, "test.jpg");
            lzip.inMemoryZipAdd(t2, 9, ob, "directory/test2.jpg");
            lzip.inMemoryZipClose(t2);
           
            lzip.free_inmemory(t2);
        }
    }

    public void DownloadProject(string objectUrl)
    {
        StartCoroutine(DownloadFile(objectUrl));
    }
}
