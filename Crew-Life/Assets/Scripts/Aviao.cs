using UnityEngine;

namespace gNox.Tipos
{
    public class Aviao : MonoBehaviour
    {
        [Header("Numero Minimo de Passageiros Acordados")]
        [SerializeField]
        private int valorMinimoPassageiro;
        public int ValorMinimoPassageiro
        {
            get { return valorMinimoPassageiro; }
            set { valorMinimoPassageiro = value; }
        }

        [Header("Numero Maximo de Passageiros Acordados/No Avi�o")]
        [SerializeField]
        private int valorMaximoPassageiro;
        public int ValorMaximoPassageiro
        {
            get { return valorMaximoPassageiro; }
            set { valorMaximoPassageiro = value; }
        }

        [Header("Numero Minimo de Tripulantes")]
        [SerializeField]
        private int valorMinimoTripulantes;
        public int ValorMinimoTripulantes
        {
            get { return valorMinimoTripulantes; }
            set { valorMinimoTripulantes = value; }
        }

        [Header("Numero Maximo de Tripulantes")]
        [SerializeField]
        private int valorMaximoTripulantes;
        public int ValorMaximoTripulantes
        {
            get { return valorMaximoTripulantes; }
            set { valorMaximoTripulantes = value; }
        }

        [Header("Estrurura do Avi�o")]
        [SerializeField]
        private GameObject estruturaAviao;
        public GameObject EstruturaAviao
        {
            get { return estruturaAviao; }
            set { estruturaAviao = value; }
        }
    }
}