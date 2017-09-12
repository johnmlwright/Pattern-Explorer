using UnityEngine;
using System.Collections;

public class EnemyInitialize : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        float inX, inY;
        Random.seed = (int)Time.time;
        inY = Random.Range(-5, 5) + 0.5f;
        inX = Random.Range(-7, 7) + 0.5f;

        transform.position = new Vector3(inX, inY, 0.4f);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
