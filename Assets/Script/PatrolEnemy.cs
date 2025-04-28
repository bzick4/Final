using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{

    [SerializeField] private Transform[] waypoints; // Массив точек маршрута
    [SerializeField] private float waitTime = 2f; // Время ожидания на точке
    [SerializeField] private float patrolSpeed = 3.5f; // Скорость патрулирования

    private NavMeshAgent agent; // Компонент NavMeshAgent для движения
    private int currentWaypointIndex = 0; // Индекс текущей точки маршрута
    private bool waiting = false; // Флаг ожидания на точке

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;

        if (waypoints.Length > 0)
        {
            MoveToNextWaypoint();
        }
    }

    private void Update()
    {
        // Проверяем, достиг ли враг текущей точки маршрута
        if (!waiting && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    private void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        // Получаем текущую позицию врага
        Vector3 currentPosition = transform.position;

        // Устанавливаем следующую точку маршрута, изменяя только координату X
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        targetPosition.y = currentPosition.y; // Сохраняем текущую высоту
        targetPosition.z = currentPosition.z; // Сохраняем текущую глубину

        agent.SetDestination(targetPosition);

        // Переходим к следующей точке (с циклическим переходом)
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }


    private IEnumerator WaitAtWaypoint()
    {
        waiting = true;

        // Ждём указанное время
        yield return new WaitForSeconds(waitTime);

        waiting = false;
        MoveToNextWaypoint();
    }
}

