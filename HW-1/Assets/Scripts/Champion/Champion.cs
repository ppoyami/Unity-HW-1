using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : MonoBehaviour
{
  public float damage = 0.1f;
  public float speed = 10f;

  bool inArea = false;

  private Monster nearestMonster = null;

  private void Start() {
  }
  // TODO: 매 프레임마다 공격하는 것이 아니라, 일정한 속도로 공격하도록 하기
  void Update () {
		ChaseMonster();
    Attack();
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
    if (!this.nearestMonster) {
      FindClosestEnemy();
    } else {
      if (!this.inArea) {
        Debug.Log("근처에 몬스터가 없어서 추적을 시작합니다!");
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), this.nearestMonster.transform.position, 3 * Time.deltaTime);
      }
    }

  }


  void Attack() {
    if (this.nearestMonster && this.inArea) {
        Debug.Log("사정거리내에 들어온 몬스터를 공격합니다.");
      this.nearestMonster.GetComponent<Monster>().TakeDamage(this.damage);
    }
  }

  public void Die() {
    Destroy(gameObject);
  }

  private void OnDestroy() {
    // EventManager.DieEvent -= Die;
  }
  // 몬스터가 사정거리내에 있을 때
  private void OnTriggerEnter2D(Collider2D coll) {
    Debug.Log("몬스터가 사정거리 내에 있습니다.");
    inArea = true;
  }
  // 몬스터가 사정거리안에 없을 때
  private void OnTriggerExit2D(Collider2D coll) {
    Debug.Log("몬스터가 사정거리 내에 없습니다.");
    inArea = false;
  }
}
