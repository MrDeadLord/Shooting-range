using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class StrategyMove : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float speed;

    CharacterController _charContr;
    Queue<Transform> _pointsQueue = new Queue<Transform>();

    private void Start()
    {
        _charContr = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Aim") && Input.GetKey(KeyCode.LeftShift) ||
            Input.GetButtonDown("Aim") && Input.GetKey(KeyCode.RightShift))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                _pointsQueue.Enqueue(DrowedPoint(hit.point));   //Добавление точки в очередь

                SetTarget(_pointsQueue.Peek());
            }
        }
        else if (Input.GetButtonDown("Aim"))
        {
            _pointsQueue.Clear();

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                _pointsQueue.Enqueue(DrowedPoint(hit.point));   //Добавление точки в очередь
                                
                SetTarget(_pointsQueue.Peek());
            }
        }

        if (_pointsQueue.Count == 0)
            return;
        else if (!_pointsQueue.Peek())
            _pointsQueue.Dequeue();
        else
            SetTarget(_pointsQueue.Peek());
    }

    /// <summary>
    /// Создает point и возвращает его Transform
    /// </summary>
    /// <param name="point">Создаваемый объект</param>
    /// <returns></returns>
    private Transform DrowedPoint(Vector3 point)
    {
        var temPoint = Instantiate(_target, point, Quaternion.identity);

        return temPoint.transform;
    }

    void SetTarget(Transform target)
    {
        gameObject.transform.LookAt(target);

        _charContr.Move(target.position * Time.deltaTime);

        //gameObject.transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Marker")
        {
            _pointsQueue.Clear();

            Destroy(collision.gameObject);
        }
    }
}
