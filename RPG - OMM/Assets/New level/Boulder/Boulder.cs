using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Rigidbody2D rb;
    private float startPosY;
    public static bool boulderStart;
    public GameObject Fakel;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosY = transform.GetChild(0).position.y;
    }
    private void Update()
    {
        if (boulderStart)
        {
            transform.GetChild(0).position = new Vector2(transform.position.x - 0.6f, startPosY);
            transform.GetChild(0).rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0, 0, -90) * 1.4f * Time.deltaTime);
            rb.velocity = new Vector2(1, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Destroy")
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Destroy").Length; i++)
            {
                Debug.Log(i);
                Destroy(GameObject.FindGameObjectsWithTag("Destroy")[i]);
            }
            Fakel.SetActive(true);
        }
        else if(collision.transform.tag == "Ignore")
        {
            collision.transform.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(collision.transform.GetChild(0).gameObject);
        }
        StartCoroutine(OnView());
    }


    private IEnumerator OnView()
    {
        yield return new WaitForSeconds(11f);
        GetComponent<SpriteRenderer>().sortingOrder = 22;
    }
}
