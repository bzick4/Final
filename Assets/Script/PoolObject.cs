using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] private GameObject _Bullet; // Префаб объекта (например, пули)
    [SerializeField] private int _PoolSize = 10; // Размер пула

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {
        // Инициализация пула
        for (int i = 0; i < _PoolSize; i++)
        {
            GameObject obj = Instantiate(_Bullet);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // Получить объект из пула
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // Если пул пуст, создаём новый объект (опционально)
            GameObject obj = Instantiate(_Bullet);
            obj.SetActive(true);
            return obj;
        }
    }

    // Вернуть объект в пул
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}

