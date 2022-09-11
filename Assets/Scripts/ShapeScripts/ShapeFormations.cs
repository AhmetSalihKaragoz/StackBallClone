using UnityEngine;

namespace ShapeScripts
{
    public class ShapeFormations : MonoBehaviour
    {
        public static int RandomSpawner(int num)
        {
            var randomNum = Random.Range(0, 101);
            int index;
            if (randomNum <=30) 
            {
                index = OddFormation(num);
            } 
            else if (randomNum <= 60)
            {
                index = TwoPieceFormation(num);
            }
            else if(randomNum<=90)
            {
                index = FivePieceFormation(num);
            }
            else
            {
                index = SamePieceFormation();
            }

            return index;
        }
        private static int OddFormation(int num)
        {
            int index;
            index = num % 2 == 0 ? 0 : 3;
            return index;
        }
        private static int TwoPieceFormation(int num)
        {
            int index;
            index = num % 4 == 2 ? 1 : 2;

            return index;
        }
        private static int FivePieceFormation(int num)
        {
            int index;
            index = num <= 5 ? 2 : 3;

            return index;
        }
        private static int SamePieceFormation()
        {
            var index = Random.Range(0, 4);
            return index;
        }
    }
}
