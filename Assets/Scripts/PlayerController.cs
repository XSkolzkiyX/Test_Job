using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform Finish;
    public GameObject bulletPrefab, path, finPanel;
    public Text finText;
    public Animator doorAnim;
    public float speed, target, scaleOfBullet, step;
    public bool needToMove = false;
    bool animStarted = false, canMove = true;

    void Update()
    {
        if (canMove)
        {
            path.transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, 0.2f, Mathf.Infinity), path.transform.localScale.y, path.transform.localScale.z);
            if (Input.touchCount > 0 && !needToMove)
            {
                Touch touch = Input.GetTouch(0);
                scaleOfBullet += Time.deltaTime;
                if (touch.phase == TouchPhase.Ended)
                {
                    //needToMove = true;
                    target = transform.position.z + step;
                    StartCoroutine(ShootAndGo());
                }
            }
            if (needToMove)
            {
                if (transform.position.z < Finish.position.z - 3f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, target), speed * Time.deltaTime);
                    if (transform.position.z == target) needToMove = false;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, Finish.transform.position.z), speed * Time.deltaTime);
                    finPanel.SetActive(true);
                    finText.text = "You Won!";
                    //canMove = false;
                }
            }
            if (transform.position.z >= Finish.transform.position.z - 6f && !animStarted)
            {
                doorAnim.SetTrigger("Start");
                animStarted = true;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Die()
    {
        finPanel.SetActive(true);
        finText.text = "You Lost!";
        canMove = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Shoot()
    {
        float scale = Mathf.Clamp(transform.localScale.x - scaleOfBullet, 0, Mathf.Infinity);
        transform.localScale = new Vector3(scale, scale, scale);
        if (scale <= 0) Die();
        else
        {
            GameObject curBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            curBullet.transform.localScale = new Vector3(scaleOfBullet, scaleOfBullet, scaleOfBullet);
            curBullet.GetComponent<BulletScript>().Finish = Finish;
            //new Vector3(transform.position.x, transform.position.y, scale + scaleOfBullet)
        }
        scaleOfBullet = 0;
    }

    IEnumerator ShootAndGo()
    {
        Shoot();
        yield return new WaitForSeconds(1f);
        needToMove = true;
    }
}