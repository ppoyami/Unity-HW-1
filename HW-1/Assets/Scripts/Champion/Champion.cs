using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : MonoBehaviour
{
  public float damage = 10f;
  public float speed = 50f;

  private void Start() {
    EventManager.DieEvent += Die;
  }

  public void Die() {
    Destroy(gameObject);
  }
}
