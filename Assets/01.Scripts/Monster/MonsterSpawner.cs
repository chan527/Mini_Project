using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float summonDelay;

    WaitForSeconds summonDelayWait;
    WaitForSeconds reduceDelayWait;

    [SerializeField] List<Rect> spawnAreas = new List<Rect>();

    void Start()
    {
        summonDelay = 1f;
        summonDelayWait = new WaitForSeconds(summonDelay);

        reduceDelayWait = new WaitForSeconds(30f);

        spawnAreas.Add(new Rect(-20, 12, 40, 8));
        spawnAreas.Add(new Rect(-20, -20, 40, 8));
        spawnAreas.Add(new Rect(-20, -20, 8, 40));
        spawnAreas.Add(new Rect(12, -20, 8, 40));

        StartCoroutine(SummonMonsterCoroutine());
        StartCoroutine(ReduceSummonDelayCoroutine());
    }

    IEnumerator SummonMonsterCoroutine()
    {
        while (true)
        {
            yield return summonDelayWait;

            SummonMonster();
        }
    }

    private void SummonMonster()
    {
        // 어떤 Rect 영역에서 스폰할지 랜덤 선택
        Rect spawnRect = spawnAreas[Random.Range(0, spawnAreas.Count)];

        // 스폰 할 때 스폰 영역에 랜덤한 위치를 정하고 플레이어 위치를 더하여 스폰 위치 계산
        Vector2 randPos = new Vector2(Random.Range(spawnRect.xMin, spawnRect.xMax) + player.position.x, 
                                        Random.Range(spawnRect.yMin, spawnRect.yMax) + player.position.y);

        GameObject monster = ObjectPoolManager.instance.GetObject("Monster");
        monster.transform.position = randPos;
        monster.GetComponent<MonsterController>().SetTarget(player);

        Debug.Log("몬스터 스폰");
    }

    IEnumerator ReduceSummonDelayCoroutine()
    {
        while (true)
        {
            yield return reduceDelayWait;

            ReduceSummonDelay();
        }
    }

    public void ReduceSummonDelay()
    {
        summonDelay *= 0.95f;
        summonDelayWait = new WaitForSeconds(summonDelay);
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = new Color(1, 0, 0, 0.3f);

        foreach(var area in spawnAreas)
        {
            Vector3 center = new Vector3((area.x + area.width / 2) + player.position.x, 
                                            (area.y + area.height / 2) + player.position.y);
            Vector3 size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);
        }
    }
}

