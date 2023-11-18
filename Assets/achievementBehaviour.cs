using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class achievementBehaviour : MonoBehaviour
{
    public float TimeToAppearFor = 3.0f;
    public AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        audioSource.Play();

        StartCoroutine(AppearFor(TimeToAppearFor));
    }

    IEnumerator AppearFor(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
