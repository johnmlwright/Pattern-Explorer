using UnityEngine;
using System.Collections;

public class PlaneInitialize : MonoBehaviour {

	// Use this for initialization
	void Start () {
//        transform.position = new Vector3(0.5f, 0.5f, 0f);
        transform.rotation = Quaternion.Euler(270f, 0, 0);
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(transform.localScale.z * 5, transform.localScale.x * 5);

    }

    public void ReSize(int height, int width)
    {
        transform.localScale = new Vector3(width, 1, height);
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(height * 5, width * 5);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
