using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCanon : Photon.MonoBehaviour {

	public GameObject crosshair;
	public GameObject enemyPrefab;

	public List<GameObject> enemies;
	private List<GameObject> killedEnemies;

	public void Start() {
		enemies = new List<GameObject>();
		killedEnemies = new List<GameObject>();
	}

	public void SpawnEnemy() {
		Debug.Log("Spawning enemy");
		GameObject newEnemy = Instantiate(enemyPrefab);
		newEnemy.GetComponent<RectTransform>()
			.SetParent(this.transform.parent.gameObject.GetComponent<RectTransform>());
		newEnemy.GetComponent<RectTransform>().SetSiblingIndex(1);
		newEnemy.transform.localPosition = new Vector3(Random.Range(-900, 900),Random.Range(-450, 450),0);
		newEnemy.transform.localScale = new Vector3(1,1,1);
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
