using UnityEngine;

namespace gNox.Tipos
{

    [CreateAssetMenu(fileName = "Passageiro", menuName = "Tipos/Passageiro", order = 3)]
    [System.Serializable]
    public class Passageiro : ScriptableObject
    {
        [Header("Numero Minimo de Moedas Geradas")]
        public int valorMinimoMoeda;
        [Header("Numero Maximo de Moedas Geradas")]
        public int valorMaximoMoeda;
        [Header("Tempo Impaciente")]
        public float impacienciaTimer;
        [Header("Valor Minimo de Ficar Impaciente")]
        public float impacienciaValorMinimo;
        [Header("Valor Maximo de Ficar Impaciente")]
        public float impacienciaValorMaximo;
        [Header("Objeto 3D")]
        public GameObject objeto3D;
        [Header("Esta acordado??")]
        public bool estaAcordado;
    }
}
