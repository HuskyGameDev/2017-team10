//ItemSwitch handles when the player wants to select a different object in their inventory.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwitch : MonoBehaviour {

    public int selectedObj = 0, itemIndex = 0;
    public bool recheckItems = false;

    // Use this for initialization
    void Start() {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update() {

        itemIndex = transform.childCount - 1;

        //Scroll Up for next weapon   
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            if (selectedObj >= itemIndex)
                selectedObj = 0;
            else
                selectedObj++;

            SelectWeapon();
            //Scroll Down for previous
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            if (selectedObj - 1 < 0)
                selectedObj = itemIndex;
            else
                selectedObj--;

            SelectWeapon();
        }

        //For top row of number keys
        if (Input.GetKeyDown(KeyCode.Alpha1) && itemIndex >= 0) {
            selectedObj = 0;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && itemIndex >= 1) {
            selectedObj = 1;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && itemIndex >= 2) {
            selectedObj = 2;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && itemIndex >= 3) {
            selectedObj = 3;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && itemIndex >= 4) {
            selectedObj = 4;
            SelectWeapon();
        }

        if (recheckItems) { //This is for when you have zero ammo and can't show it on zero ammo, but then you get more ammo and need to refresh.
            SelectWeapon();
            recheckItems = !recheckItems;
        }

    }

    void SelectWeapon() {
        int index = 0;

        //loops through each child in the WeaponHolder
        foreach (Transform weapon in transform) {
            if (index == selectedObj) {
                if(!weapon.GetComponent<ItemHeld>().ammoShow && weapon.GetComponent<ItemHeld>().GetAmmo() <= 0)
                    weapon.gameObject.SetActive(false);
                else if (weapon.GetComponent<ItemHeld>().ammoShow && weapon.GetComponent<ItemHeld>().GetAmmo() == 0)
                    weapon.gameObject.SetActive(true);
                else if (!weapon.GetComponent<ItemHeld>().ammoShow && weapon.GetComponent<ItemHeld>().GetAmmo() != 0)
                    weapon.gameObject.SetActive(true);
            }
            else {
                weapon.gameObject.SetActive(false);
            }

            index++;
        }

    }
}
