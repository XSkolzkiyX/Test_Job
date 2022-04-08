using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public Material infectedMaterial;
    public Animator disappearanceAnim;

    public void Explode()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material = infectedMaterial;
        disappearanceAnim.SetTrigger("Start");
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.GetComponent<PlayerController>().Die();
        }
    }
}
