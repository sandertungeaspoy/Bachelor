using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool QuizCleared;
    public Animator animatorButton;

    public Text UIscore;

    public DoorButton button;
    private Scene scene;
    public bool played = false;

    public bool fromHub = false;

    public float score = 3000;

    public bool gameWon = false;
    private bool updatescore = true;
    public TextMesh scoreText;
    private string bestScore;


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
        DontDestroyOnLoad(UIscore);
        

        
}


    // Start is called before the first frame update
    void Start()
    {
        bestScore = getBestsCore();
        Debug.Log(bestScore + " maaa");
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
            //print("from hub");
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
            //CreateText();
            //displayLatest();
            gameWon = false;
            updatescore = false;
        }

        updateScore();
        if (fromHub)
        {
            UIscore.text = "";
        }
        else
        {
            UIscore.text = "Score: " + score.ToString("0") + "\nCurrent best score: " + bestScore;
        }
        
    }

    IEnumerator Delete()
    {

        yield return new WaitForSeconds(4f);
        button.enabled = true;
        Destroy(gameObject);
    }


    public void updateScore()
    {
        if(score > 0 && !gameWon && updatescore)
        {
            score -= Time.deltaTime;
        }
        
    }

    public string getBestsCore()
    {
        string path = Application.dataPath + "/scores.txt";
        string content = File.ReadAllText(path);

        string[] lines = content.Split(
         new[] { "\r\n", "\r", "\n" },
         System.StringSplitOptions.None
);
        for (int i = 0; i < lines.Length && i < 5; i++)
        {
  
        }

        List<float> numbers = new List<float>();
        List<scoreName> nameAndscores = new List<scoreName>();
        for (int i = 0; i < lines.Length - 1; i++)
        {


            string[] test = lines[i].Split(',');

            scoreName nameAndscore = new scoreName(test[1], float.Parse(test[0]));

            nameAndscores.Add(nameAndscore);
        }
        List<scoreName> sorted = nameAndscores.OrderBy(A => A.score).ToList();
        string test1 = ((sorted[sorted.Count - 1].score).ToString() + " by" + sorted[sorted.Count - 1].name);
        Debug.Log(test1);
        return test1; 
    }

    public void CreateText(string name)
    {
        Debug.Log("Adding score");
        string path = Application.dataPath + "/scores.txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, score.ToString());
        }
        string content = score.ToString("0") + ", " + name  + "\n";
        File.AppendAllText(path, content);
    }

    public void displayLatest()
    {
        string path = Application.dataPath + "/scores.txt";
        string content = File.ReadAllText(path);

        string[] lines = content.Split(
         new[] { "\r\n", "\r", "\n" },
         System.StringSplitOptions.None
);
        for(int i = 0; i < lines.Length && i < 5; i++)
        {
            Debug.Log(lines[i]);
        }

        List<float> numbers = new List<float>();
        List<scoreName> nameAndscores = new List<scoreName>();
        for (int i = 0; i < lines.Length -1 ; i++)
        {
            Debug.Log(lines[i]);

            string [] test = lines[i].Split(',');

            scoreName nameAndscore = new scoreName(test[1], float.Parse(test[0]));

            nameAndscores.Add(nameAndscore);
        }

        List<scoreName> sorted = nameAndscores.OrderBy(A => A.score).ToList();

        Debug.Log("Sortert:" + sorted.Count);

        scoreText.text += "\nHighscores:\n\n";
        int j = 1;
        for(int i = sorted.Count-1; i >= 0; i--)
        {
                if(j < 9)
            {
                scoreText.text += j++ + ": " + sorted[i].name + " - Score: " + sorted[i].score + "\n";
                Debug.Log(sorted[i].name + " Score: " + sorted[i].score);
            }          
        }
    }

    public class scoreName
    {
        public string name;
        public float score;

        public scoreName(string name, float score)
        {
            this.name = name;
            this.score = score;
        }



    }

}