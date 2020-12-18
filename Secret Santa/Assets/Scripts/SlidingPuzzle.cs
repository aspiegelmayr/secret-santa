using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidingPuzzle : MonoBehaviour
{
    public PuzzleTile tile;

    private List<PuzzleTile> puzzleTiles = new List<PuzzleTile>();

    private Vector2 startPosition = new Vector2(-3.0f, 4.1f);

    private Vector2 offset = new Vector2(3.0f, 3.0f);

    public LayerMask collisionMask;

    public Texture[] images;

    Ray

            rayUp,
            rayDown,
            rayLeft,
            rayRight;

    RaycastHit hit;

    private BoxCollider collider;

    private Vector3 colliderSize;

    private Vector3 colliderCenter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
