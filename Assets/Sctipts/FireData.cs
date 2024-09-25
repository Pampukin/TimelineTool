using UnityEngine;

[CreateAssetMenu(fileName = "FireData", menuName = "ScriptableObject/Create FireData")]
public class FireData : ScriptableObject
{
    [SerializeField] private float _speed;

    public float Speed => _speed;

    [SerializeField] private float _power;

    public float Power => _power;
}
