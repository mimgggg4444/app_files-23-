using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type
    {Ammo,Coin,Grenade,Heart,Weapon};
    
    public Type type; 
    public int value;

    void Update()
    {
        //아이템 회전
    transform.Rotate(Vector3.up*20*Time.deltaTime);    
    }

}
