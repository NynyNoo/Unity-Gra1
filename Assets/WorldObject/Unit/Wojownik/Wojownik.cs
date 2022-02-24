using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTS;
using Newtonsoft.Json;
public class Wojownik : Unit
{
    private Quaternion aimRotation;
    public int damage;
    private Animation animation;


    protected override void Start()
    {
        base.Start();
        moveSpeed = 7;
        weaponRange = 4;
        rotateSpeed = 15;
        if(damage==0) damage = 20;
        detectionRange = 2000;
        
    }

    protected override void Update()
    {
    

        base.Update();
        if (aiming)
        {
            if (!target.IsAlive())
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
        animation=GetComponentInChildren<Animation>();
        animation.Play("walk");
    }
    private void AttackingAnimation()
    {
        animation = GetComponentInChildren<Animation>();
        animation.Play("attack");
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
        InflictDamage();
        base.UseWeapon();
    }
    private void InflictDamage()
    {
        if (target) target.TakeDamage(damage);
    }
    public override void SaveDetails(JsonWriter writer)
    {
        base.SaveDetails(writer);
        SaveManager.WriteQuaternion(writer, "AimRotation", aimRotation);
    }
}
