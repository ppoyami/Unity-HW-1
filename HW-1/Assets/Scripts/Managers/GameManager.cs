using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameManager _instance;

    public SpawnManager spawnManager;
    public Text GoldText;

    int gold = 0;
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

    private void Start() {
      EventManager.MonsterDieEvent += UpdateGoldText;
      this.UpdateGoldText();
    }

    public void OnDamageUp() {
      if (this.gold < 10) return;

      GameObject championObj = GameObject.FindGameObjectWithTag("Champion");
      Champion champion = championObj.GetComponent<Champion>();
      champion.damage += 1.1f;
      this.gold -= 10;
      this.UpdateGoldText();
      Debug.Log("upgrade" + champion.damage);
    }

    public void OnSpeedUp() {
      if (this.gold < 10) return;
      
      GameObject championObj = GameObject.FindGameObjectWithTag("Champion");
      Champion champion = championObj.GetComponent<Champion>();
      champion.speed += 1.1f;
      this.gold -= 10;
      this.UpdateGoldText();
      Debug.Log("upgrade" + champion.speed);
    }

    public void OnRelocate() {
      this.ResetObjects();
      spawnManager.RandomSpawnMonster();
      inCombat = false;
    }

    public void setGold(int value) {
      this.gold += value;
    }

    void UpdateGoldText() {
      GoldText.text = System.String.Format("Gold: {0:n0}", this.gold);
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
