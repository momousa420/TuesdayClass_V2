using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GoGoGo rubyGO = collision.GetComponent<GoGoGo>();
        print("�I�쪺�F��O�G" + rubyGO);
        rubyGO.ChangeHealth(-1);

    }
}
