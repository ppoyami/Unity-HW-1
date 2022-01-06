using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameManager _instance;
    public SpawnManager spawnManager;

    bool inCombat = false;

    public static GameManager Instance {
      get {
        if (!_instance) {
          _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
          if (_instance == null) {
            Debug.Log("no GameManager instance");
          }
        }
        return _instance;
      }
    }
    private void Awake() {
      if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnRelocate() {
      this.ResetObjects();
      spawnManager.RandomSpawnMonster();
      inCombat = false;
    }

    void ResetObjects() {
      GameObject[] champions = GameObject.FindGameObjectsWithTag("Champion");
      GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

      for (int i = 0; i < champions.Length; i++) {
        champions[i].GetComponent<Champion>().Die();
      }
      for (int i = 0; i < monsters.Length; i++) {
        monsters[i].GetComponent<Monster>().Die();
      }
    }

    public void OnStartCombat() {
      // 리셋 -> 챔피언 배치 -> 전투 시작
      if (!inCombat) {
        spawnManager.SpawnChampion();
      }

      inCombat = true;
    }
}
