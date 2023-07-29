using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMoment : MonoBehaviour
{
    float score;
    public float MinScore { get { return score; } set { score = value; } }
    private float horizontalInput;
    private float verticalInput;

    [SerializeField] private ClientApi Client;
    Player player;

    private void Awake()
    {
        MinScore = 0;
    }
    void Start()
    {
        score = 0;

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * 5f;
        verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * 5f;

        float rawHorizontal = transform.position.x + horizontalInput;
        float rawVertical = transform.position.y + verticalInput;

        float clamphorizontal = Mathf.Clamp(rawHorizontal, -6.5f, 6.5f);
        float clampvertical = Mathf.Clamp(rawVertical, -4.5f, 4.5f);

        transform.localPosition = new Vector3(clamphorizontal, clampvertical, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            score++;
            MinScore = score;
        }

        else if(collision.gameObject.tag == "Bomb")
        {
            if (player != null)
            {
                StartCoroutine(Client.Put(Tags.putUrl, player));
            }
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }
        Destroy(collision.gameObject);
    }

    public void CreatePlayer(Player pl)
    {
        player = pl;
    }


}
