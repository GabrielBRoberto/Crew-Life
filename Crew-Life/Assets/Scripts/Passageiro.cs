using UnityEngine.UI;
using UnityEngine;

namespace gNox.Tipos
{
    public class Passageiro : MonoBehaviour
    {
        [Header("Numero Minimo de Moedas Geradas")]
        [SerializeField]
        private int valorMinimoMoeda;
        public int ValorMinimoMoeda
        {
            get { return valorMinimoMoeda; }
            set { valorMinimoMoeda = value; }
        }

        [Header("Numero Maximo de Moedas Geradas")]
        [SerializeField]
        private int valorMaximoMoeda;
        public int ValorMaximoMoeda
        {
            get { return valorMaximoMoeda; }
            set { valorMaximoMoeda = value; }
        }

        [Header("Valor Inicial do Timer")]
        [SerializeField]
        private float valorInicialTimer;
        public float ValorInicialTimer
        {
            get { return valorInicialTimer; }
            set { valorInicialTimer = value; }
        }

        [Header("Tempo Impaciente")]
        [SerializeField]
        private float impacienciaTimer;
        public float ImpacienciaTimer
        {
            get { return impacienciaTimer; }
            set { impacienciaTimer = value; }
        }

        [Header("Valor Minimo de Ficar Impaciente")]
        [SerializeField]
        private float impacienciaValorMinimo;
        public float ImpacienciaValorMinimo
        {
            get { return impacienciaValorMinimo; }
            set { impacienciaValorMinimo = value; }
        }

        [Header("Valor Maximo de Ficar Impaciente")]
        [SerializeField]
        private float impacienciaValorMaximo;
        public float ImpacienciaValorMaximo
        {
            get { return impacienciaValorMaximo; }
            set { impacienciaValorMaximo = value; }
        }

        [Header("Esta acordado??")]
        [SerializeField]
        private bool estaAcordado;
        public bool EstaAcordado
        {
            get { return estaAcordado; }
            set { estaAcordado = value; }
        }

        [Header("Problema esta sendo resolvido")]
        [SerializeField]
        private bool sendoResolvido;
        public bool SendoResolvido
        {
            get { return sendoResolvido; }
            set { sendoResolvido = value; }
        }

        [Header("Esta com problema")]
        [SerializeField]
        private bool estaComProblema;
        public bool EstaComProblema
        {
            get { return estaComProblema; }
            set { estaComProblema = value; }
        }

        [Header("Slider da Paciencia")]
        [SerializeField]
        private Slider pacienciaSlider;
        public Slider PacienciaSlider
        {
            get { return pacienciaSlider; }
            set { pacienciaSlider = value; }
        }

        private void Awake()
        {
            valorMinimoMoeda = Random.Range(1, 50);
            valorMaximoMoeda = Random.Range(50, 150);

            impacienciaValorMinimo = Random.Range(25, 50);
            impacienciaValorMaximo = Random.Range(0, 25);
            impacienciaTimer = Random.Range(75, 150);

            valorInicialTimer = impacienciaTimer;

            PacienciaSlider.minValue = ImpacienciaValorMaximo;
            PacienciaSlider.maxValue = ImpacienciaValorMinimo;
            
            float estaAcordadoRandom;
            estaAcordadoRandom = Random.Range(0, 100);

            if (estaAcordadoRandom <= 50)
            {
                estaAcordado = true;
                gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            else
            {
                estaAcordado = false;
                gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
            }
        }
        private void FixedUpdate()
        {
            if (estaAcordado)
            {
                if (ImpacienciaTimer > ImpacienciaValorMinimo)
                {
                    gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                }
                else
                {
                    PacienciaSlider.value = ImpacienciaTimer;

                    if (ImpacienciaTimer < ImpacienciaValorMinimo)
                    {
                        gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    }
                    if (ImpacienciaTimer < (ImpacienciaValorMaximo * 1.60))
                    {
                        gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    }
                    if (ImpacienciaTimer < (ImpacienciaValorMaximo * 1.30))
                    {
                        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    }
                }
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
            }
        }

        public void ClicaBotaoResolveProblema()
        {
            if (!FindObjectOfType<GameManager>().trigged)
            {
                StartCoroutine(FindObjectOfType<GameManager>().TripulanteVaiAoPassageiro(this.gameObject));
            }
        }
    }
}
