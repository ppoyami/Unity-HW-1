using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Monster;
    public GameObject Champion;

    void SpawnObject(GameObject prefab, Vector2 pos)
    {
        GameObject monster = Instantiate(prefab);
        monster.transform.position = pos;
    }
    // TODO: 생성되는 좌표 변수를 관리하기 - 몬스터, 챔피온
    public void RandomSpawnMonster()
    {
        for (int i = 0; i < 5; i++) {
          Vector2 pos = new Vector2(Random.Range(-8.0f, -3.25f), Random.Range(0.0f, 4.0f));
          this.SpawnObject(Monster, pos);
        }
        for (int i = 0; i < 5; i++) {
          Vector2 pos = new Vector2(Random.Range(-1.25f, 3.5f), Random.Range(0.0f, 4.0f));
          this.SpawnObject(Monster, pos);
        }
        for (int i = 0; i < 5; i++) {
          Vector2 pos = new Vector2(Random.Range(-8.0f, -3.25f), Random.Range(-4.0f, 0.0f));
          this.SpawnObject(Monster, pos);
        }
        for (int i = 0; i < 5; i++) {
          Vector2 pos = new Vector2(Random.Range(-1.25f, 3.5f), Random.Range(-4.0f, 0.0f));
          this.SpawnObject(Monster, pos);
        }
    }

    public void SpawnChampion() {
        for (int i = 0; i < 3; i++) {
          Vector2 pos = new Vector2(Random.Range(-3.25f, -1.25f), Random.Range(-3.25f, -1.25f));
          this.SpawnObject(Champion, pos);
        }
    }
}
