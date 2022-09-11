using System.Collections.Generic;
using UnityEngine;

namespace ShapeScripts
{
    [CreateAssetMenu(fileName = "ShapeLists", menuName = "shapeList")]
    public class ShapeLists : ScriptableObject
    {
        public List<GameObject> circleList;
        public List<GameObject> flowerList;
        public List<GameObject> hexList;
        public List<GameObject> spikeList;
        public List<GameObject> squareList;
    }
}
