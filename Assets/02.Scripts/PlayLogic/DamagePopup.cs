using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro _textMesh;

    private void Awake()
    {
        _textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damage, Vector3 pos)
    {
        _textMesh.SetText(damage.ToString());
        transform.position = pos;
        Invoke(nameof(Delete), 1);
    }


    private void Delete()
    {
        Destroy(gameObject);
    }
}
