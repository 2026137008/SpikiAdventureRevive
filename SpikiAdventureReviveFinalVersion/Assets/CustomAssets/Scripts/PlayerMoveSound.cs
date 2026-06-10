using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveSound : MonoBehaviour
{
    public AudioClip moveSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 방향키를 눌렀을 때
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame ||
            Keyboard.current.rightArrowKey.wasPressedThisFrame ||
            Keyboard.current.upArrowKey.wasPressedThisFrame ||
            Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            audioSource.Play();
        }

        // 방향키를 땠을 때
        if (Keyboard.current.leftArrowKey.wasReleasedThisFrame ||
            Keyboard.current.rightArrowKey.wasReleasedThisFrame ||
            Keyboard.current.upArrowKey.wasReleasedThisFrame ||
            Keyboard.current.downArrowKey.wasReleasedThisFrame)
        {
            audioSource.Stop();
        }
    }
}