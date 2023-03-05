using UnityEngine;

public class Player : StaticInstance<Player>
{
    private void Start()
    {
        MoveToSpawnPoint();
    }

    public void MoveToSpawnPoint()
    {
        if(SpawnPoint.Instance is null)
        {
            Debug.LogError("SpawnPoint does not exsist");
            return;
        }

        transform.position = SpawnPoint.Instance.transform.position;
    }
}
