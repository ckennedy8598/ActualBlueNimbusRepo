using UnityEngine;
using UnityEngine.SceneManagement;

public class NEW_GameManager : MonoBehaviour
{
    public static NEW_GameManager Instance { get; private set; }

    [Header("Checkpoint Information")]
    [SerializeField] private Vector2 checkpointPosition;
    [SerializeField] private string checkpointScene;
    [SerializeField] private bool hasCheckpoint = false;

    [Header("Scene Transition Information")]
    [SerializeField] private string pendingSpawnID = "";

    private bool respawningAtCheckpoint = false;

    private void Awake()
    {
        // Make sure only one NEW_GameManager exists.
        if (Instance == null)
        {
            Instance = this;

            // Keep this GameObject alive when changing scenes.
            DontDestroyOnLoad(gameObject);

            // Listen for scene changes.
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            // Destroy duplicate GameManagers.
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // When the game first begins, use the Player's normal
        // starting position as the first checkpoint.
        GameObject player = FindPlayer();

        if (player != null)
        {
            SetCheckpoint(
                (Vector2)player.transform.position,
                SceneManager.GetActiveScene().name
            );

            BindRetryButton(player);
        }
    }

    private void OnDestroy()
    {
        // Only unsubscribe if this is the real persistent GameManager.
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    // =========================================================
    // SCENE TRANSITIONS
    // =========================================================

    public void LoadSceneAtSpawn(string sceneName, string spawnID)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("NEW_GameManager: No target scene was provided.");
            return;
        }

        if (string.IsNullOrEmpty(spawnID))
        {
            Debug.LogError("NEW_GameManager: No target Spawn ID was provided.");
            return;
        }

        // Remember which SpawnPoint we want in the next scene.
        pendingSpawnID = spawnID;

        // We are changing levels, not respawning from death.
        respawningAtCheckpoint = false;

        Debug.Log(
            "NEW_GameManager: Loading scene " +
            sceneName +
            " at Spawn ID " +
            spawnID
        );

        SceneManager.LoadScene(sceneName);
    }

    // =========================================================
    // CHECKPOINTS
    // =========================================================

    public void SetCheckpoint(Vector2 position)
    {
        SetCheckpoint(
            position,
            SceneManager.GetActiveScene().name
        );
    }

    private void SetCheckpoint(Vector2 position, string sceneName)
    {
        checkpointPosition = position;
        checkpointScene = sceneName;
        hasCheckpoint = true;

        Debug.Log(
            "NEW_GameManager: Checkpoint saved at " +
            checkpointPosition +
            " in scene " +
            checkpointScene
        );
    }

    // =========================================================
    // RETRY / DEATH
    // =========================================================

    public void RetryFromCheckpoint()
    {
        Time.timeScale = 1f;

        // Stop any pending normal level transition.
        pendingSpawnID = "";

        if (!hasCheckpoint)
        {
            Debug.LogWarning(
                "NEW_GameManager: No checkpoint exists. Reloading current scene."
            );

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name
            );

            return;
        }

        respawningAtCheckpoint = true;

        Debug.Log(
            "NEW_GameManager: Retrying from checkpoint in scene " +
            checkpointScene +
            " at " +
            checkpointPosition
        );

