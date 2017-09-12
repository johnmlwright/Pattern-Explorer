using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Initialize : MonoBehaviour {

    public float speed;

    public int health, attack;

    TileMap tiles;

    public GameObject panel;


	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0f, 0f, 0.4f);
	}

    public void UpdateHealth()
    {
        if(health <= 0)
        {
            Debug.Log("Game Over");
            //Do Game Over script here.
            panel.SetActive(true);
        }

    }

    public void Init()
    {
        tiles = GameObject.FindWithTag("TileMap").GetComponent<TileMap>();
        int height = (tiles.height * 10 - 1) / 2;
        int width = (tiles.width * 10 - 1) / 2;
        do
        {
            transform.position = new Vector3(Random.Range(width * -1, width), Random.Range(height * -1, height), 0.4f);
            if (tiles.AddToMap(gameObject))
                break;
        } while (true);
        GameObject.FindWithTag("MainCamera").transform.position = transform.position + new Vector3(0,0,-10.4f);
    }
    

    // Update is called once per frame
    void Update () {
	
	}
}
