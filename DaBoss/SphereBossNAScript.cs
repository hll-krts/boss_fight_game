using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBossNAScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject sphereBoss;

    private void Awake()
    {
        Destroy(this, 3);

        sphereBoss = FindObjectOfType<SphereBossScript>().gameObject;
        rb = GetComponent<Rigidbody>();

        this.transform.position = sphereBoss.transform.position;
        transform.rotation = sphereBoss.transform.rotation;
    }
    void FixedUpdate()
    {
        rb.AddForce(transform.forward * 40f, ForceMode.Force);
    }
}
