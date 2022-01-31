using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;
using gNox.Tipos;
using ColorDebug;

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

    [Header("Triggers + Timers")]
    public bool trigged = false;
    bool adTrigged;
    bool canTriggerAd;
    bool velocidadeTriggerAdReward;
    bool impacienciaTriggerAdReward;
    bool resolucaoTriggerAdReward;
    public bool isTimer;
    [SerializeField]
    private float timerObjetivo;
    [SerializeField]
    private float timerAtual;
    [SerializeField]
    private float timerInicial;
    float timerRemoveAdEffect;

    [Header("Randoms")]
    [SerializeField]
    private int liberaAdRandom;
    [SerializeField]
    private int liberaAdPorcentagem;

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

        timerAtual = timerInicial;

        timerObjetivo = Random.Range(0, 100);

        if (timerAtual < timerObjetivo)
        {
            timerAtual = timerObjetivo * 2;
            timerInicial = timerAtual;
        }

        canTriggerAd = true;
    }
    private void FixedUpdate()
    {
        AtualizaBotaoResolverProblema();
        AtualizaHUDGeral();
        RemoveAdsEffect();
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
        }

        canvas.transform.Find("InGame").Find("AreaMoedas").Find("MoedaBasica").Find("Text").GetComponent<Text>().text = moedaGratis.ToString();
        canvas.transform.Find("InGame").Find("AreaMoedas").Find("MoedaPaga").Find("Text").GetComponent<Text>().text = moedaPaga.ToString();

        if (timerAtual < timerObjetivo)
        {
            liberaAdRandom = Random.Range(0, 100);
        }
        
        if (liberaAdRandom > liberaAdPorcentagem)
        {
            canvas.transform.Find("InGame").Find("PopUpAds").gameObject.SetActive(true);

            timerAtual = timerInicial;
        }
        else
        {
            canvas.transform.Find("InGame").Find("PopUpAds").gameObject.SetActive(false);

            if (canTriggerAd)
            {
                timerAtual -= Time.deltaTime;
            }
        }
    }
    void RemoveAdsEffect()
    {
        if (adTrigged)
        {
            timerRemoveAdEffect -= Time.deltaTime;

            if (timerRemoveAdEffect <= 0)
            {
                if (velocidadeTriggerAdReward)
                {
                    for (int i = 0; i < tripulacaoGameObject.Length; i++)
                    {
                        tripulacaoGameObject[i].GetComponent<Tripulação>().VelocidadeMovimento /= 2;
                    }
                }
                if (resolucaoTriggerAdReward)
                {
                    for (int i = 0; i < tripulacaoGameObject.Length; i++)
                    {
                        tripulacaoGameObject[i].GetComponent<Tripulação>().VelocidadeProblema /= 2;
                    }
                }
                if (impacienciaTriggerAdReward)
                {
                    for (int i = 0; i < tripulacaoGameObject.Length; i++)
                    {
                        tripulacaoGameObject[i].GetComponent<Tripulação>().Paciencia /= 2;
                    }
                }

                velocidadeTriggerAdReward = false;
                resolucaoTriggerAdReward = false;
                impacienciaTriggerAdReward = false;

                adTrigged = false;
                canTriggerAd = true;
            }
        }
        else
        {
            timerRemoveAdEffect = 20;
        }
    }
    public void AdsRewardsSort()
    {
        DebugX.Log(@"Sorteia a recompensa:b;");

        int boostTripulanteSpeed = 20;
        int boostResolucaoSpeed = 40;
        int boostImpaciencia = 60;
        int giveBasicCoin = 80;

        /// <summary>
        /// 0 - 20 Duplica a velocidade dos tripulantes.
        /// 21 - 40 Diminui a velocidade de resolver o problema.
        /// 41 - 60 Aumenta a impaciencia do tripulante.
        /// 61 - 80 Da uma quantidade de moeda baisca.
        /// 81 - 100 Da uma quantidade de moeda paga.
        /// </summary>

        int valorRandom = Random.Range(0, 100);

        if (valorRandom <= boostTripulanteSpeed)
        {
            DebugX.Log(@"Aumenta Velocidade Tripulação:green:b;");

            for (int i = 0; i < tripulacaoGameObject.Length; i++)
            {
                tripulacaoGameObject[i].GetComponent<Tripulação>().VelocidadeMovimento *= 2;
            }

            adTrigged = true;
            canTriggerAd = false;
            velocidadeTriggerAdReward = true;
        }
        else
        {
            if (valorRandom <= boostResolucaoSpeed)
            {
                DebugX.Log(@"Aumenta Velocidade Resolução:green:b;");

                for (int i = 0; i < tripulacaoGameObject.Length; i++)
                {
                    tripulacaoGameObject[i].GetComponent<Tripulação>().VelocidadeProblema *= 2;
                }

                adTrigged = true;
                canTriggerAd = false;
                resolucaoTriggerAdReward = true;
            }
            else
            {
                if (valorRandom <= boostImpaciencia)
                {
                    DebugX.Log(@"Aumenta Impaciencia Maxima da Tripulação:green:b;");

                    for (int i = 0; i < tripulacaoGameObject.Length; i++)
                    {
                        tripulacaoGameObject[i].GetComponent<Tripulação>().Paciencia *= 2;
                    }

                    adTrigged = true;
                    canTriggerAd = false;
                    impacienciaTriggerAdReward = true;
                }
                else
                {
                    if (valorRandom <= giveBasicCoin)
                    {
                        DebugX.Log(@"Da moedas basica:green:b;");
                    }
                    else
                    {
                        DebugX.Log(@"Da moeda paga:green:b;");
                    }
                }
            }
        }

        timerInicial = Random.Range(timerObjetivo + 20, timerObjetivo * 2);

        timerAtual = timerInicial;

        liberaAdRandom = Random.Range(0, 100);
        
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

        Debug.Log("1");

        GameObject tripulanteProximo = FindClosestTripulante(passageiro);

        if (!tripulanteProximo.GetComponent<Tripulação>().EstaResolvendoAlgo)
        {
            tripulanteProximo.GetComponent<NavMeshAgent>().SetDestination(passageiro.transform.position);

            tripulanteProximo.GetComponent<Tripulação>().Passageiro = passageiro;

            float distancia = Vector3.Distance(tripulanteProximo.transform.position, passageiro.transform.position);
            DebugX.Log($"{tripulanteProximo.name}:yellow:b; esta ha uma distancia de ; {distancia}:red; do ; {passageiro.name}:yellow:b;");

            yield return new WaitForSeconds(1f);

            tripulanteProximo.GetComponent<Tripulação>().EstaResolvendoAlgo = true;

            StartCoroutine(TimerCoroutine());
        }
    }
    /*
    public IEnumerator ResolveProblemaTimer(Tripulação tripulacaoScript, Passageiro passageiroScript)
    {
        float timer = 100;


    }*/
}