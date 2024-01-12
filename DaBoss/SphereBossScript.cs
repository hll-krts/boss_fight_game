using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBossScript : MonoBehaviour
{
    [SerializeField] private GameObject NA_Prefab, DoubleA_Prefab;
    private GameObject Player;
    bool alive;
    // Start is called before the first frame update
    private void Awake()
    {
        alive = FindObjectOfType<PlayrInputScript>().Alive;
        Player = FindObjectOfType<PlayrInputScript>().gameObject;
        StartCoroutine(Attack());
        StartCoroutine(DoubleAttack());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform.position);
    }

    IEnumerator Attack()
    {
        while (alive)
        {
            yield return new WaitForSeconds(0.3f);
            Instantiate(NA_Prefab);
        }
    }
    IEnumerator DoubleAttack()
    {
        while (alive)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            Instantiate(DoubleA_Prefab); 
        }
    }
}
