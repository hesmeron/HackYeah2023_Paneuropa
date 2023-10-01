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
        DamageText instance = Instantiate(_damageTextPrefab, transform);
        instance.transform.position = new Vector3(position.x ,position.y ,-5);
        instance.SetText(text);
    }


}
