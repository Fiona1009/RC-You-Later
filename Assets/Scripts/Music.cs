using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource audioSource;

    [Header("Clips")]
    public AudioClip musicMenus; // musique des deux scènes
    public AudioClip musicMain; // musique de la scène "spéciale"
    public AudioClip musicEnd; // musique de fin

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        // Charger la musique de la scène actuelle si on démarre ici
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip clipToPlay = null;
        float targetPitch = 1f; // pitch par défaut (oui parce que j'ai décidé de me compliquer la tâche et de mettre la musique en plus aigüe -_-

        // Les deux scènes qui partagent la même musique
        if (scene.name == "0_Menu" || scene.name == "1_Credits")
        {
            clipToPlay = musicMenus;
            targetPitch = 1f; // pitch normal
        }

        // La scène avec musique différente
        if (scene.name == "2_Main")
        {
            clipToPlay = musicMain;
            targetPitch = 1.1f; // pitch augmenté pour cette scène spécifique parce que banger
        }

        // La scène avec musique différente
        if (scene.name == "3_End")
        {
            clipToPlay = musicEnd;
            targetPitch = 1f; // pitch normal
        }
           

        // Si la musique est déjà la bonne, ne rien faire
        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.Stop();
            audioSource.clip = clipToPlay;
            audioSource.loop = true;
            audioSource.Play();
        }

        audioSource.pitch = targetPitch;
    }
}
