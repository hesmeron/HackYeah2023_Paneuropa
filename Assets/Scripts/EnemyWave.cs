using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [SerializeField]
    private List<Fighter> _figters;

    public List<Fighter> Figters => _figters;
}
