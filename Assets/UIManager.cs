using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [Header("FeverBar")]
    [SerializeField] private Image _feverProgressBar;
    private float movedDistance;
    private float goalDistance = 2f;

    [Header("LevelProgressBar")]
    [SerializeField] private Image _levelProgressBar;
    private int _startingChildCount;

    
    
    private GameObject _shapeSpawner;

    private void Start()
    {
        
        _shapeSpawner = GameObject.FindGameObjectWithTag("ShapeSpawner");
        _startingChildCount = _shapeSpawner.GetComponent<Transform>().childCount;
    }

    private void Update()
    {
        FeverModeProgress();
        LevelProgress();
    }

    private void LevelProgress()
    {
        var currentChildCount = _shapeSpawner.transform.childCount;
        var offset = _startingChildCount - (currentChildCount-0.1f);
        _levelProgressBar.fillAmount = offset / _startingChildCount;
    }
    
    private void FeverModeProgress()
    {
        
        if (!Ball.isFever)
        {
            _feverProgressBar.material.color = Color.white;
            if (Ball.isMoving)
            {
                if (movedDistance < goalDistance)
                {
                    movedDistance += Time.deltaTime;
                    movedDistance = Mathf.Clamp(movedDistance, 0, 2.1f);
                    Mathf.Clamp(movedDistance, 0, 2.1f);
                    _feverProgressBar.fillAmount = movedDistance / goalDistance;
                }
                else
                {
                    Ball.isFever = true;
                }
            }
            else
            {
                movedDistance -= Time.deltaTime;
                movedDistance = Mathf.Clamp(movedDistance, 0, 2.1f);
                _feverProgressBar.fillAmount = movedDistance / goalDistance;
            }
            
        }
        if(Ball.isFever)
        {
            Ball._fireEffect.Play();
            _feverProgressBar.material.color = Color.red;
            if (movedDistance > 0.1f)
            {
                Ball.moveSpeed = 10;
                movedDistance -= Time.deltaTime;
                movedDistance = Mathf.Clamp(movedDistance, 0, 2.1f);
                _feverProgressBar.fillAmount = movedDistance / goalDistance;
            }
            else
            {
                Ball._fireEffect.Stop();
                Ball.isFever = false;
                Ball.moveSpeed = 5;
            }
            
        }
        
    }
}
