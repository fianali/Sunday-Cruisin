using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillTester : MonoBehaviour
{
    [SerializeField] private Rigidbody thisRigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody.velocity = new Vector3(10, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
