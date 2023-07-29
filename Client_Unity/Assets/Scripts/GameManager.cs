using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    ClientApi Client;
    Player player;
    [SerializeField]
    TMP_InputField InputField_New;
    [SerializeField]
    TMP_InputField InputField_Old;
    [SerializeField]
    TextMeshProUGUI User_Name;
    [SerializeField]
    TextMeshProUGUI Player_Score;
    [SerializeField]
    GameObject InPut;
    [SerializeField]
    TextMeshProUGUI HighScore;
    [SerializeField]
    PlayerMoment ourPlayer;
    TMP_InputField input;
    string message;
    ResultData resultData;
    public void Start()
    {       
        player = new Player();
      
        StartCoroutine(Client.Get(Tags.getUrl));
        Invoke(nameof(Highscore), 0.3f);
    }
    private void Update()
    {
        if (ourPlayer.gameObject.activeSelf)
        {
            player.score = (int)ourPlayer.MinScore;
            Player_Score.text = player.score.ToString();
        }   
       
    }

    void Highscore()
    {
        HighScore.text = "High Score" + '\n';
        resultData = new ResultData();
        resultData = Client._resultData;
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

        }
        else
        {

            Debug.Log("Failed to parse the result data.");
        }
        resultData= null;
    }
    public void HideHighScore()
    {
        InPut.SetActive(true);
        HighScore.gameObject.SetActive(false);
        
    }
  
    public void GetData()
    {
        if(InputField_Old.text.Length > 0)
        {
            string url = Tags.getUrl + "/" + InputField_Old.text;
            CreatePlayer(InputField_Old.text, 0);
            ourPlayer.CreatePlayer(player);
            StartCoroutine(Client.Get(url));
            input = InputField_Old;
            message = "No such username exsists";
            Invoke("Check", 0.3f);
        }
    }

    public void PostData()
    {
        if (InputField_New.text.Length > 0)
        {
            CreatePlayer(InputField_New.text, 0);
            ourPlayer.CreatePlayer(player);
            StartCoroutine(Client.Post(Tags.postUrl, player));
            input = InputField_New;
            message = "This Username already exsists";
            Invoke("Check", 0.3f);
        }
    }

    void Check()
    {
        resultData=new ResultData();
        resultData = Client._resultData;
        bool okget = (resultData != null);
        if (okget||Client.I_okpost)
        {
            User_Name.text = player.user_name;
            InputField_New.gameObject.SetActive(false);
            ourPlayer.gameObject.SetActive(true);
            InputField_Old.gameObject.SetActive(false);
        }
        else
        {
            input.text = "";
            input.transform.Find("Text Area").transform.Find("Placeholder").GetComponent<TMP_Text>().text = message;
        }
        resultData = null;
    }

    void CreatePlayer(string name,int score)
    {
        player = new Player()
        {
            user_name = name,
            score = score
        };
    }
}
