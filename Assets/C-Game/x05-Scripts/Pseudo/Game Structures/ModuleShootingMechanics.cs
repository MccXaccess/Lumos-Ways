using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleShootingMechanics : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform[] shootPoints;
    public float shootForce = 10f;
    public float interval = 1f;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        while ( true )
        {
            foreach(Transform pos in shootPoints)
            {
                GameObject projectile = Instantiate(projectilePrefab, pos.position, pos.rotation);
                Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.AddForce(pos.right * shootForce, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(interval);
        }
    }
}