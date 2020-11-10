using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapons : MonoBehaviourPun
{
    public Sprite woodenShield;
    public Sprite bronzeShield;
    public Sprite silverShield;
    public Sprite goldenShield;
    public Sprite diamondShield;

    public Sprite woodenSword;
    public Sprite bronzeSword;
    public Sprite silverSword;
    public Sprite goldenSword;
    public Sprite diamondSword;

    public SpriteRenderer weaponSprite;
    public SpriteRenderer shieldSprite;

    public static Weapons instance;

    void Start()
    {
        instance = this;
        weaponSprite.sprite = woodenSword;
        shieldSprite.sprite = woodenShield;
    }

    [PunRPC]
    void UpdateShield(int weaponNum)
    {
        switch (weaponNum)
        {
            case 5:
                shieldSprite.sprite = woodenShield;
                break;
            case 6:
                shieldSprite.sprite = bronzeShield;
                break;
            case 7:
                shieldSprite.sprite = silverShield;
                break;
            case 8:
                shieldSprite.sprite = goldenShield;
                break;
            case 9:
                shieldSprite.sprite = diamondShield;
                break;
        }
    }
    [PunRPC]
    void UpdateSword(int weaponNum)
    {
        switch (weaponNum)
        {
            case 0:
                weaponSprite.sprite = woodenSword;
                break;
            case 1:
                weaponSprite.sprite = bronzeSword;
                break;
            case 2:
                weaponSprite.sprite = silverSword;
                break;
            case 3:
                weaponSprite.sprite = goldenSword;
                break;
            case 4:
                weaponSprite.sprite = diamondSword;
                break;
        }
    }

}
