using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Die()
    {
        QuestManager.Instance.AddProgress();

        Destroy(gameObject);
    }
}