using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMap : MonoBehaviour {

    public int width;
    public int height;

    int size;

    public int getSize()
    {
        return size;
    }

    public GameObject[] tiles;
    private GameObject map;

    public void Restart()
    {
        size = (++width * 10 - 1) * (++height * 10 - 1);
        tiles = new GameObject[size];
        map = GameObject.FindWithTag("Plane");
        map.GetComponent<PlaneInitialize>().ReSize(width, height);

        GetComponent<MazeGenerator>().Init();
    }

    void Start() { }

    // Use this for initialization
    void Awake () {
        size = (width * 10 - 1) * (height * 10 - 1);
        tiles = new GameObject[size];
        map = GameObject.FindWithTag("Plane");
        map.GetComponent<PlaneInitialize>().ReSize(width, height);	
        //AddToMap(GameObject.FindGameObjectWithTag("Player"));

        GetComponent<MazeGenerator>().Init();
	}

    public void Kill(Vector3 objLoc)
    {
        Kill(convertPosition(objLoc));
    }
    public void Kill(int objLoc)
    {
        Destroy(tiles[objLoc]);
        tiles[objLoc] = null;
    }


    int convertPosition(Vector3 curPos)
    {
        return ((int)curPos.x + (width * 10 - 1)/2) + ((int)curPos.y + (height * 10 - 1)/2)* (height * 10 - 1);
    }

    int convertPosition(int x, int y)
    {
        return (x + (width * 10 - 1) / 2) + (y + (height * 10 - 1) / 2) * (height * 10 - 1);
    }

    public bool PointIsEmpty(int x, int y)
    {
        return tiles[convertPosition(x, y)] == null ? true : false;
    }

    public bool PointIsEmpty(Vector3 pos)
    {
        return tiles[convertPosition(pos)] == null ? true : false;
    }

    public GameObject ObjectAt(int x, int y)
    {
        return tiles[convertPosition(x, y)];
    }

    public GameObject ObjectAt(Vector3 pos)
    {
        return tiles[convertPosition(pos)];
    }

    public bool ValidPoint(int x, int y)
    {
        if (Mathf.Abs(x) >= (width * 10 ) / 2 || Mathf.Abs(y) >= (height * 10 ) / 2)
            return false;
        int pos = convertPosition(x,y);
        return pos >= 0 ? (pos < size ? /*(tiles[pos] == null ?*/ true : /*false) :*/ false) : false;
    }

    public bool ValidPoint(Vector3 loc)
    {
        return ValidPoint((int)loc.x, (int)loc.y);
    }

    public int CheckAndMoveObj(Vector3 loc1, Vector3 loc2)
    {
        return CheckAndMoveObj((int)loc1.x, (int)loc1.y, (int)loc2.x, (int)loc2.y);
    }

    public int CheckAndMoveObj(int x1, int y1, int x2, int y2) //Return -1 for attack, 0 for no move, 1 for move
    {
        if (!PointIsEmpty(x1,y1))
        {

            if (!PointIsEmpty(x2, y2))
            {
                GameObject obj1 = tiles[convertPosition(x1, y1)];
                GameObject obj2 = tiles[convertPosition(x2, y2)];
                if (obj1.tag == "Enemy")
                {
                    if(obj2.tag == "Enemy")
                    {
                        return 0;
                    }
                    if(obj2.tag == "Player")
                    {
                        return -1;
                    }
                }
                if (obj1.tag == "Player")
                {
                    if(obj2.tag == "Enemy")
                    {
                        return -1;
                    }
                }
/*                Enemy ene1 = tiles[convertPosition(x1, y1)].GetComponentInChildren(typeof(Enemy)) as Enemy;
                if(ene1 != null)
                {
                    Enemy ene2 = tiles[convertPosition(x2, y2)].GetComponentInChildren(typeof(Enemy)) as Enemy;
                    if (ene2 != null)
                        return 0;
                    Initialize play2 = tiles[convertPosition(x2, y2)].GetComponentInChildren(typeof(Initialize)) as Initialize;
                    if (play2 != null)
                        return -1;
                }
                Initialize play1 = tiles[convertPosition(x1, y1)].GetComponentInChildren(typeof(Initialize)) as Initialize;
                if(play1 != null)
                {
                    Enemy ene2 = tiles[convertPosition(x2, y2)].GetComponentInChildren(typeof(Enemy)) as Enemy;
                    if (ene2 != null)
                        return -1;
                    Initialize play2 = tiles[convertPosition(x2, y2)].GetComponentInChildren(typeof(Initialize)) as Initialize;
                    if (play2 != null)
                        return 0;
                }*/
            }
            if (PointIsEmpty(x2, y2))
            {
                tiles[convertPosition(x2, y2)] = tiles[convertPosition(x1, y1)];
                tiles[convertPosition(x1, y1)] = null;
                return 1;
            }
        }
        return 0;
    }

    public bool AddToMap(GameObject obj)
    {
        Vector3 pos = obj.transform.position;
        if(ValidPoint(pos))
        {
            /*Debug.Log(obj.name);
            Debug.Log(obj.transform.localPosition);
            Debug.Log(pos);*/
            if(PointIsEmpty(pos))
            {
                tiles[convertPosition(pos)] = obj;
                return true;
            }
        }
        return false;
    }

    public void spawnTiles() //??? forgot what this is for
    {
    }

    //create boundaries (player spawns at 0,0, plane is centered at 0,0)

    // Update is called once per frame
    void Update () {
	
	}
}

