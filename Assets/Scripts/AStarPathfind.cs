using UnityEngine;
using System;
using System.Collections.Generic;

public class AStarPathfind : MonoBehaviour {

    private TileMap tilemap;
    private List<Vector3> list;

	// Use this for initialization
	void Start () {
        tilemap = GameObject.FindWithTag("TileMap").GetComponent<TileMap>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void getMapSearch(Vector3 objLoc)
    {

    }

    List<Vector3> getPath(Vector3 start, Vector3 end)
    {
        int sx = (int)start.x;
        int sy = (int)start.y;
        List<Path> openList = new List<Path>();
        List<Path> closedList = new List<Path>();
        Path path = new Path();
        path.set((int)(Mathf.Abs(sx - end.x) + Mathf.Abs(sy - end.y)), 0);
        path.location = end;        
        openList.Add(path);
        do
        {
            //Find object closest to destination target.
            int lowestScore = int.MaxValue;
            foreach (Path p in openList)
            {
                if(p.destdist < lowestScore)
                {
                    lowestScore = p.destdist;
                    path = p;
                }
            }

            closedList.Add(path);
            openList.Remove(path);

            if(closedList.Contains(new Path(start)))
            {
                break;
            }

            78j

        } while (openList.Count > 0);
//        do
//        {
//            currentSquare = [openList squareWithLowestFScore]; // Get the square with the lowest F score


//    [closedList add: currentSquare]; // add the current square to the closed list

//    [openList remove:currentSquare]; // remove it to the open list
 
//	if ([closedList contains:destinationSquare]) { // if we added the destination to the closed list, we've found a path
//		// PATH FOUND
//		break; // break the loop
//	}
 
//	adjacentSquares = [currentSquare walkableAdjacentSquares]; // Retrieve all its walkable adjacent squares
 
//	foreach (aSquare in adjacentSquares) {
 
//		if ([closedList contains:aSquare]) { // if this adjacent square is already in the closed list ignore it
//			continue; // Go to the next adjacent square
//		}
 
//		if (![openList contains:aSquare]) { // if its not in the open list
 
//			// compute its score, set the parent
//			[openList add:aSquare]; // and add it to the open list
 
//		} else { // if its already in the open list
 
//			// test if using the current G score make the aSquare F score lower, if yes update the parent because it means its a better path
 
//		}
//	}
 
//} while(![openList isEmpty]); // Continue until there is no more available square in the open list (which means there is no path)

        return list;
    }
}

public class Path : IEquatable<Path>
{
    public Path()
    {
    }

    public Path(Vector3 vec)
    {
        location = vec;
    }

    public Path(Path path)
    {
        this.destdist = path.destdist;
        this.startdist = path.startdist;
        this.location = path.location;
        sum = startdist + destdist;
    }
    
    public bool Equals(Path other)
    {
        if(this.location == other.location)
        {
            return true;
        }else
        {
            return false;
        }
    }

    public Vector3 location;
    //Destination Distance F, Distance from Start G, sum of the two numbers.
    public int destdist, startdist, sum;
    public void set(int dest, int start)
    {
        destdist = dest;
        startdist = start;
        sum = destdist + startdist;
    }
}