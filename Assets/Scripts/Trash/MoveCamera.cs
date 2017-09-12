using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

    private GameObject myCamera;
    private Vector3 nextMove;

	// Use this for initialization
	void Start () {
        myCamera = GameObject.FindWithTag("MainCamera");
        nextMove = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        nextMove *= 0;
        if(Input.GetKeyDown("w"))
            nextMove.y += 1;
        if (Input.GetKeyDown(KeyCode.S))
            nextMove.y -= 1;
        if (Input.GetKeyDown("a"))
            nextMove.x -= 1;
        if (Input.GetKeyDown("d"))
            nextMove.x += 1;
        
        myCamera.transform.Translate(nextMove);   
	}
}
