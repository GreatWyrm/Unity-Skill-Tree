using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStarfieldScript : MonoBehaviour
{
    [SerializeField]
    private int totalStars;
    [SerializeField]
    private float starSize;
    [SerializeField]
    private Vector2 bounds;
    [SerializeField]
    private int seed;
    [SerializeField]
    private float parallax;

    private ParticleSystem starSystem;
    private ParticleSystem.EmitParams[] stars;

    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Awake()
    {
        starSystem = GetComponent<ParticleSystem>();
        
        stars = new ParticleSystem.EmitParams[totalStars];

        Random.seed = seed;
        for (int i = 0; i < totalStars; i++)
        {
            Vector3 starPos = new Vector3(Random.Range(0f, bounds.x) + transform.position.x, Random.Range(0f, bounds.y) + transform.position.y, transform.position.z);
            stars[i].position = starPos;
            stars[i].startSize = starSize;
            stars[i].startLifetime = Mathf.Infinity;
            starSystem.Emit(stars[i], 1);
        }
    }
    private void FixedUpdate()
    {
        Transform camPos = Camera.main.transform;
        transform.position = new Vector3(startingPosition.x + camPos.position.x * parallax, startingPosition.y + camPos.position.y * parallax, 0);
    }
}