using UnityEngine;
using System.Collections;
//using Pathfinding;

public class Enemy : MonoBehaviour {
    
    public int health, attack, speed;
    public uint steps;

    protected int tempSteps;
    public static uint slideSteps;

    private Vector3 nextMove;
    private static float slideStepsRatio;
    protected static Transform playerTransform;
    //protected Seeker seeker;
    TileMap myTiles;

    //public Path path;

    //protected int currentWaypoint = 0;

    void Start()
    {
//        playerTransform = GameObject.FindWithTag("Player").transform;
//        Debug.Log(playerTransform);
        Init();
        //tempSteps = steps;
    }

    protected void Init()
    {
        //seeker = GetComponent<Seeker>();
        myTiles = GameObject.FindWithTag("TileMap").GetComponent<TileMap>();
        nextMove = new Vector3();
        slideSteps = GameObject.FindWithTag("GameController").GetComponent<Movement>().slideSteps;
        playerTransform = GameObject.FindWithTag("Player").transform;
        //tempSteps = (int)Random.Range(0f,steps);
        slideStepsRatio = 1f / slideSteps;
        //seeker.pathCallback += OnPathComplete;
        //seeker.StartPath(transform.position, playerTransform.position);
        //GameObject.FindGameObjectWithTag("TileMap").GetComponent<TileMap>().AddToMap(this.gameObject);
    }
    
    /*public void OnDisable()
    {
        seeker.pathCallback -= OnPathComplete;
    }*/

    /*public virtual void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
        else
        {
            Debug.Log(p.error);
        }

    }*/

    protected virtual IEnumerator Slide(Vector3 move)
    {
        for (uint f = slideSteps; f > 0; f -= 1)
        {
            transform.Translate(move * slideStepsRatio);
            yield /**/ return null;//*/ new WaitForSeconds(0.01f);//*/
        }
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
        //EventTrigger\
    }

    public virtual int UpdateHealth()
    {
        if(health <= 0)
        {
            Movement.Move -= move;
            myTiles.Kill(transform.localPosition);
            return 1;
        }
        return 0;
    }

    public virtual void move()
    {
        Movement.Move -= move;
        //todo tile map with a* search algorithm for "chase"
        if (--tempSteps <= 0)
        {
            tempSteps = (int)steps;
            //nextMove = (path.vectorPath[++currentWaypoint] - transform.position);//.normalized;
            //nextMove.z = 0;


            nextMove *= 0;
            float playerX = playerTransform.position.x;
            float playerY = playerTransform.position.y;
            //Start Coroutine with Event Listener
            if (playerX > transform.position.x)
            {
                nextMove.x += speed;
            }
            else if (playerY > transform.position.y)
            {
                nextMove.y += speed;
            }
            else if (playerX < transform.position.x)
            {
                nextMove.x -= speed;
            }
            else if (playerY < transform.position.y)
            {
                nextMove.y -= speed;
            }
            else
            {
                return;
            }

            /*if (!myTiles.ValidPoint(playerTransform.transform.localPosition + nextMove))
                return;*/

            //Debug.Log("Position: " + transform.localPosition + " Next Move: " + nextMove);
            switch (myTiles.CheckAndMoveObj(transform.localPosition, transform.localPosition + nextMove))
            {
                case -1:
                    Initialize player = myTiles.ObjectAt(transform.localPosition + nextMove).GetComponentInChildren(typeof(Initialize)) as Initialize;
                    player.health -= attack;
                    player.UpdateHealth();
                    
                    break;
                case 0:
                    //--currentWaypoint;
                    tempSteps = 1;
                    break;
                case 1:
                    slideStepsRatio = 1.0f / slideSteps;
                    StartCoroutine(Slide(nextMove));
                    break;
                default:
                    break;
            }
        }
        return;
    }

    public virtual void Attack()
    {

    }


}
