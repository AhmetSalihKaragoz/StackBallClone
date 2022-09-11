using System.Collections;
using UnityEngine;

namespace ShapeScripts
{
    public class WholeShape : MonoBehaviour
    {
        private ShapePiece[] shapePieces;

        private void Start()
        {
            shapePieces = GetComponentsInChildren<ShapePiece>();
        }

    
        public void DestroyWholeShape()
        {
            foreach (var shape in shapePieces)
            {
                shape.Break();
            }
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }

    }
}
