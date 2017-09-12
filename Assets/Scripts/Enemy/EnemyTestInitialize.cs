using UnityEngine;
using System.Collections;

public class EnemyTestInitialize : Enemy {


    // Use this for initialization
    void Awake() {
        TileMap tiles = GameObject.FindGameObjectWithTag("TileMap").GetComponent<TileMap>();
        int height = (tiles.width * 10 - 1)/2;
        int width = (tiles.height * 10 - 1)/2;
        int attempts = 0;
        do
        {
            transform.position = new Vector3(Random.Range(width * -1, width), Random.Range(height * -1, height), 0.4f);
            if (tiles.AddToMap(gameObject))
            {
                Init();
                goto succeed;    
            }
        } while (attempts++ < 10);

        Destroy(this);

        succeed:;

    }
	
	// Update is called once per frame
	void Update () {

	}
}
