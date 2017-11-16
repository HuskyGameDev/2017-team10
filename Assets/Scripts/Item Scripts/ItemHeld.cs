using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeld : MonoBehaviour {

    public int ammo = 0; //This will determine if you can access the item if not 0
    public bool ammoShow = true; //Whether or not to show the item if there's no ammo

    public int GetAmmo() {
        return ammo;
    }

    public int SetAmmo(int newAmmo) {
        return ammo = newAmmo;
    }

}

