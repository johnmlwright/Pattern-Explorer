using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//#define NOSLIDE

public class Movement : MonoBehaviour {

//    private GameObject myCamera, myPlayer, myTiles;
    private Initialize myPlayer;
    private TileMap myTiles;
    private Transform myCamera;
    private Vector3 nextMove;
    float speed;
    public uint slideSteps;
    private float slideStepsRatio;
    bool moving;
    int enemyCount;
    
    public GameObject enemy1;

    public Text text;
    
    void Restart()
    {
        Cleanup();
        myTiles.Restart();
        myPlayer.Init();
        //myPlayer.health = 2;
        spawnEnemy((uint)Random.Range(1, Mathf.Sqrt(myTiles.getSize())));
        enemyCount = countEnemy();
        text.text = "Enemies: " + enemyCount;
    }

    void Cleanup()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
             myTiles.Kill(obj.transform.position);
            
        }
        foreach (var obj in GameObject.FindGameObjectsWithTag("Wall"))
        {
            myTiles.Kill(obj.transform.position);
            
        }
    }

    // Use this for initialization
    void Start()
    {
        enemyCount = 0;
        Random.seed = System.Environment.TickCount;
        slideStepsRatio = 1.0f / slideSteps;
        myPlayer = GameObject.FindWithTag("Player").GetComponent<Initialize>();
        myCamera = GameObject.FindWithTag("MainCamera").transform;
        myTiles = GameObject.FindWithTag("TileMap").GetComponent<TileMap>();
        nextMove = new Vector3(0, 0, 0);
        speed = myPlayer.GetComponent<Initialize>().speed;
        myTiles.spawnTiles();
        myPlayer.Init();
        spawnEnemy((uint)Random.Range(1,5));
        enemyCount = countEnemy();
        text.text = "Enemies: " + enemyCount;
        
    }

    public int countEnemy()
    {
        return enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void spawnEnemy(uint num)
    {
        GameObject newEnemy;
        for(uint i = num; --i > 0;)
        {
            newEnemy = (GameObject)Instantiate(enemy1, new Vector3(),new Quaternion() );
            if(myTiles.ObjectAt(newEnemy.transform.position) != newEnemy)
            {
                Destroy(newEnemy);
            }
        }
    }

    public delegate void PlayerMove();

    public static event PlayerMove Move;

    IEnumerator Slide(Vector3 move)
    {
        for (uint f = slideSteps; f > 0; f -= 1)
        {
            moving = true;
            myPlayer.transform.Translate(move*slideStepsRatio);
            myCamera.transform.Translate(move*slideStepsRatio);
            yield /**/ return null;//*/ new WaitForSeconds(0.01f);//*/
        }
        myPlayer.transform.position = new Vector3(Mathf.Round(myPlayer.transform.position.x), Mathf.Round(myPlayer.transform.position.y), myPlayer.transform.position.z);
        myCamera.position = new Vector3(myPlayer.transform.position.x, myPlayer.transform.position.y, myCamera.transform.position.z);
        moving = false;
        //EventTrigger
        if(enemyCount > 0)
            Move();
    }

    // Update is called once per frame
    void Update()
    {
        if (countEnemy() == 0)
        {
            Debug.Log("You Win!");
            Restart();
        }
        if(myPlayer.panel.activeSelf)
        {
            moving = true;
            if(Input.GetKeyDown("r"))
            {
                //myPlayer.panel.SetActive(false);

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//                Application.LoadLevel(Application.loadedLevel);
            }
        }
        nextMove *= 0;
        //Change !moving with movement event.
        if (Input.anyKeyDown && !moving)
        {
            Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);
            //Check valid inputs
            if (Input.GetKeyDown("w"))
            {
                nextMove.y += speed;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                nextMove.y -= speed;
            }
            else if (Input.GetKeyDown("a"))
            {
                nextMove.x -= speed;
            }
            else if (Input.GetKeyDown("d"))
            {
                nextMove.x += speed;
            }
            else if (Input.GetKeyDown("r"))
            {
                //myPlayer.panel.SetActive(false);

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //                Application.LoadLevel(Application.loadedLevel);
            }
            else
            {
                return; //to whence ye came (invalid input)
            }
            //"If" valid input
            /*            if (!myTiles.GetComponent<TileMap>().ValidPoint(myPlayer.transform.localPosition + nextMove)) //redundant for now
                            return;*/
            //GameUpdate Here
            switch(myTiles.CheckAndMoveObj(myPlayer.transform.localPosition, myPlayer.transform.localPosition + nextMove))
            {
                case -1:
                    foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        if(obj.GetComponent<Enemy>() != null)
                        {
                            Move += obj.GetComponent<Enemy>().move;
                        }
                    }
                    if (enemyCount != 0)
                        Move();
                    //doattackthing
                    Enemy ene = myTiles.ObjectAt(myPlayer.transform.localPosition + nextMove).GetComponentInChildren(typeof(Enemy)) as Enemy;
                    ene.health -= myPlayer.attack;
                    enemyCount -= ene.UpdateHealth();

                    text.text = "Enemies: " + enemyCount;
                    if(enemyCount == 1)
                    {
                        enemyCount = countEnemy();
                    }
                    break;
                case 1:
                    foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        if(obj.GetComponent<Enemy>() != null)
                            Move += obj.GetComponent<Enemy>().move;
                    }
#if NOSLIDE
            myPlayer.transform.Translate(nextMove);
            myCamera.transform.Translate(nextMove);*/
#else
                    slideStepsRatio = 1.0f / slideSteps;
                    StartCoroutine(Slide(nextMove));
#endif
                    break;
                case 0:
                    //nothing
                    break;
                default:
                    //nothing
                    break;
            }

        }
    }
}


