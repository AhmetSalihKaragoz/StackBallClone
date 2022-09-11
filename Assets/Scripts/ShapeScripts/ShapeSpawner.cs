using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

namespace ShapeScripts
{
    public class ShapeSpawner : MonoBehaviour
    {
        public ShapeLists _shapeLists;

        [SerializeField] private GameObject _pole;
        [SerializeField] private Material _shapeMaterial;
        private List<List<GameObject>> _permShapeLists = new List<List<GameObject>>();
        private List<List<GameObject>> _tempShapeLists = new List<List<GameObject>>();
        private List<GameObject> _currentShapeList = new List<GameObject>();


        private float heightGap = 0.5f;
        private float _yPos;
        private float _yRot;
        
        private int _startingChildCount;

        private void Awake()
        {
            AddShapeLists();
            SetCurrentShapeList();
            Spawn();
        }

        private void Start()
        {
            _startingChildCount = transform.childCount;
            SpawnPole();
        }

        private void Update()
        {
            SetShapeColor();
        }

        private void AddShapeLists()
        {
            _permShapeLists.Add(_shapeLists.circleList);
            _permShapeLists.Add(_shapeLists.flowerList);
            _permShapeLists.Add(_shapeLists.hexList);
            _permShapeLists.Add(_shapeLists.spikeList);
            _permShapeLists.Add(_shapeLists.squareList);
        }
        private void SetCurrentShapeList()
        {
            if (_tempShapeLists.Count == 0)
            {
                FillTempList();
            }
            if (PlayerPrefs.GetInt("level", LevelManager.Instance.level) == 1)
            {
                _currentShapeList = _tempShapeLists[0];
                _tempShapeLists.Remove(_tempShapeLists[0]);
            }
            else if (PlayerPrefs.GetInt("level", LevelManager.Instance.level) == 2)
            {
                _currentShapeList = _tempShapeLists[1];
                _tempShapeLists.Remove(_tempShapeLists[1]);
            }
            else if (PlayerPrefs.GetInt("level", LevelManager.Instance.level) == 3)
            {
                _currentShapeList = _tempShapeLists[2];
                _tempShapeLists.Remove(_tempShapeLists[2]);
            }
            else if (PlayerPrefs.GetInt("level", LevelManager.Instance.level) == 4)
            {
                _currentShapeList = _tempShapeLists[3];
                _tempShapeLists.Remove(_tempShapeLists[3]);
            }
            else if (PlayerPrefs.GetInt("level", LevelManager.Instance.level) == 5)
            {
                _currentShapeList = _tempShapeLists[4];
                _tempShapeLists.Remove(_tempShapeLists[4]);
            }
            else
            {
                SetRandomShape();
            }
        }
        private void Spawn()
        {
            for (int i = 0; i < 100; i += 10)
            {
                for (int j = 0; j < 10; j++)
                {
                    var shapeRotation = Quaternion.Euler(new Vector3(0, _yRot, 0));
                    var shapePosition = new Vector3(0, _yPos, 0);
                    Instantiate(_currentShapeList[ShapeFormations.RandomSpawner(j)], shapePosition, shapeRotation,
                        transform);

                    _yPos += heightGap;
                    _yRot += 2.5f;
                }
            }
        }
        private void FillTempList()
        {
            foreach (var shapeList in _permShapeLists)
            {
                _tempShapeLists.Add(shapeList);
            }
        }
        private void SetRandomShape()
        {
            int index = Random.Range(0, 5);
            _currentShapeList = _tempShapeLists[index];
            _tempShapeLists.Remove(_tempShapeLists[index]);
        }
        private void SetShapeColor()
        {
            if (transform.childCount >= _startingChildCount / 3 * 2)
            {
                _shapeMaterial.color = ColorManager.CurrentColorList[0];
            }
            if (transform.childCount >= _startingChildCount / 3 && transform.childCount < _startingChildCount / 3 * 2)
            {
                _shapeMaterial.color = ColorManager.CurrentColorList[1];
            }
            if(transform.childCount <= _startingChildCount/3)
            {
                _shapeMaterial.color = ColorManager.CurrentColorList[2];
            }
        }

        private void SpawnPole()
        {
           var pole = Instantiate(_pole, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
           pole.transform.localScale = new Vector3(0.8f, (_startingChildCount * 0.25f + 0.6f), 0.8f);

        }


    }
}
