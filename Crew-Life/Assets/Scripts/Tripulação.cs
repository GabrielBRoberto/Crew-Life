using UnityEngine;

namespace gNox.Tipos
{

    [CreateAssetMenu(fileName = "Tripulação", menuName = "Tipos/Tripulação", order = 2)]
    [System.Serializable]
    public class Tripulação : ScriptableObject
    {
        [Header("Velocidade de Movimento")]
        public int velocidadeMovimento;
        [Header("Velocidade da Resolução de Problemas")]
        public int velocidadeProblema;
        [Header("Paciencia Maxima")]
        public float Paciencia;
        [Header("Objeto 3D")]
        public GameObject objeto3D;
        [Header("Card do Tripulante")]
        public Sprite cardTripulante;
        [Header("Esta Resolvendo Algo??")]
        public bool estaResolvendoAlgo;

    }
}
