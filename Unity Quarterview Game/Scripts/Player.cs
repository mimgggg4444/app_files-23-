using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject[] weapons;
    public bool[] hasWeapons;

    public float speed;
    float hAxis;
    float vAxis;
    Vector3 moveVec;
    Vector3 dodgeVec;

    public int ammo; public int coin; public int health; 
    public int maxAmmo; public int maxCoin; public int maxHealth; public int maxHasGrenades;

    public GameObject[] grenades;
    public int hasGrenades;

    public Camera followCamera;



    bool wDown;
    bool jDown;
    bool iDown;
    bool fDown;
    bool rDown;
    bool sDown1;
    bool sDown2;



    bool isSwap;
    bool isReload;
    bool isFireReady = true;
    bool isBorder;

    Animator anim;
    Rigidbody rigid;

    bool isJump;
    bool isDodge;

    GameObject nearObject;
    Weapon equipWeapon;
    int equipWeaponIndex = -1;
    float fireDelay;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Attack();
        Reload();

        Dodge();
        Swap();
        Interation();

     }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButton("Fire1");
        rDown = Input.GetButtonDown("Reload");
        iDown = Input.GetButtonDown("Interation");

        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");


    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;

        if (isSwap || isReload ||!isFireReady)
            moveVec = Vector3.zero;

        if (!isBorder)
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;


        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

    }
    void Turn()
    {

        transform.LookAt(transform.position + moveVec);

        if (fDown)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec);
            }
        }
    }
    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump&&!isDodge && !isSwap)
        {rigid.AddForce(Vector3.up*15,ForceMode.Impulse);
            anim.SetBool("isJump",true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void Attack()
    {
        if (equipWeapon == null)
            return;
        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(fDown&&isFireReady && !isDodge && !isSwap)
        {
            equipWeapon.Use();
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
            fireDelay = 0;
        }
    }

    void Reload()
    {
        if(equipWeapon == null) return;
        if(equipWeapon.type == Weapon.Type.Melee) return;
        if (ammo == 0) return;
        if (rDown && !isJump && !isDodge && !isSwap && !isFireReady)
        {
            anim.SetTrigger("doReload");
            isReload = true;

            Invoke("ReloadOut", .9f);
        }
    }



    void ReloadOut()
    {
        int reAmmo = ammo < equipWeapon.maxAmmo ? ammo : equipWeapon.maxAmmo;
        equipWeapon.curAmmo = reAmmo;

        ammo -= reAmmo;
        isReload = false;
    }


    void Dodge()
    {
        if (jDown && moveVec!=Vector3.zero &&!isJump && !isDodge&&!isSwap)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;
            Invoke("DodgeOut", .5f);
        }
    }
    void DodgeOut()
    {
        speed *= .5f;
        isDodge=false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isJump)
        {
            if (collision.gameObject.tag == "Floor")
            {
                anim.SetBool("isJump", false);

                isJump = false;
            }
        }
    }


    void SwapOut()
    {
        isSwap = false;
    }
    void Swap() {

        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;

        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;

        if ((sDown1 || sDown2) && !isJump && !isDodge)
        {
            if(equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("doSwap"); 
            isSwap = true;
            Invoke("SwapOut", .4f);
        }
    }
    void Interation()
    {
        if(iDown && nearObject != null && !isJump && !isDodge) {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;

                hasWeapons[weaponIndex] = true;
                Destroy(nearObject);
            }
        }
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5 , Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }
    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = other.gameObject;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
    if(other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch(item.type) { 
                case Item.Type.Ammo:
                    ammo += item.value;
                    if (ammo > maxAmmo)
                        ammo=maxAmmo;   
                    break;
                case Item.Type.Coin:
                    coin+= item.value;
                    if (coin > maxCoin)    
                        coin = maxCoin;
                    break;
                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;
                case Item.Type.Grenade:
                    grenades[hasGrenades].SetActive(true);
                    hasGrenades+= item.value;
                    if (hasGrenades > maxHasGrenades)
                        hasGrenades = maxHasGrenades;
                    break;
            }
            Destroy(other.gameObject);
        }    
    }
}

