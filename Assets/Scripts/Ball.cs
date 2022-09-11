using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using ShapeScripts;
using Random = UnityEngine.Random;


public class Ball : MonoBehaviour
{
    [Header("Ball")]
    public static bool isMoving;
    public static bool isFever;
    public static float moveSpeed = 5;
    [SerializeField] private float _bounceSpeed;
    [SerializeField] private float _jumpDuration;
    private static Vector3 ballPos = new Vector3(0, 0, -1.25f);
    
    [Header("Visual Effects")]
    [SerializeField] private GameObject _splashPrefab;
    [SerializeField] private Material _ballMaterial;
    [SerializeField] private ParticleSystem _dustEffect;
    public static ParticleSystem _fireEffect;


    [Header("Components")]
    private Rigidbody _rb;
    private Collider _col;
    private GameObject _shapeSpawner;
    private Tween _jumpTween;
    
    [Header("AudioClips")]
    [SerializeField] private AudioClip _onJumpSFX;
    [SerializeField] private AudioClip _normalBreakSFX;
    [SerializeField] private AudioClip _immortalBreakSFX;
    [SerializeField] private AudioClip _onDeathSFX;
    

    private void Start()
    {
        _fireEffect = gameObject.GetComponent<ParticleSystem>();
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
        _shapeSpawner = GameObject.FindGameObjectWithTag("ShapeSpawner");
        SetStartingBallPosition();
        SetSplashColor();
    }
    
    private void Update()
    {
        Move();
        ballPos.y = gameObject.transform.position.y;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isMoving) return;
        var splashPos = gameObject.transform.position - new Vector3(0, .1f, 0);
        var splashRot = Quaternion.Euler(new Vector3(90, 0, 0));
        var endPos = new Vector3(0, (collision.transform.position.y + 1), -1.25f);
        _jumpTween = gameObject.transform.DOJump(endPos,
            _bounceSpeed, 1,
            _jumpDuration).OnComplete(() =>
        {
           var splash = Instantiate(_splashPrefab,splashPos,splashRot,collision.transform);
           splash.transform.localScale =
               new Vector3(Random.Range(.15f, 0.35f), Random.Range(.15f, 0.25f), Random.Range(.15f, 0.25f));
           StartCoroutine(ClearSplash(splash));
           AudioSource.PlayClipAtPoint(_onJumpSFX,gameObject.transform.position);
           _dustEffect.Play();
        });
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isMoving) return;
        if (!isFever)
        {
            if (other.CompareTag("Piece"))
            {
                other.gameObject.GetComponent<ShapePiece>().StartDestroyChain(); 
                AudioSource.PlayClipAtPoint(_normalBreakSFX,gameObject.transform.position);
                ScoreKeeper.SetScore(1);
            }
            else if (other.CompareTag("enemy"))
            {
                var splashPos = gameObject.transform.position - new Vector3(0, -.06f, 0);
                var splashRot = Quaternion.Euler(new Vector3(90, 0, 0));
                var splash = Instantiate(_splashPrefab,splashPos,splashRot,other.transform);
                splash.transform.localScale =
                    new Vector3(0.25f, 0.25f,  0.25f);
                AudioSource.PlayClipAtPoint(_onDeathSFX,gameObject.transform.position);
                Destroy(gameObject);
            }
        }
        else
        {
            other.gameObject.GetComponent<ShapePiece>().StartDestroyChain();
            AudioSource.PlayClipAtPoint(_immortalBreakSFX,gameObject.transform.position);
            ScoreKeeper.SetScore(1);

        }
        
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.S))
        {
            isMoving = true;
            _jumpTween.Kill();
            _col.isTrigger = true;
            _rb.velocity = Vector3.down * moveSpeed;
        }
        else
        {
            _col.isTrigger = false;
            isMoving = false;
        }
    }
    private void SetStartingBallPosition()
    {
        ballPos.y = _shapeSpawner.transform.childCount * 0.5f + 0.75f;
        gameObject.transform.position = ballPos;
    }

    public static Vector3 GetBallPos()
    {
        return ballPos;
    }
    
    private void SetSplashColor()
    {
        _splashPrefab.GetComponent<SpriteRenderer>().color = _ballMaterial.color;
        var main = _dustEffect.main;
        main.startColor = _ballMaterial.color;
        
    }
    
    private  IEnumerator ClearSplash(GameObject splash)
    {
        yield return new WaitForSeconds(5f);
        Destroy(splash);
    }

}
