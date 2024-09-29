using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObject/Create BulletData")]
public class BulletData : ScriptableObject
{
    [SerializeField] private float _speed;

    public float Speed => _speed;

    [SerializeField] private float _curvePower;

    public float CurvePower => _curvePower;
}
