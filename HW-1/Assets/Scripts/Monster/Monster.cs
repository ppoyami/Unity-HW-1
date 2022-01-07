using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
  private float hp = 50f;
  public int gold = 10;

  private void Start() {
  }

  public void TakeDamage(float value) {
    this.hp -= value;

    if (hp <= 0) {
      this.Die();
    }
  }

  public void Die() {
    Destroy(gameObject);
    GameManager.Instance.setGold(this.gold);
    EventManager.RunMonsterDieEvent();
  }

  private void OnDestroy() {
    // EventManager.DieEvent -= Die;
  }
}
