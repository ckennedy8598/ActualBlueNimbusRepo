using UnityEngine;

public class NEW_LevelTransition : MonoBehaviour
{
    [Header("Destination")]
    [SerializeField] private string targetSceneName;
    [SerializeField] private string targetSpawnID;

    private bool transitionStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transitionStarted)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (NEW_GameManager.Instance == null)
        {
            Debug.LogError(
                "NEW_LevelTransition: No NEW_GameManager exists."
            );

            return;
        }

        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError(
                "NEW_LevelTransition: Target Scene Name is empty."
            );

            return;
        }

        if (string.IsNullOrEmpty(targetSpawnID))
        {
            Debug.LogError(
                "NEW_LevelTransition: Target Spawn ID is empty."
            );

            return;
        }

        transitionStarted = true;

        Debug.Log(
            "NEW_LevelTransition: Player entered transition. " +
            "Loading " +
            targetSceneName +
            " at " +
            targetSpawnID
        );

        NEW_GameManager.Instance.LoadSceneAtSpawn(
            targetSceneName,
            targetSpawnID
        );
    }
}