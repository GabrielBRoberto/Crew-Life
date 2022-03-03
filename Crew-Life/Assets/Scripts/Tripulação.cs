using UnityEngine.AI;
using UnityEngine;
using ColorDebug;

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

        [Header("GameObject do Passageiro")]
        private GameObject passageiro;
        public GameObject Passageiro
        {
            get { return passageiro; }
            set { passageiro = value; }
        }

        private void Update()
        {
            GetComponent<NavMeshAgent>().speed = VelocidadeMovimento;

            if (EstaResolvendoAlgo)
            {
                if (Vector3.Distance(this.transform.position, this.GetComponent<NavMeshAgent>().destination) <= 7.58f)
                {
                    DebugX.Log($"{this.name}:green:b; chegou no ; {Passageiro.name}:green:b;");


                    //Comeca a resolucao do problema
                    if (FindObjectOfType<GameManager>().isTimer)
                    {
                        GameManager manager = FindObjectOfType<GameManager>();

                        if (manager.timerResolveProblemaAtual > 0)
                        {
                            manager.timerResolveProblemaAtual -= Time.deltaTime;
                        }
                        else
                        {
                            
                            //passageiro.GetComponent<Passageiro>().EstaComProblema = false;
                        }
                        
                    }
                    else
                    {

                    }
                }
                else
                {
                    float distancia = Vector3.Distance(this.transform.position, this.GetComponent<NavMeshAgent>().destination);
                    DebugX.Log($"{this.name}:yellow:b; esta ha uma distancia de ; {distancia}:red; do ; {Passageiro.name}:yellow:b;");
                }
            }
            else
            {
                Passageiro = null;
            }
        }
    }
}
