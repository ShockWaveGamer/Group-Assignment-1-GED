using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable] public class Achievement
{
    public bool completed = false;
    public string achievementName = "Achievement";
    public string particleType = "Particle";
    public int requiredAmount = 0;

    /*
    public Achievement(bool _completed, string _achievementName, string _particleType, int _requiredAmount)
    {
        completed = completed;
        achievementName = achievementName;
        particleType = particleType;
        requiredAmount = requiredAmount;
    }*/
}


public class AchievementManager : MonoBehaviour
{
    [SerializeField] public List<Achievement> achievements = new List<Achievement>();
    public Dictionary<string, int> particleAmounts = new Dictionary<string, int>();
    public GameObject AchievementPopup;

    public void AddParticle(string particleType)
    {
        if (particleAmounts.ContainsKey(particleType))
        {
            particleAmounts[particleType]++;
        }
        else
        {
            particleAmounts.Add(particleType, 1);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Achievement achievement in achievements)
        {
            if (achievement.completed == false)
            {
                if (particleAmounts.ContainsKey(achievement.particleType))
                {
                    if (particleAmounts[achievement.particleType] >= achievement.requiredAmount)
                    {
                        achievement.completed = true;
                        //Debug.Log($"Achievement {achievement.achievementName} completed!");
                        GameObject AP = Instantiate(AchievementPopup, FindObjectOfType<Canvas>().transform);
                        AP.transform.GetChild(0).GetComponent<TMP_Text>().text = achievement.achievementName;
                    }
                }
            }
        }
    }
}
