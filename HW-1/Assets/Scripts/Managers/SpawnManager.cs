using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Monster;
    public GameObject Champion;
    public GameObject[] monsterSpawnRanges;
    List<BoxCollider2D> rangeColliders = new List<BoxCollider2D>();

    private void Awake()
    {
        foreach (GameObject rangeObj in monsterSpawnRanges) {
          BoxCollider2D collder = rangeObj.GetComponent<BoxCollider2D>();
          rangeColliders.Add(collder);
        }
    }

    // * 랜덤한 포지션 값 얻기
    Vector2 GetRandomPosition()
    {
        int randomIndex = (int)Random.Range(0, 4);
        Vector2 originPosition = monsterSpawnRanges[randomIndex].transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeColliders[randomIndex].bounds.size.x;
        float range_Y = rangeColliders[randomIndex].bounds.size.y;
        
        range_X = Random.Range( (range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range( (range_Y / 2) * -1, range_Y / 2);
        Vector2 RandomPostion = new Vector2(range_X, range_Y);

        Vector2 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
    // * 오브젝트 생성
    void SpawnObject(GameObject prefab, Vector2 pos)
    {
        GameObject monster = Instantiate(prefab);
        monster.transform.position = pos;
    }

    public void RandomSpawnMonster()
    {
        for (int i = 0; i < GameManager.Instance.monsterCount; i++) {
          Vector2 pos = this.GetRandomPosition();
          this.SpawnObject(Monster, pos);
        }
    }

    public void SpawnChampion() {
        for (int i = 0; i < GameManager.Instance.championCount; i++) {
          Vector2 pos = new Vector2(Random.Range(-2.0f, -1.0f), Random.Range(-1.0f, 0.0f));
          this.SpawnObject(Champion, pos);
        }
    }
}
