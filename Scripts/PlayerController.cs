using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    private Rigidbody rb;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    public AudioSource musicSource;

    //Powerups
    public float powerTimer;
    public GameObject starSheild;

    private void Start()
    {
        musicSource.Stop();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            musicSource.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Pick Up"))
        {
            Destroy(other.gameObject);
            ActivateStarShield();
            return;
        }
    }

    void ActivateStarShield()
    {
        float i = 0;
        while (i < powerTimer)
        {
            starSheild.SetActive(true);
            i++;
        }
    }

}