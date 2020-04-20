﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverhaulCombat : MonoBehaviour
{
    //Set to display sword by default
    public GameObject sword, gun, bulletPrefab;

    //For animations
    private Animator animator;

    //Start point for all attacks
    public Transform attackPoint;

    private static float swordAttackRange = 0.5f;
    public static float attackRate = 2f;
    public static float nextAttackTime = 0f;
    public static int attackDamage = 5;

    public LayerMask enemiesLayer;

    public int maxAmmo = 10;
    public int currentAmmo;


    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime && currentAmmo > 0)
        {
            if (Input.GetMouseButton(0))
            {
                shoot();
                currentAmmo = currentAmmo - 1;
                Debug.Log("Current Ammo: " + currentAmmo);
                //Resets attack timer
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void melee()
    {

        //animator.SetTrigger("Sword Attack");
        //Damages all enemies within Circle
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, swordAttackRange, enemiesLayer);
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            hitEnemies[i].GetComponent<EnemyBasic>().takeDamage(attackDamage);
        }
    }

    void shoot()
    {
        //Fires a bullet
        Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
    }

    void OnDrawGizmosSelected()
    {
        //Just so we can see how big the size needs to be
        Gizmos.DrawWireSphere(attackPoint.position, swordAttackRange);
    }
}