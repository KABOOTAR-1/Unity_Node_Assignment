using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public ClientApi Client;
    public Player player;
    public TMP_InputField InputField_New;
    public TMP_InputField InputField_Old;
    public TextMeshProUGUI User_Name;
    public TextMeshProUGUI Player_Score;
    [SerializeField]
    GameObject InPut;
    [SerializeField]
    GameObject highscore;
    public PlayerMoment ourPlayer;
    TMP_InputField input;
    string message;
    public void Start()
    {       
        player = new Player();
        StartCoroutine(Client.Get(Tags.getUrl)); 
    }
    private void Update()
    {
        if (ourPlayer.gameObject.activeSelf)
        {
            player.score = (int)ourPlayer.MinScore;
            Player_Score.text = player.score.ToString();
        }   
       
    }

    public void HideHighScore()
    {
        InPut.SetActive(true);
        highscore.SetActive(false);
        
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
        if (Client.I_okget||Client.I_okpost)
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
