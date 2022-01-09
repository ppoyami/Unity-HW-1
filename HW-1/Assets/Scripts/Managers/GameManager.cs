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

    public int championCount = 3;
    public int monsterCount = 10;

    int gold = 0;
    bool inCombat = false;

    enum Upgrade {
      Damage, Speed
    }

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

    // * 업그레이드
    void UpgradeChampions(Upgrade type) {
      if (this.gold < 10) return;

      GameObject[] champions = GameObject.FindGameObjectsWithTag("Champion");
      foreach (GameObject championObj in champions) {
        Champion champion = championObj.GetComponent<Champion>();
        switch (type)
        {
            case Upgrade.Damage:
              champion.Damage *= 1.1f;
              Debug.Log("upgrade" + champion.Damage);
              break;
            case Upgrade.Speed:
              champion.Speed *= 1.1f;
              Debug.Log("upgrade" + champion.Speed);
              break;
            default:
              return;
        }
      }
      this.gold -= 10;
      this.UpdateGoldText();
    }

    public void OnDamageUp() {
      UpgradeChampions(Upgrade.Damage);
    }

    public void OnSpeedUp() {
      UpgradeChampions(Upgrade.Speed);
    }

    // * 재배치
    void ResetObjects() {
      GameObject[] champions = GameObject.FindGameObjectsWithTag("Champion");
      GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

      for (int i = 0; i < champions.Length; i++) {
        champions[i].GetComponent<Champion>().Die(true);
      }
      for (int i = 0; i < monsters.Length; i++) {
        monsters[i].GetComponent<Monster>().Die(true);
      }
    }
    public void OnRelocate() {
      this.ResetObjects();
      spawnManager.RandomSpawnMonster();
      inCombat = false;
    }
    
    // * 골드 텍스트 UI
    public void setGold(int value) {
      this.gold += value;
    }

    void UpdateGoldText() {
      GoldText.text = System.String.Format("Gold: {0:n0}", this.gold);
    }

    // * 전투시작 - 땅 클릭
    public void OnStartCombat() {
      if (!inCombat) {
        spawnManager.SpawnChampion();
      }

      inCombat = true;
    }
}
