using UnityEngine;

public class MasterSoundFXScript : MonoBehaviour
{
    public AudioClip Gun;
    public AudioClip Walk;
    public AudioClip Roll;
    public static MasterSoundFXScript Instance { get; set; }
    private AudioSource audioSource;
    private float LastTick;
    public float Volume;
    public float time;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        time++;
    }
    public void PlayFX(int chosen)
    {
        audioSource.volume = Volume;
        if(chosen == 1)
        {
            
            audioSource.PlayOneShot(Gun);
        }
        if(chosen == 2)
        {
            
            audioSource.PlayOneShot(Roll);
        }
        if(chosen == 3)
        {
            if (time > 15)
            {
                audioSource.PlayOneShot(Walk);
                time = 0;
            }
            
            
        }
    }
}
