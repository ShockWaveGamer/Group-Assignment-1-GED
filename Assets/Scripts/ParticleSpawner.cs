using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject SandPrefab;
    public TMP_Text OutputText;
    private int numParticles;
    private enum ParticleType
    {
        Sand,
        Water,
        Gas,
        Lava,
        Stone
    }
    private void CreateParticle(ParticleType _type, Vector3 _spawnPos)
    {
        numParticles++;
        // todo implement type switching 
        var NewSpawnedParticle = Instantiate(SandPrefab, _spawnPos, Quaternion.identity);
        // todo add NewSpawnedParticle to pooling etc.
    }

    private void OnMouseDown()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OutputText.text = numParticles.ToString();
        if (Input.GetMouseButton(0))
        {
            Vector3 ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); ClickPos.z = 0;
            CreateParticle(ParticleType.Sand, ClickPos);
        }
    }
}
