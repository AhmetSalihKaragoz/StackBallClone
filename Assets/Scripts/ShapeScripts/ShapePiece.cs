using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShapeScripts
{
    public class ShapePiece : MonoBehaviour
    {
        private WholeShape _wholeShape;
        private GameObject _ShapeSpawner;

        private Transform myTf;
        private Vector3 endPos;
        private Tween _jumpTween;
        private Tween _rotationTween;

        private void Start()
        {
            myTf = gameObject.transform;
            _wholeShape = GetComponentInParent<WholeShape>();
            _ShapeSpawner = GameObject.FindGameObjectWithTag("ShapeSpawner");
        }
        public void Break()
        {
            
            Vector3 point = myTf.GetChild(0).transform.position;
            endPos = (point - myTf.position) * 2;
            endPos.y = _ShapeSpawner.transform.childCount * 0.5f;
            _jumpTween = transform.DOJump(endPos,3,1,1);
            _rotationTween =
                transform.DORotate(new Vector3(Random.Range(90, 359), Random.Range(90, 359), Random.Range(90, 359)),
                    0.5f);
        }

        public void StartDestroyChain()
        {
            _wholeShape.DestroyWholeShape();
        }

        private void OnDestroy()
        {
            _jumpTween.Kill(gameObject);
            _rotationTween.Kill(gameObject);
        }
        
    }
}
