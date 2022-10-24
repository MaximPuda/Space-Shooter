using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Level", menuName = "New Level", order = 51)]
public class Level : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Image _image;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private float _enemiesSpeed;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private float _timeToComplete;

    public string Name => _name;
    public Image Image => _image;
    public Enemy[] Enemies => _enemies;
    public float EnemiesSpeed => _enemiesSpeed;
    public float TimeBetweenSpawn => _timeBetweenSpawn;
    public float TimeToComplete => _timeToComplete;
    public LevelStatus Status { get; set; }
}
