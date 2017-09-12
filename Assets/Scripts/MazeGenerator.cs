using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wall;
    public int roomTries;
    public int roomExtraSize;
    public int roomSize;
    int height, width;
    TileMap tiles;

    public void Init()
    {
        Run();
    }

    void Run()
    {
        tiles = GetComponent<TileMap>();
        height = (tiles.height * 10) / 2;
        width = (tiles.width * 10) / 2;
//        roomSize = tiles.height;
//        roomExtraSize = tiles.height * 2;
//        roomTries = tiles.height * 20;
        fillMap();
        placeRooms();
        //var gridGraph = AstarPath.active.astarData.gridGraph;
        //gridGraph.width = width * 2 - 1;
        //gridGraph.depth = height * 2 - 1;
        //gridGraph.UpdateSizeFromWidthDepth();
        //AstarPath.active.Scan();
    }

    void placeRooms()
    {
        List<Room> rooms = new List<Room>();

        Vector2 prevCenter = new Vector2(0, 0);

        for (int i = 0; i < roomTries; i++)
        {
            int w = Random.Range(roomSize, roomSize + roomExtraSize);
            int h = Random.Range(roomSize, roomSize + roomExtraSize);
            int x = Random.Range(1, width * 2 - w);
            int y = Random.Range(1, height * 2 - h);

            Room newRoom = new Room();
            newRoom.Init(x, y, w, h);

            foreach (Room otherRoom in rooms)
            {
                if (newRoom.intersects(otherRoom))
                {
                    /**/ goto fail;//*/break;//*/
                }
            }

            if(i != 0)
            {
                cutCorridor(prevCenter, newRoom.center);
            }

            prevCenter = newRoom.center;

            //Debug.Log("" + w + " " + h + " "  + x+ " "  + y);
            createRoom(newRoom);
            rooms.Add(newRoom);
        fail:;
            
        }

    }

    void fillMap()
    {
       int size = tiles.getSize();
//        int xmod = tiles.width * 2;
//        int ymod = tiles.height * 2;
       for (int i = 0; i < size / height; i++)
        {
            for(int k = 0; k < size / width; k++)
            {
                GameObject newObj = (GameObject)Instantiate(wall, new Vector3(k - width , i - height, 0.4f), new Quaternion());
                if (!tiles.AddToMap(newObj))
                    Destroy(newObj);
            }
        }
    }

    void createRoom(Room room)
    {
        for(int i = room.bL; i < room.bR; i++)
        {
            for(int k = room.tL; k < room.tR; k++)
            {
                //Debug.Log("" + k + i + width);
                tiles.Kill(i * (width *2 - 1) + k); //Safe Destroy
            }
        }
    }

    void cutCorridor(Vector2 oldCenter, Vector2 newCenter)
    {
        vCorridor((int)oldCenter.x, (int)newCenter.x, (int)oldCenter.y);
        hCorridor((int)oldCenter.y, (int)newCenter.y, (int)newCenter.x);
    }


    void vCorridor(int x1, int x2, int y)
    {
        int min = Mathf.Min(x1, x2);
        int max = Mathf.Max(x1, x2);
        for(int i = min; i < max + 1; i++)
        {
            tiles.Kill(i * (width * 2 - 1) + y);
        }
    }

    void hCorridor(int y1, int y2, int x)
    {
        int min = Mathf.Min(y1, y2);
        int max = Mathf.Max(y1, y2);

        for(int i = min; i < max + 1; i++)
        {
            tiles.Kill(x * (width * 2 - 1) + i);
        }
    }

}

public class Room// : MonoBehaviour
{
    //Corners
    public int bL, bR, tL, tR;

    //Width Height of Room
    public int w, h;

    public Vector2 center;

    public void Init(int x, int y, int _w, int _h)
    {
        bL = x;
        bR = x + _w;
        tL = y;
        tR = y + _h;
        w = _w;
        h = _h;
        center = new Vector2(x + _w/2, _h/2 + y);
    }

    public bool intersects(Room room)
    {
        return (bL <= room.bR && bR >= room.bL && tL <= room.tR && tR >= room.tL);
    }

}