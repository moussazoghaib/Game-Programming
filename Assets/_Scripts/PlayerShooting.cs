﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour { 

    Animator player_anim;
    bool firing_start;
    public GameObject projectile;
    public GameObject Player;
   //private GameObject tempProjectile;
    public List<GameObject> pooledObjects;
    public int amountToPool = 20;
    //private Vector3 mousePos;
    private AudioSource audiosrc;
    public AudioClip  spellAudio;

    void Start()
    {
       // Debug.Log("hello 1");
        player_anim = GetComponent<Animator>();
        firing_start = false;
        audiosrc = GetComponent<AudioSource>();

        pooledObjects = new List<GameObject>();
        amountToPool = 20;
        for (int i = 0; i < amountToPool; i++)
        {
            //Debug.Log("hello ; player shooting script");
            GameObject obj = (GameObject)Instantiate(projectile);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!firing_start)
            {
                firing_start = true;
                /*tempProjectile = GameObject.Instantiate(projectile, transform);
                tempProjectile.transform.SetParent(null);
                tempProjectile.GetComponent<Rigidbody>().AddForce(Player.transform.forward * 15, ForceMode.Impulse);*/
                GameObject tempProjectile =GetPooledObject();
              //  Debug.Log("Player shooting script; pooledObjects length"+ pooledObjects.Count);
                if (tempProjectile != null)
                {
                    tempProjectile.transform.position = this.transform.position + new Vector3(0.129f, 0.991f, -0.027f);
                    tempProjectile.transform.rotation = this.transform.rotation;
                    tempProjectile.SetActive(true);
                    tempProjectile.transform.SetParent(null);
                    tempProjectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    tempProjectile.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    tempProjectile.GetComponent<Rigidbody>().AddForce(Player.transform.forward * 15, ForceMode.Impulse);
                }
                StartCoroutine("Fire");
            }
        }
    }

    private IEnumerator Fire()
    {
        // Play the animation for firing


        player_anim.SetBool("isFiring", true);

        yield return new WaitForSeconds(0.45f);

        player_anim.SetBool("isFiring", false);

        firing_start = false;
    }

    public GameObject GetPooledObject()
    {
        //1
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //2
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        //3
        GameObject obj = (GameObject)Instantiate(projectile);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    public void PlaySpell()
    {
        audiosrc.clip = spellAudio;
        audiosrc.Play();
    }
}
