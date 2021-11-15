using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject AttackPrefab;
    [SerializeField] private float startDelay;
    [SerializeField] private float attackDelay;
    [SerializeField] private bool looping = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(startDelay);
        do
        {
            yield return StartCoroutine(Spawn());
        } while (looping);

    }

    private IEnumerator Spawn()
    {
        var newAttack = Instantiate(AttackPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(attackDelay);
    }
}
