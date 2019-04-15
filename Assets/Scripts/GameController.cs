using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool QuizCleared;
    public Animator animatorButton;
    public DoorButton button;
    private Scene scene;
    public bool played = false;

    public bool fromHub = false;

    public float score = 3000;

    public bool gameWon = false;
    public TextMesh scoreText;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        

        
}


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        scene = SceneManager.GetActiveScene();

        if(scene.name == "EscapeRoom")
        {
            button = GameObject.Find("Button").GetComponent<DoorButton>();
        }

        if (scene.name == "GameHub")
        {
            fromHub = true;
            print("from hub");
        }
        //print(fromHub);

        if (QuizCleared && !played)
        {
            button = GameObject.Find("Button").GetComponent<DoorButton>();
            //button.enabled = false;
            animatorButton = GameObject.Find("DungeonButton").GetComponent<Animator>();

            animatorButton.Play("PopUP");
            played = true;

            //StartCoroutine("Delete");


        }

        if (gameWon)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMesh>();
            scoreText.text = "Your Score: " + score.ToString("0");
        }

        updateScore();

    }

    IEnumerator Delete()
    {

        yield return new WaitForSeconds(4f);
        button.enabled = true;
        Destroy(gameObject);
    }


    public void updateScore()
    {
        if(score > 0 && !gameWon)
        {
            score -= Time.deltaTime;
            Debug.Log(score);
        }
        
    }

}