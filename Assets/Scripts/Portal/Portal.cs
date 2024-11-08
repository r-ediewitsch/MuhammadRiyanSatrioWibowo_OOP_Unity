using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    Vector2 newPosition;

    void Start()
    {
        ChangePosition();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        if (Player.Instance != null)
        {
            bool hasWeapon = Player.Instance.currentWeapon != null;

            GetComponent<SpriteRenderer>().enabled = hasWeapon;
            GetComponent<Collider2D>().enabled = hasWeapon;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.LoadScene("Main");
            }
        }
    }

    void ChangePosition()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-10f, 10f);
        newPosition = new Vector2(randomX, randomY);
    }
}
