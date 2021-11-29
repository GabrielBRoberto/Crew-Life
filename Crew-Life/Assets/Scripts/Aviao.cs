using UnityEngine;

namespace gNox.Tipos
{

    [CreateAssetMenu(fileName = "Avião", menuName = "Tipos/Avião", order = 0)]
    [System.Serializable]
    public class Aviao : ScriptableObject
    {
        [Header("Numero Minimo de Passageiros Acordados")]
        public int valoMinimoPassageiro;
        [Header("Numero Maximo de Passageiros Acordados")]
        public int valorMaximoPassageiro;
        [Header("Numero Minimo de Tripulantes")]
        public int valorMinimoTripulantes;
        [Header("Numero Maximo de Tripulantes")]
        public int valorMaximoTripulantes;
        [Header("Estrurura do Avião")]
        public GameObject estruturaAviao;
        [Header("Assentos")]
        public Assento[] assentos;
    }
}