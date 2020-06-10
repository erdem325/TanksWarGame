using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehavior : MonoBehaviour
{
  
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    float forceAmaount = 1200f;
    float timeFromLastShoot;


    public void Shoot(float shootFreq)
    {
        
        if ((timeFromLastShoot += Time.deltaTime) >= 1f/ shootFreq)
        {
            InstantiateBullet();
            timeFromLastShoot = 0;
        }
    }

    public void Shoot()
    {
        InstantiateBullet();
        
    }


    private void InstantiateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
       

        bullet.GetComponent<Rigidbody>().AddForce(forceAmaount * transform.forward);
        
    }

}
