using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : MonoBehaviour
{
  public float damage = 10f;
  public float speed = 50f;

  private Monster nearestMonster = null;

  private void Start() {
  }
  void Update () {
		ChaseMonster();
	}

  // TODO: 몬스터가 죽을 때마다(이벤트) 업데이트되는 배열을 제공받으면 좋을 거 같음
	void FindClosestEnemy()
	{
		float distanceToClosestEnemy = Mathf.Infinity;
		Monster[] allEnemies = GameObject.FindObjectsOfType<Monster>();

		foreach (Monster currentEnemy in allEnemies) {
			float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
			if (distanceToEnemy < distanceToClosestEnemy) {
				distanceToClosestEnemy = distanceToEnemy;
				this.nearestMonster = currentEnemy;
			}
		}
	}

  // TODO: 추적함수는, 자신이 공격하는 몬스터가 null이 되었으면 호출하는 것이 좋을 거 같음
  void ChaseMonster() {
    FindClosestEnemy();

    transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), this.nearestMonster.transform.position, 3 * Time.deltaTime);
  }

  public void Die() {
    Destroy(gameObject);
  }

  private void OnDestroy() {
    // EventManager.DieEvent -= Die;
  }
}