        // Reloading the scene recreates the Player that
        // PlayerCombat.Die() destroyed.
        SceneManager.LoadScene(checkpointScene);
    }

    // =========================================================
    // WHEN A SCENE FINISHES LOADING
    // =========================================================

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = FindPlayer();

        // Some scenes, such as a main menu, may have no Player.
        if (player == null)
        {
            Debug.Log(
                "NEW_GameManager: Scene " +
                scene.name +
                " contains no active Player."
            );

            return;
        }

        // -----------------------------------------------------
        // CASE 1:
        // Player died and pressed Retry.
        // -----------------------------------------------------

        if (respawningAtCheckpoint)
        {
            MovePlayer(player, checkpointPosition);

            respawningAtCheckpoint = false;

            BindRetryButton(player);

            Debug.Log(
                "NEW_GameManager: Player respawned at checkpoint " +
                checkpointPosition
            );

            return;
        }

        // -----------------------------------------------------
        // CASE 2:
        // Player just entered a new level through a transition.
        // -----------------------------------------------------

        if (!string.IsNullOrEmpty(pendingSpawnID))
        {
            NEW_SpawnPoint spawnPoint =
                FindSpawnPoint(pendingSpawnID);

            if (spawnPoint != null)
            {
                Vector2 spawnPosition =
                    (Vector2)spawnPoint.transform.position;

                MovePlayer(player, spawnPosition);

                // The entrance to the new level automatically
                // becomes the baseline checkpoint.
                SetCheckpoint(
                    spawnPosition,
                    scene.name
                );

                Debug.Log(
                    "NEW_GameManager: Player entered " +
                    scene.name +
                    " at Spawn ID " +
                    pendingSpawnID
                );
            }
            else
            {
                Debug.LogError(
                    "NEW_GameManager: Could not find SpawnPoint with ID '" +
                    pendingSpawnID +
                    "' in scene '" +
                    scene.name +
                    "'."
                );

                // Use the Player's normal scene position as a
                // fallback so the checkpoint system still works.
                SetCheckpoint(
                    (Vector2)player.transform.position,
                    scene.name
                );
            }

            pendingSpawnID = "";

            BindRetryButton(player);

            return;
        }

        // -----------------------------------------------------
        // CASE 3:
        // Scene was loaded normally.
        // For example, starting a level directly in the Editor.
        // -----------------------------------------------------

        SetCheckpoint(
            (Vector2)player.transform.position,
            scene.name
        );

        BindRetryButton(player);
    }

    // =========================================================
    // FIND PLAYER
    // =========================================================

    private GameObject FindPlayer()
    {
        GameObject player = null;

        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        catch
        {
            Debug.LogError(
                "NEW_GameManager: The project needs a tag named 'Player'."
            );
        }

        return player;
    }

    // =========================================================
    // FIND SPAWN POINT
    // =========================================================

    private NEW_SpawnPoint FindSpawnPoint(string spawnID)
    {
        NEW_SpawnPoint[] spawnPoints =
            FindObjectsOfType<NEW_SpawnPoint>();

        foreach (NEW_SpawnPoint spawnPoint in spawnPoints)
        {
            if (spawnPoint.SpawnID == spawnID)
            {
                return spawnPoint;
            }
        }

        return null;
    }

    // =========================================================
    // MOVE PLAYER
    // =========================================================

    private void MovePlayer(GameObject player, Vector2 position)
    {
        Rigidbody2D rb =
            player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Move through the Rigidbody when the Player has one.
            rb.position = position;

            // Prevent any old movement from carrying through.
            rb.velocity = Vector2.zero;
        }
        else
        {
            // Preserve the Player's Z coordinate.
            player.transform.position =
                new Vector3(
                    position.x,
                    position.y,
                    player.transform.position.z
                );
        }
    }

    // =========================================================
    // EXISTING PLAYERCOMBAT RETRY BUTTON
    // =========================================================

    private void BindRetryButton(GameObject player)
    {
        PlayerCombat playerCombat =
            player.GetComponent<PlayerCombat>();

        if (playerCombat == null)
        {
            Debug.LogWarning(
                "NEW_GameManager: Player does not have PlayerCombat."
            );

            return;
        }

        if (playerCombat.retry == null)
        {
            Debug.LogWarning(
                "NEW_GameManager: PlayerCombat has no Retry button assigned."
            );

            return;
        }

        // Prevent this new listener from being added more than once.
        playerCombat.retry.onClick.RemoveListener(
            RetryFromCheckpoint
        );

        playerCombat.retry.onClick.AddListener(
            RetryFromCheckpoint
        );

        Debug.Log(
            "NEW_GameManager: Retry button connected."
        );
    }
}