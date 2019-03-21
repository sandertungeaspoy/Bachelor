using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCannon : MonoBehaviour
{
    public Transform Cannon;
    public float rotationSpeed = 500f;
    public GameObject bullet;
    public GameObject obstacleObject;
    public GameObject obstacleObject2;
    public GameObject obstacleObject3;
    public TextMesh ScoreText;
    public Vector3 ahead = new Vector3(0, 0, 3);

    private float UpwardForce = 0f;
    public float shootingforce = 5f;
    public GameObject target;
    public bool hittarget = false;
    private double yPositionBullet;
    public Text UI;

   

    public VRTK_BaseControllable controllable;
    public VRTK_BaseControllable wheel;

    private float newValue;
    public VRTK_BaseControllable cannon;
    private float CurrentLevel = 0; // 5 Levels in total
    //private bool limitedAmmo = false;
    //private float currentAmmo;
    private bool levelCleared = false;
    private bool AllLevelsCleared = false;
    //private bool obstacle = false;
    private float numberOfLevels = 5f;
    //private float numberOfTargets;
    //private float targetsLeft;
    private bool LevelLost = false;
    private bool LevelRulesSet = false;
    private double angle;
    private float distanceToTarget;
    private float test = 30;
    private List<levelClass> levels = new List<levelClass>();

    // Start is called before the first frame update
    void Start()
    {

        
        LevelRules();
        wheel.SetValue(0);
        cannon.SetValue(0);
        CreateNewTarget();
        
    }

    // Update is called once per frame
    void Update()
    {
        
   
        cannon.SetValue(wheel.GetValue()); // Makes cannon rotate with the wheel

        angle = (90 - (wheel.GetNormalizedValue() * 110));

        if (CurrentLevel == 3)
        {
            obstacleObject2.transform.GetChild(2).Rotate(Vector3.forward, test * Time.deltaTime);
        }
        
        if (Input.GetKeyUp("space"))
        {
            if (!AmmoLeft())
            {
                RestartCurrentLevel();
                return;
            }

            if (levels[(int)CurrentLevel].getLimitedAmmo())
            {
                levels[(int)CurrentLevel].setCurrentAmmo(levels[(int)CurrentLevel].getCurrentAmmo() - 1);
                
            }

            ahead.y = (float)yPositionBullet;
            GameObject Bullet1 = Instantiate(bullet);
            //Bullet1.transform.parent = Cannon.transform;
            bullet test = Bullet1.GetComponent<bullet>();

            test.transform.position = Cannon.transform.position;

            Debug.Log(controllable.GetValue());
            //test.shootingForce = 10 * controllable.GetValue();
            //test.shootingForce = controllable.GetValue() * 10;
            test.shootingForce = GetForce();
            //test.shootingForce = 1800;
            //double Radian = (-wheel.GetValue() * Mathf.PI) / 180;
            //test.upwardForce = (float)((controllable.GetValue() * 10) *( System.Math.Tan(Radian)));
            //UpwardForce = (float)(test.shootingForce * -System.Math.Tanh(wheel.GetValue()));
            //test.upwardForce = UpwardForce;
            //Vector3 test3 = new Vector3(0, test.upwardForce, test.shootingForce);
            Vector3 forward = new Vector3(0, 0, test.shootingForce);
            //-wheel.GetValue()
            Vector3 dir = Quaternion.AngleAxis(-(90 - (wheel.GetNormalizedValue() * 110)), Vector3.right) * Vector3.forward;

            //test.shootingDirection = test3.normalized;
            test.shootingDirection = dir;
            
            
        }

        if (!AllLevelsCleared)
        {
            ScoreText.text = "LVL:" + CurrentLevel + " Ammo" + levels[(int)CurrentLevel].getCurrentAmmo() + "Targets " + levels[(int)CurrentLevel].gettargetsLeft() /*+ AllLevelsCleared*/  ; 
        }
        UI.text = "Angle: " + angle.ToString("0") + "\nForce " + GetForce().ToString("0") + "\nDistance to Target: " + distanceToTarget.ToString("0") + "\nAmmo: " + levels[(int)CurrentLevel].getCurrentAmmo();
        
    }

    public void HitEvent()
    {
        
       // CheckLevel();
        
        LevelCleared();

        if (!levelCleared && !AllLevelsCleared && AmmoLeft())
        {
            Debug.Log("Hei");
            //targetsLeft--;
            levels[(int)CurrentLevel].setTargetsLeft(levels[(int)CurrentLevel].gettargetsLeft() -1);

            
            CreateNewTarget();
        }
    }

    public void RestartCurrentLevel()
    {
        LevelRulesSet = false;
        //LevelRules();
        RestartLvl();
    }

    public bool AmmoLeft()
    {
        if(levels[(int)CurrentLevel].getLimitedAmmo() == true)
        {
            if(levels[(int)CurrentLevel].getCurrentAmmo() == 0)
            {
                LevelLost = true;
                return false;
            }
            return true;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Check if there is any targets left. Starts CheckLevel if there is none left
    /// </summary>
    public bool LevelCleared()
    {
        if(levels[(int)CurrentLevel].gettargetsLeft() == 0)
        {
            Debug.Log("No er targets 0");
            levelCleared = true;
            CheckLevel();
            return true;
        }
        return false;
        
    }

    public float GetForce()
    {
        if(controllable.GetValue() * 10 >= 500)
        {
           // return controllable.GetValue() * 10;
        }
        return controllable.GetValue() * 10 + 500;

        //return 500;
    }
    
    /// <summary>
    /// If the player loses, the player starts the current round again
    /// </summary>
    public void RestartLvl()
    {
        levels[(int)CurrentLevel].setTargetsLeft(levels[(int)CurrentLevel].getnumberOfTargets());
        levels[(int)CurrentLevel].setCurrentAmmo(levels[(int)CurrentLevel].getStartAmmo());
    }

    /// <summary>
    /// Starts and setups a new level
    /// </summary>
    public void LevelRules()
    {
        Debug.Log("Setter nye regler");
        levelClass newLevel;
        
        
        if (LevelRulesSet)
        {
            return;
        }
        
        else if(CurrentLevel == 0 )
        {
            //limitedAmmo = false;
            //numberOfTargets = 1;
           // targetsLeft = 1;
            LevelRulesSet = true;

            newLevel = new levelClass(1, false,99 ,false);
            Debug.Log(1);
        }
        else if(CurrentLevel == 1 )
        {
            //limitedAmmo = true;
            //currentAmmo = 10;
            //numberOfTargets = 1;
            //targetsLeft = 1;
            LevelRulesSet = true;
            newLevel = new levelClass(1, true,10, false);
            Debug.Log(2);
        }
        else if (CurrentLevel == 2)
        {
            //numberOfTargets = 4;
            //currentAmmo = 9;
           // targetsLeft = 4;
            //limitedAmmo = true;
            //obstacle = true;
            LevelRulesSet = true;
            newLevel = new levelClass(1, true, 10, true);
            spawnObstacle1();
            Debug.Log(3);
        }
        else if (CurrentLevel == 3)
        {

            LevelRulesSet = true;
            newLevel = new levelClass(1, true, 10, true);
            spawnObstacle2();
            Debug.Log(4);
        }
        else
        {
            LevelRulesSet = true;
            newLevel = new levelClass(1, true, 8, true);
            destroyObstacle1();
            destroyObstacle2();
            spawnObstacle3();
            Debug.Log(5);
        }
        
        if(levels.Count == CurrentLevel)
        {
            levels.Add(newLevel);
        }    
        
        
        
        
    }
    /// <summary>
    /// Spawns a wall in the middle of the map
    /// </summary>
    public void spawnObstacle1()
    {
        obstacleObject =  Instantiate(obstacleObject);
    }
    public void spawnObstacle2()
    {
        obstacleObject2 =  Instantiate(obstacleObject2);
    }
    public void spawnObstacle3()
    {
        obstacleObject3 = Instantiate(obstacleObject3);
    }
    public void destroyObstacle1()
    {
        Destroy(obstacleObject.gameObject);
    }
    public void destroyObstacle2()
    {
        Destroy(obstacleObject2.gameObject);   
    }

    public void destroyObstacle3()
    {
        Destroy(obstacleObject3.gameObject);       
    }
    /*
    public void destroyAllRemains()
    {
        targetDestroyed[] test = FindObjectsOfType<targetDestroyed>();
        for(int i = 0; i < test.Length; i++)
        {
            test[i].DestroyRemains(test[i].gameObject);
        }
    }
    */

    /// <summary>
    /// Check if the levels is cleared and starts the next one
    /// </summary>
    public void CheckLevel()
    {
        if (levelCleared == true)
        {
            if(CurrentLevel+1 >= numberOfLevels)
            {
                AllLevelsCleared = true;
                GameOver();
            }
            Debug.Log("Level okes med 1" + CurrentLevel);
            CurrentLevel++;
            //destroyAllRemains();
            LevelRulesSet = false;
            levelCleared = false;
            LevelRules();
        }
    }

    public void GameOver()
    {
        ScoreText.text = "Congratulations! You beat all levels! \nHint: What is the Graviational acceleration on earth's surface?";
        StartCoroutine("ReturnToMain");
    }

    IEnumerator ReturnToMain()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(3);
    }

    /// <summary>
    /// Creates a new Target randomly placed
    /// </summary>
    public void CreateNewTarget()
    {
        GameObject newtarget = Instantiate(target);
        newtarget.tag = "Target";
        Vector3 newPos;
        if(CurrentLevel == 2 || CurrentLevel == 3)
        {
            newPos = new Vector3(Cannon.transform.position.x, Random.Range(0, 10), Random.Range(Cannon.transform.position.x + 20 , 40));
        }
        else if(CurrentLevel == 4)
        {
            newPos = new Vector3(Cannon.transform.position.x, Random.Range(5, 8), 38);
        }
        else
        {
            newPos = new Vector3(Cannon.transform.position.x, Random.Range(0, 10), Random.Range(Cannon.transform.position.x + 5, 30));
        }
        
        
        newtarget.transform.position = newPos;
        distanceToTarget = newtarget.transform.position.z - Cannon.transform.position.z;
        hittarget = false;

    }


    public void Shoot()
    {
        if (!AmmoLeft())
        {
            RestartCurrentLevel();
            return;
        }
        if (levels[(int)CurrentLevel].getLimitedAmmo())
        {
            levels[(int)CurrentLevel].setCurrentAmmo(levels[(int)CurrentLevel].getCurrentAmmo() - 1);
            
        }

        //currentAmmo--;
        ahead.y = (float)yPositionBullet;
        GameObject Bullet1 = Instantiate(bullet);
        //Bullet1.transform.parent = Cannon.transform;
        bullet test = Bullet1.GetComponent<bullet>();

        test.transform.position = Cannon.transform.position;

        Debug.Log(controllable.GetValue());

        //test.shootingForce = controllable.GetValue() * 10;
        test.shootingForce = GetForce();

        Vector3 dir = Quaternion.AngleAxis(-(90 - (wheel.GetNormalizedValue() * 110)), Vector3.right) * Vector3.forward;

        //test.shootingDirection = test3.normalized;
        test.shootingDirection = dir;

        print("Wheel " + -wheel.GetValue());
        print("Cannon " + -cannon.GetValue());
        print("Up " + test.upwardForce);
        print("Forward " + test.shootingForce);
        print("Controllable " + controllable.GetValue());
        //print("TAN: " + (-System.Math.Tan(Radian)));
        print("aa " + wheel.GetNormalizedValue());
        print(90 - (wheel.GetNormalizedValue() * 110));
    }

    

    class levelClass {

        private float numberOfTargets;
        private float targetsLeft;
        private bool limitedAmmo;
        private bool obstacle;
        private float currentAmmo;
        private float startAmmo;

        public levelClass()
        {

        }
      
        public levelClass(float numberOfTargets, bool limitedAmmo, float currentAmmo, bool obstacle)
        {
            this.numberOfTargets = numberOfTargets;
            this.targetsLeft = numberOfTargets;
            this.limitedAmmo = limitedAmmo;
            this.obstacle = obstacle;
            this.currentAmmo = currentAmmo;
            this.startAmmo = currentAmmo;

        }


        public float getStartAmmo()
        {
            return startAmmo;
        }
        public float getnumberOfTargets()
        {
            return numberOfTargets;
        }
        public float gettargetsLeft()
        {
            return targetsLeft;
        }
        public bool getLimitedAmmo()
        {
            return limitedAmmo;
        }
        public bool getObstacle()
        {
            return obstacle;
        }
        public float getCurrentAmmo()
        {
            return currentAmmo;
        }
        public void setCurrentAmmo(float value)
        {
            currentAmmo = value;
            Debug.Log("Minker ammo med 1");
        }

        public void setTargetsLeft( float value)
        {
            targetsLeft = value;
            Debug.Log("Minker targets med 1");

        }


        


    }


}
