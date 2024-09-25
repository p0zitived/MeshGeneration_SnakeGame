using UnityEngine;

public class SnakeFood : MonoBehaviour
{
    [SerializeField] private GameObject _particleToSpawn;
    public int foodValue = 1;

    private void Update()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime, Space.World);
    }

    private void OnDestroy()
    {
        Instantiate(_particleToSpawn, transform.position + Vector3.up*1f, Quaternion.identity);
    }
}