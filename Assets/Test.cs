using DeadLords.Shooter;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Transform barrel;
    public int[] distance = { 20, 50, 100 };

    [SerializeField] private Image _hit;
    RaycastHit hit;
    Transform origin;

    void Start()
    {
        _hit = Main.Instance.GetObjectManager.Hit;
        HitDissapear();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            origin = barrel;

            StartCoroutine(NextRay(0));

            Debug.DrawRay(origin.position, origin.transform.forward * distance[0], Color.green, 0.2f);
        }
    }

    void HitDissapear() { _hit.enabled = false; }

    IEnumerator NextRay(int index)
    {
        yield return new WaitForSeconds(index / 10);

        if (Physics.Raycast(origin.position, origin.transform.forward, out hit, distance[index]))
        {
            if (hit.transform.tag == "Enemy")
            {
                _hit.enabled = true;
                Invoke("HitDissapear", 0.1f);
            }
        }
        else
        {
            if (index == 0)
                StartCoroutine(NextRay(1));
            else
                StartCoroutine(NextRay(2));
        }
        Debug.DrawRay(origin.position, origin.transform.forward * distance[1], Color.red, 0.2f);
    }
}
