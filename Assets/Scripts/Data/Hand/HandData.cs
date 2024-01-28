using UnityEngine;

[CreateAssetMenu(fileName = "HandData", menuName = "Data/HandData")]
public class HandData : ScriptableObject
{
    [SerializeField] private float _distanceBetweenCards = 20f;
    [SerializeField] private float _startAngle = 0f;
    [SerializeField] private float _radius = 5f;

    public float DistanceBetweenCard => _distanceBetweenCards;
    public float StartAngle => _startAngle;
    public float Radius => _radius;
}
