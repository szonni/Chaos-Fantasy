using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip menuMusic;
    public AudioClip gameplayMusic;
    public AudioClip atkMusic;
    public AudioClip deathMusic;
    public AudioClip hitenemyMusic;
    private void Awake()
    {
        // Đảm bảo AudioManager không bị phá hủy khi chuyển scene
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        // Đăng ký sự kiện sceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện sceneLoaded
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Kiểm tra tên scene để phát nhạc phù hợp
        if (scene.name == "Menu") // Thay "Menu" bằng tên scene Menu của bạn
        {
            PlayMenuMusic();
        }
        else if (scene.name == "Gameplay") // Thay "Gameplay" bằng tên scene Gameplay của bạn
        {
            PlayGameplayMusic();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMenuMusic()
    {
        if (musicSource.clip != menuMusic)
        {
            musicSource.Stop();
            musicSource.clip = menuMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayGameplayMusic()
    {
        if (musicSource.clip != gameplayMusic)
        {
            musicSource.Stop();
            musicSource.clip = gameplayMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
