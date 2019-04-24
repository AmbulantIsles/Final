using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

    public float pewPew;

    private AudioSource audioSource;


    void Start()
    {
        pewPew = 1;
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate*pewPew);
    }

    void Fire ()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
