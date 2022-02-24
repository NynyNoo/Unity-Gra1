using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class Lucznik : Unit
{
    private Quaternion aimRotation;
    public int damage;
    private Animator animator;
    protected override void Start()
    {
        base.Start();
        moveSpeed = 7;
        rotateSpeed = 1;
        if (damage == 0) damage = 20;
        detectionRange = 2000;
    }

    protected override void Update()
    {
        base.Update();
        if (aiming)
        {
            if(!target.IsAlive())
                aiming = false;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, aimRotation, weaponAimSpeed);
            CalculateBounds();
            //sometimes it gets stuck exactly 180 degrees out in the calculation and does nothing, this check fixes that
            Quaternion inverseAimRotation = new Quaternion(-aimRotation.x, -aimRotation.y, -aimRotation.z, -aimRotation.w);
            if (transform.rotation == aimRotation || transform.rotation == inverseAimRotation)
            {
                aiming = false;
            }
        }
        if (moving)
            MovingAnimation();
        if (attacking)
            AttackingAnimation();
    }
    private void MovingAnimation()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play("Combat_run");
    }
    private void AttackingAnimation()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play("Attack1");
    }
    public override bool CanAttack()
    {
        return true;
    }
    protected override void AimAtTarget()
    {
        base.AimAtTarget();
        aimRotation = Quaternion.LookRotation(target.transform.position - transform.position);
    }
    protected override void UseWeapon()
    {
        base.UseWeapon();
        Vector3 spawnPoint = transform.position;
        spawnPoint.x += (1 * transform.forward.x);
        spawnPoint.y += 1;
        spawnPoint.z += (1 * transform.forward.z);
        GameObject gameObject = (GameObject)Instantiate(ResourceManager.GetWorldObject("ArrowProjectile"), spawnPoint, transform.rotation);
        Projectile projectile = gameObject.GetComponentInChildren<Projectile>();
        projectile.SetRange(1.0f * weaponRange);
        projectile.SetTarget(target);
    }
}
