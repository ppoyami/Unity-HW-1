using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
  private float hp = 50f;

  private void Start() {
    EventManager.DieEvent += Die;
  }

  void TakeDamage(float value) {
    this.hp -= value;

    if (hp <= 0) {
      Destroy(gameObject);
    }
  }

  public void Die() {
    Destroy(gameObject);
  }
}
