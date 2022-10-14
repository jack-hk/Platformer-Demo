using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSwitch : MonoBehaviour
{
    private GameObject door;
    private SpriteRenderer childSprite;

    private void Awake()
    {
        door = GameObject.Find("GreenDoor");
        childSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (LayerTagManager.ContainsBoomerangTag(collider))
        {
            childSprite.enabled = false;
            door.SetActive(false);
        }
    }
}
