using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using System;
public class ClientApi : MonoBehaviour
{
    public TextMeshProUGUI HighScore;
    private bool ok_post = false;
    public bool I_okpost => ok_post;
    private bool ok_get = false;
    public bool I_okget => ok_get;
    private void Awake()
    {
        ok_post = false;
        ok_get = false;
    }

    public IEnumerator Get(string url)
    {
        using (UnityWebRequest ClientConnect = UnityWebRequest.Get(url))
        {
            yield return ClientConnect.SendWebRequest();

            if (ClientConnect.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(ClientConnect.error);
            }
            else
            {
                if (ClientConnect.isDone)
                {
                    //result data
                    var result = System.Text.Encoding.UTF8.GetString(ClientConnect.downloadHandler.data);
                    result = "{\"result\":" + result + "}";
                    ResultData resultData;
                        try
                        {
                            var re= JsonUtility.FromJson<ResultData>(result);
                            Debug.Log(re.result);
                            resultData = re;
                        }
                        catch(Exception err)
                        {
                            resultData=null;
                            Debug.Log("This username does not exsists");
                        };

                    HighScore.text = "High Score"+'\n';
                    if (resultData != null && resultData.result != null)
                    {
                        if (resultData.result.Length > 0)
                        {
                            foreach (Player item in resultData.result)
                            {
                                HighScore.text += item.user_name + ":" + item.score + "\n";
                            }
                        }
                        else
                        {
                            HighScore.text += "No Users";
                        }
                        if (!HighScore.gameObject.activeSelf)
                        {
                            ok_get = true;
                        }

                    }              
                    else
                    {
                       
                        Debug.Log("Failed to parse the result data.");
                    }

                    //Debug.Log(result);
                }
                else
                {
                    Debug.Log("Couldn't get the data");
                }
            }

            ClientConnect.Dispose();
        }
    }

    [System.Serializable]
    public class ResultData
    {
        public Player[] result;
    }

    public IEnumerator Post(string url, Player player)
    {
        var jsonData = JsonUtility.ToJson(player);
        Debug.Log(jsonData);

        using (UnityWebRequest ClientCreate = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            using (var uploadHandler = new UploadHandlerRaw(bodyRaw)) // Use 'using' to dispose of the UploadHandlerRaw
            {
                ClientCreate.uploadHandler = uploadHandler;
                ClientCreate.downloadHandler = new DownloadHandlerBuffer();
                ClientCreate.SetRequestHeader("Content-Type", "application/json");

                yield return ClientCreate.SendWebRequest();

                if (ClientCreate.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(ClientCreate.error);
                }
                else
                {
                    if (ClientCreate.isDone)
                    {
                        // handle the result
                        string result = System.Text.Encoding.UTF8.GetString(ClientCreate.downloadHandler.data);
                        Player res=new Player();
                        try
                        {
                            var re= JsonUtility.FromJson<Player>(result);
                            res = re;
                        }
                        catch(Exception err)
                        {
                            Debug.Log("This username already exsists");
                        };

                        Debug.Log(res.user_name);

                        if (res.user_name != null)
                        {
                            ok_post = true;
                        }
                    }
                    else
                    {
                        Debug.Log("Error! Data couldn't get.");
                    }
                }
                uploadHandler.Dispose();
            }
            ClientCreate.Dispose();
        }
    }

    public IEnumerator Put(string url, Player player)
    {
        var json = JsonUtility.ToJson(player);
        Debug.Log(json);

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        using (var uploadHandler = new UploadHandlerRaw(bodyRaw)) // Use 'using' to dispose of the UploadHandlerRaw
        {
            using (UnityWebRequest ClientUpdate = new UnityWebRequest(url, "PUT"))
            {
                ClientUpdate.uploadHandler = uploadHandler;
                ClientUpdate.downloadHandler = new DownloadHandlerBuffer();
                ClientUpdate.SetRequestHeader("Content-Type", "application/json");

                yield return ClientUpdate.SendWebRequest();

                if (ClientUpdate.result == UnityWebRequest.Result.ConnectionError || ClientUpdate.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error");
                }
                else if (ClientUpdate.isDone)
                {
                    string result = System.Text.Encoding.UTF8.GetString(ClientUpdate.downloadHandler.data);
                    Debug.Log("Request successful!");
                    Debug.Log(result);
                }
                else
                {
                    Debug.LogError("Error! Data couldn't be received.");
                }
                ClientUpdate.Dispose();
            }
            uploadHandler.Dispose();
        }
    }
}
