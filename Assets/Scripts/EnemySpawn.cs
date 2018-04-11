using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

	//Array of objects to spawn (note I've removed the private goods variable)
	public GameObject[] theEnemy;

	//Time it takes to spawn theGoodies
	[Space(3)]
	public float waitingForNextSpawn = 2;
	public float theCountdown = 2;

	// the range of X
	[Header ("X Spawn Range")]
	public float xMin;
	public float xMax;

	// the range of y
	[Header ("Y Spawn Range")]
	public float yMin;
	public float yMax;
 
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// timer to spawn the next goodie Object
         theCountdown -= Time.deltaTime;
         if(theCountdown <= 0)
         {
             SpawnEnemy ();
             theCountdown = waitingForNextSpawn;
         }
	}

	void SpawnEnemy() {
		
		Vector2 pos = new Vector2 (Random.Range (xMin, xMax), Random.Range (yMin, yMax));
		Debug.Log(pos);
		// Choose a new goods to spawn from the array (note I specifically call it a 'prefab' to avoid confusing myself!)
		GameObject enemyPrefab = theEnemy [Random.Range (0, theEnemy.Length)];
		// Creates the random object at the random 2D position.
		Instantiate (enemyPrefab, pos, transform.rotation);
		// If I wanted to get the result of instantiate and fiddle with it, I might do this instead:
        //GameObject newGoods = (GameObject)Instantiate(goodsPrefab, pos)
        //newgoods.something = somethingelse
	}
}
