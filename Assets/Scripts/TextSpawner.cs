using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    [SerializeField] 
    private DamageText _damageTextPrefab;

    [SerializeField] 
    private Camera main;

    public void ShowText(string text, Vector3 position)
    {
        Vector3 screenPosition = main.WorldToScreenPoint(position);
        DamageText instance = Instantiate(_damageTextPrefab, transform);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }


}
