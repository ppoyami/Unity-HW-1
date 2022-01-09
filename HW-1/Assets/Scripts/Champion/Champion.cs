using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : MonoBehaviour
{
  [SerializeField]
  protected float _damage; // 데미지
  [SerializeField]
  protected float _speed; // 이동속도
  [SerializeField]
  protected float _delay; // 공격속도

  protected bool _inArea; // 사거리 내에 몬스터가 있는 지 여부
  protected bool _isAttack; // 공격 중 인지 여부

  protected Monster _nearestMonster; // 가장 가까운 몬스터

  public float Damage {get {return _damage;} set {_damage = value;}}
  public float Speed {get {return _speed;} set {_speed = value;}}
  public float Delay {get {return _delay;} set {_delay = value;}}

  private void Awake() {
    _damage = 10.0f;
    _speed = 1.0f;
    _delay = 1.0f;
    _inArea = false;
    _isAttack = false;
    _nearestMonster = null;
  }

  void Update () {
		AI();
	}

  // * 가장 가까운 적을 찾기
	void FindClosestMonster()
	{
    _isAttack = false;
		float distanceToClosestMonster = Mathf.Infinity;
		Monster[] allMonsters = GameObject.FindObjectsOfType<Monster>();

		foreach (Monster monster in allMonsters) {
			float distanceToMonster = (monster.transform.position - this.transform.position).sqrMagnitude;
			if (distanceToMonster < distanceToClosestMonster) {
				distanceToClosestMonster = distanceToMonster;
				this._nearestMonster = monster;
			}
		}
	}
  // * 가까운 몬스터 추적하기
  void ChaseMonster() {
    _isAttack = false;
    transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), this._nearestMonster.transform.position, this._speed * Time.deltaTime);
  }

  // * 공격하기
  IEnumerator Attack() {
    _isAttack = true;
    while (true)
    {
      Debug.Log("사정거리내에 들어온 몬스터를 공격합니다.");
      this._nearestMonster.GetComponent<Monster>().TakeDamage(this._damage);
      yield return new WaitForSeconds(this._delay);
    }
  }

  void AI() {
    if (!this._nearestMonster) { // 타겟팅 된 몬스터가 없을 때
      StopCoroutine("Attack");
      this.FindClosestMonster();
    } 
    else if (!this._inArea) { // 타켓팅 된 몬스터가 사거리 내에 없을 때
        StopCoroutine("Attack");
        this.ChaseMonster();
    } 
    else if(!this._isAttack) { // 사거리내에 있는 몬스터를 공격하지 않았을 때
        StartCoroutine("Attack");
    } else { // 사거리 내에 몬스터가 있고, 공격 중 일 때,
      return;
    }
  }
  
  public void Die(bool isReset = false) {
    Destroy(gameObject);
  }

  // 몬스터가 사정거리내에 있을 때
  private void OnCollisionEnter2D(Collision2D coll) {
    if (coll.gameObject.CompareTag("Monster")) {
      _inArea = true;
    }
  }

  // 몬스터가 사정거리안에 없을 때
  private void OnCollisionExit2D(Collision2D coll) {
    if (coll.gameObject.CompareTag("Monster")) {
      Debug.Log("근처에 몬스터가 없어서 추적을 시작합니다!");
      _inArea = false;
    }
  }
}
