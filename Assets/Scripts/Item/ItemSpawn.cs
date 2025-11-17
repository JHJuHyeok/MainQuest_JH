using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnArea;
    [SerializeField] private BoxCollider spawnCollider;
    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private int spawnCount = 5;    // 초반 생성 갯수
    [SerializeField] private int spawnMax = 15;     // 전체 생성 갯수

    private void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject instantArea = Instantiate(itemPrefab,
                Return_RandomPosition(), Quaternion.identity);
        }
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = spawnArea.transform.position;
        float range_X = spawnCollider.bounds.size.x;
        float range_Z = spawnCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPosition = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPosition;
        return respawnPosition;
    }


}
