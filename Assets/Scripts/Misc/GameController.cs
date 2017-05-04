using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const int ENEMY_TYPES = 3;

    private Transform _tank;
    private GameObject _enemyPrefab;
    private Transform _enemiesRoot;
    private int _currentEnemyCount;

    private void Start()
    {
        _tank = GameObject.Find("Tank").transform;
        _enemiesRoot = GameObject.Find("Enemies").transform;
        _enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");

        StartCoroutine(CreateEnemyCrtn());
    }

    private IEnumerator CreateEnemyCrtn()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.5f);

            if (_currentEnemyCount < 10)
                CreateRandomEnemy();
        }
    }

    private void CreateRandomEnemy()
    {
        var rand = Random.Range(0, ENEMY_TYPES);
        var enemyName = "Enemy" + rand;
        var enemyInst = Instantiate(_enemyPrefab);
        enemyInst.transform.position = CalcRandomPosition();
        enemyInst.name = enemyName;
        var enemy = enemyInst.GetComponent<EnemyController>();
        enemy.Init(enemyName, _tank);
        enemyInst.transform.SetParent(_enemiesRoot, false);
        _currentEnemyCount++;
        enemy.OnDeath += DecreaseEnemyCount;
    }

    private Vector2 CalcRandomPosition()
    {
        var sign = new int[] { -1, 1 };
        var side = Random.Range(0, 1);
        var bounds = Utilities.GetScreenBoundsInWorldSpace();

        if(side == 0)
        {
            var x = Random.Range(bounds.x, bounds.z);
            var y = bounds.y * sign[Random.Range(0, sign.Length)];
            return new Vector2(x, y);
        }
        else
        {
            var x = bounds.x * sign[Random.Range(0, sign.Length)];
            var y = Random.Range(bounds.y, bounds.w);
            return new Vector2(x, y);
        }
    }

    private void DecreaseEnemyCount()
    {
        _currentEnemyCount--;
    }
}