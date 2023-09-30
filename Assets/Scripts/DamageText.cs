using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text _text;
    [SerializeField]
    private AnimationCurve _sizeCurve = AnimationCurve.Linear(1f,0f, 0f, 1f);
    [SerializeField]
    private AnimationCurve _alphaCurve = AnimationCurve.Linear(1f,0f, 0f, 1f);
    [SerializeField]
    private float _flightDuration = 1.5f;
    [SerializeField] 
    private float _speed = 0.1f;
    private float _currentTime = 0f;

    void Update()
    {
        _currentTime += Time.deltaTime;
        float proportion = _currentTime / _flightDuration;
        if (proportion > 1f)
        {
            Destroy(this.gameObject);
            return;
        }

        float size = _sizeCurve.Evaluate(proportion);
        transform.localScale = Vector3.one *size;
        float alpha = _alphaCurve.Evaluate(proportion);
        _text.alpha = alpha;
        transform.position += Vector3.up * Time.deltaTime * _speed;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
