using UnityEngine.AI;
using UnityEngine;

namespace gNox.Tipos
{
    public class Tripulação : MonoBehaviour
    {
        [Header("Velocidade de Movimento")]
        [SerializeField]
        private int velocidadeMovimento;
        public int VelocidadeMovimento
        {
            get { return velocidadeMovimento; }
            set { velocidadeMovimento = value; }
        }

        [Header("Velocidade da Resolução de Problemas")]
        [SerializeField]
        private int velocidadeProblema;
        public int VelocidadeProblema
        {
            get { return velocidadeProblema; }
            set { velocidadeProblema = value; }
        }

        [Header("Paciencia Maxima")]
        [SerializeField]
        private float paciencia;
        public float Paciencia
        {
            get { return paciencia; }
            set { paciencia = value; }
        }

        [Header("Card do Tripulante")]
        [SerializeField]
        private Sprite cardTripulante;
        public Sprite CardTripulante
        {
            get { return cardTripulante; }
            set { cardTripulante = value; }
        }

        [Header("Esta Resolvendo Algo??")]
        [SerializeField]
        private bool estaResolvendoAlgo;
        public bool EstaResolvendoAlgo
        {
            get { return estaResolvendoAlgo; }
            set { estaResolvendoAlgo = value; }
        }

        [Header("Pode Resolver Algo??")]
        [SerializeField]
        private bool podeResolverAlgo;
        public bool PodeResolverAlgo
        {
            get { return podeResolverAlgo; }
            set { podeResolverAlgo = value; }
        }
        private void Update()
        {
            GetComponent<NavMeshAgent>().speed = velocidadeMovimento;
        }
    }
}
