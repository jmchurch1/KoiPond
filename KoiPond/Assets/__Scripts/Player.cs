using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 3;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(Input.GetAxis("Vertical") * _speed * Time.deltaTime, 0, 0);
        gameObject.transform.position += new Vector3(0, 0, Input.GetAxis("Horizontal") * _speed * Time.deltaTime);

        
    }
}
