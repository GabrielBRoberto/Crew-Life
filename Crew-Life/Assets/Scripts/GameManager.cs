using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;
using gNox.Tipos;

public class GameManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField]
    private GameObject inGameCanvas;
    [SerializeField]
    private GameObject storeCanvas;
    [SerializeField]
    private GameObject tripulacaoCanvas;
    [SerializeField]
    private GameObject settingsCanvas;

    private Canvas canvas;

    [Header("Aviao, Tripulação e Passageiros")]
    [SerializeField]
    private Aviao aviao;
    [SerializeField]
    private GameObject[] tripulacaoGameObject;
    [SerializeField]
    private GameObject[] passageirosGameObject;

    [Header("Moedas")]
    [SerializeField]
    private int moedaGratis;        //Definir nome.
    [SerializeField]
    private int moedaPaga;          //Definir nome.

    public bool trigged = false;

    #region Mudança de HUD

    public void openStore()
    {
        tripulacaoCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        storeCanvas.SetActive(true);
    }
    public void openTripulacao()
    {
        settingsCanvas.SetActive(false);
        storeCanvas.SetActive(false);
        tripulacaoCanvas.SetActive(true);
    }
    public void openSettings()
    {
        tripulacaoCanvas.SetActive(false);
        storeCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }
    public void backToGame()
    {
        tripulacaoCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        storeCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
    }

    #endregion

    private void Awake()
    {
        tripulacaoGameObject = GameObject.FindGameObjectsWithTag("Tripulante");
        passageirosGameObject = GameObject.FindGameObjectsWithTag("Passageiro");

        aviao = FindObjectOfType<Aviao>();
        aviao.ValorMaximoPassageiro = passageirosGameObject.Length;

        for (int i = 0; i < passageirosGameObject.Length; i++)
        {
            if (passageirosGameObject[i].GetComponent<Passageiro>().EstaAcordado)
            {
                aviao.ValorMinimoPassageiro++;
            }
            else
            {
                aviao.ValorMaximoTripulantes--;
            }

            passageirosGameObject[i].transform.Find("Canvas").gameObject.SetActive(false);
        }

        canvas = FindObjectOfType<Canvas>();

        backToGame();

        canvas.transform.Find("InGame").Find("NumeroPassageiros").Find("Panel").Find("PassageirosText").GetComponent<Text>().text = aviao.ValorMinimoPassageiro + " / " + aviao.ValorMaximoPassageiro;
    }

    private void FixedUpdate()
    {
        AtualizaBotaoResolverProblema();
        AtualizaHUDGeral();
    }

    void AtualizaBotaoResolverProblema()
    {
        for (int i = 0; i < passageirosGameObject.Length; i++)
        {
            if (passageirosGameObject[i].GetComponent<Passageiro>().EstaAcordado)
            {
                passageirosGameObject[i].GetComponent<Passageiro>().ImpacienciaTimer -= Time.deltaTime;
                if (passageirosGameObject[i].GetComponent<Passageiro>().ImpacienciaValorMinimo >= passageirosGameObject[i].GetComponent<Passageiro>().ImpacienciaTimer)
                {
                    if (passageirosGameObject[i].GetComponent<Passageiro>().ImpacienciaTimer <= passageirosGameObject[i].GetComponent<Passageiro>().ImpacienciaValorMaximo)
                    {
                        passageirosGameObject[i].GetComponent<Passageiro>().ImpacienciaTimer = passageirosGameObject[i].GetComponent<Passageiro>().ImpacienciaValorMaximo;
                    }

                    passageirosGameObject[i].GetComponent<Passageiro>().EstaComProblema = true;

                    //Ativa o botao pra ir resolver o problema.
                    passageirosGameObject[i].transform.Find("Canvas").gameObject.SetActive(true);
                }
                else
                {
                    passageirosGameObject[i].GetComponent<Passageiro>().EstaComProblema = false;
                    passageirosGameObject[i].transform.Find("Canvas").gameObject.SetActive(false);
                }
            }
            else
            {
                passageirosGameObject[i].GetComponent<Passageiro>().ImpacienciaTimer = passageirosGameObject[i].GetComponent<Passageiro>().ValorInicialTimer;
            }
        }
    }
    void AtualizaHUDGeral()
    {
        for (int i = 0; i < passageirosGameObject.Length; i++)
        {
            if (passageirosGameObject[i].GetComponent<Passageiro>().EstaAcordado)
            {
                aviao.ValorMinimoPassageiro++;
            }
            else
            {
                aviao.ValorMaximoTripulantes--;
            }

            passageirosGameObject[i].transform.Find("Canvas").gameObject.SetActive(false);
        }

        canvas.transform.Find("InGame").Find("AreaMoedas").Find("MoedaBasica").Find("Text").GetComponent<Text>().text = moedaGratis.ToString();
        canvas.transform.Find("InGame").Find("AreaMoedas").Find("MoedaPaga").Find("Text").GetComponent<Text>().text = moedaPaga.ToString();
    }

    public GameObject FindClosestTripulante(GameObject passageiro)
    {
        if (passageiro.GetComponent<Passageiro>().EstaComProblema)
        {
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = passageiro.transform.position;
            foreach (GameObject go in tripulacaoGameObject)
            {
                if (go.GetComponent<Tripulação>().EstaResolvendoAlgo)
                {
                    continue;
                }

                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }

            return closest;
        }
        else
        {
            return null;
        }
    }

    IEnumerator TimerCoroutine()
    {
        Debug.Log("TimerCoroutine Trigged");

        yield return new WaitForSeconds(1);

        trigged = false;

        Debug.Log("TimerCoroutine Ended");
    }
    public IEnumerator TripulanteVaiAoPassageiro(GameObject passageiro)
    {
        trigged = true;

        GameObject tripulanteProximo = FindClosestTripulante(passageiro);

        if (!tripulanteProximo.GetComponent<Tripulação>().EstaResolvendoAlgo)
        {
            tripulanteProximo.GetComponent<NavMeshAgent>().SetDestination(passageiro.transform.position);
            tripulanteProximo.GetComponent<Tripulação>().EstaResolvendoAlgo = true;

            StartCoroutine(TimerCoroutine());


            StartCoroutine(TripulanteChegouNoPassageiro(tripulanteProximo, passageiro));

            yield return new WaitForSeconds(5);
        }
    }
    IEnumerator TripulanteChegouNoPassageiro(GameObject tripulante, GameObject passageiro)
    {
        Debug.Log(tripulante.GetComponent<NavMeshAgent>().remainingDistance);

        if (tripulante.GetComponent<NavMeshAgent>().remainingDistance <= 1f)
        {
            Debug.Log("Tripulante Chegou");

            yield break;
        }
    }
}
