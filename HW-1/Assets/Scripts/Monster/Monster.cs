using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
  [SerializeField]
  protected float _hp;
  [SerializeField]
  protected int _gold;

  public float Hp {get {return _hp;} set {_hp = value;}}
  public int Gold {get {return _gold;} set {_gold = value;}}

  private void Awake() {
    _hp = 50.0f;
    _gold = 10;
  }

  public void TakeDamage(float value) {
    _hp -= value;
    Debug.Log("몬스터가 데미지를 입습니다." + value);
    if (_hp <= 0) {
      this.Die();
    }
  }

  public void Die(bool isReset = false) {
    Destroy(gameObject);
    if (!isReset) {
      GameManager.Instance.setGold(_gold);
      EventManager.RunMonsterDieEvent();
    }
  }

  private void OnDestroy() {
    // EventManager.DieEvent -= Die;
  }
}
