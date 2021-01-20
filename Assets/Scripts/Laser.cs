using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //variable definition
    [SerializeField]
    private float _speed = 8.0f;
    // Update is called once per frame
    void Update()
    {
        //if position.y > 8, destroy object
        if (transform.position.y > 8f)
            {
            if(transform.parent != null)
                {
                Destroy(transform.parent.gameObject);
                }
                Destroy(this.gameObject);
            }
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        
    }
}
