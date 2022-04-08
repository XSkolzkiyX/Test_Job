using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Transform Finish;
    public float speed, scaleValue;
    public LayerMask obstaclesLayer;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Finish.position, speed * Time.deltaTime);
        if (transform.position == Finish.position) Explode();
    }

    void Explode()
    {
        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + transform.localScale.z);
        Collider[] hitObstacles = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + transform.localScale.z), transform.localScale.x * scaleValue, obstaclesLayer);
        foreach(Collider obstacle in hitObstacles)
        {
            //Destroy(obstacle.gameObject);
            obstacle.GetComponent<ObstacleScript>().Explode();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag != "Player") Explode();
    }
}
