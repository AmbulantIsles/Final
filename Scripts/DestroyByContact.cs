using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameControllerObject != null)
        {
            Debug.Log("Cannot find 'GameController' Script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if  (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("Pick Up"))
        {
            return;
        }
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        if (other.tag == "Player"){
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.UpdateLose();
        }
        if (other.CompareTag("Power Up"))
        {
            gameController.AddScore(scoreValue);
            Destroy(gameObject);
            return;
        }
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
