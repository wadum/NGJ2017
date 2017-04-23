using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireCanon : Photon.MonoBehaviour {

	public GameObject crosshair;
	public GameObject enemyPrefab;
	public RectTransform radarArea;

	public List<GameObject> enemies;

    [Header("Auto spawning")]
    public float TimeToBegin = 3f;
    public float TimeBetweenSpawns = 15f;
    public int MinEnemies = 3;

	private List<GameObject> killedEnemies;
    private bool _autospawning;

	public void Start() {
		enemies = new List<GameObject>();
		killedEnemies = new List<GameObject>();
	}

    public void StartAutoSpawn() {
        if (_autospawning)
            return;

        _autospawning = true;
        StartCoroutine(AutoSpawnBegin());
    }

    private IEnumerator AutoSpawnBegin() {
        if (TimeToBegin > 0)
            yield return new WaitForSeconds(TimeToBegin);

        SpawnEnemy();
        var time = Time.timeSinceLevelLoad;

        yield return null;

        while(true)
        {
            if (TimeBetweenSpawns < Time.timeSinceLevelLoad - time
                || enemies.Count < MinEnemies)
            {
                SpawnEnemy();
                time = Time.timeSinceLevelLoad;
            }

            yield return null;
        }
    }

    public void SpawnEnemy() {
		Debug.Log("Spawning enemy");
		GameObject newEnemy = Instantiate(enemyPrefab);
		newEnemy.GetComponent<RectTransform>().SetParent(radarArea);
		newEnemy.GetComponent<RectTransform>().SetSiblingIndex(0);
		newEnemy.transform.localPosition = new Vector3(Random.Range(-525+40, 525-40),Random.Range(-350+40, 350-40),0);
		newEnemy.transform.localScale = new Vector3(1,1,1);
		newEnemy.SetActive(true);
		enemies.Add(newEnemy);
	}

	public void PressFireButton() {
		transform.localRotation = Quaternion.Euler(new Vector3(180,0,0));
	}

	public void ReleaseFireButton() {
		transform.localRotation = Quaternion.identity;
	}

	public void Fire(){

		Debug.Log("Fire!");

		killedEnemies.Clear();

		enemies.ForEach(enemy => {
			if (Vector2.Distance(enemy.GetComponent<RectTransform>().localPosition, crosshair.GetComponent<RectTransform>().localPosition) < 100)
				killedEnemies.Add(enemy);
		});

		if(killedEnemies.Count > 0)
			photonView.RPC("FireClients", PhotonTargets.Others, true);

		killedEnemies.ForEach( enemy => { enemies.Remove(enemy); Destroy(enemy); });
	}

	[PunRPC]
	public void FireClients(bool hit) {
		
	}
}
