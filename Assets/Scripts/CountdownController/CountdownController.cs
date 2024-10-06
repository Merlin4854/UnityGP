using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using TMPro;
using UnityEngine.Events;

public class CountdownController : MonoBehaviour
{
    public GameObject buttonPanel; // Il pannello che contiene i pulsanti (Start, Cancel)
    public TMP_Text countdownText; // Cambiato da Text a TMP_Text
    public float cameraFollowSpeed = 5f; // Velocità di spostamento della camera
    public Button startButton;
    public Button cancelButton;
    public GameObject[] racers; // I tre oggetti che parteciperanno alla gara
    public Camera raceCamera; // Camera che seguirà i racers
    public Camera mainCamera; // Camera principale
    public Transform followTarget; // Un punto centrale che la camera seguirà

    private CancellationTokenSource cts;

    // Eventi Unity per gestire l'inizio e la fine della gara
    public UnityEvent onRaceStart;
    public UnityEvent<string> onRaceEnd;

    void Start()
    {
        // Assegna i listener ai pulsanti
        startButton.onClick.AddListener(StartCountdown);
        cancelButton.onClick.AddListener(CancelCountdown);

        // Assegna i listener agli eventi della gara
        onRaceStart.AddListener(StartRace);
        onRaceEnd.AddListener(DisplayWinner);

        // Inizializza il testo del countdown
        countdownText.text = "Premi 'Avvia' per iniziare";

        // Disattiva la camera della gara finché non inizia
        raceCamera.gameObject.SetActive(false);
    }

    void StartCountdown()
    {
        // Inizializza il CancellationTokenSource
        cts = new CancellationTokenSource();

        // Avvia il conto alla rovescia
        CountdownAsync(cts.Token).Forget();
    }

    private async UniTask CountdownAsync(CancellationToken token)
    {
        // Conto alla rovescia da 10 a 0
        for (int i = 10; i >= 0; i--)
        {
            countdownText.text = i.ToString();
            await UniTask.Delay(1000, cancellationToken: token); // Aspetta un secondo tra un numero e l'altro

            // Se il token è cancellato, esci dal ciclo
            if (token.IsCancellationRequested)
            {
                countdownText.text = "Conto alla rovescia annullato!";
                return;
            }
        }

        // Quando il countdown termina, avvia la gara
        countdownText.text = "Gara Iniziata!";
        onRaceStart.Invoke();

        // Passa alla camera della gara e disattiva il pannello
        mainCamera.gameObject.SetActive(false);
        raceCamera.gameObject.SetActive(true);
        buttonPanel.SetActive(false); // Disattiva il pannello dei pulsanti
    }


    private void CancelCountdown()
    {
        // Richiedi la cancellazione dell'operazione
        if (cts != null)
        {
            cts.Cancel();
        }
    }

    private void StartRace()
    {
        // Avvia la gara in asincrono
        StartRaceAsync().Forget();
    }

    private async UniTask StartRaceAsync()
    {
        float[] speeds = new float[racers.Length];
        bool raceFinished = false;
        string winner = "";

        // Assegna velocità casuali agli oggetti
        for (int i = 0; i < racers.Length; i++)
        {
            speeds[i] = Random.Range(1f, 5f); // Velocità casuali tra 1 e 5
        }

        while (!raceFinished)
        {
            // Aggiorna la posizione del followTarget come punto medio tra i racers
            UpdateFollowTarget();

            for (int i = 0; i < racers.Length; i++)
            {
                if (racers[i].transform.position.x >= 10f) // Condizione di vittoria (posizione X >= 10)
                {
                    raceFinished = true;
                    winner = "Racer " + (i + 1).ToString();
                    break;
                }
                else
                {
                    // Muovi i racers in avanti
                    racers[i].transform.Translate(Vector3.right * speeds[i] * Time.deltaTime);
                }
            }

            await UniTask.Yield(); // Attendere il frame successivo
        }

        // Quando la gara termina, ferma la gara e dichiara il vincitore
        onRaceEnd.Invoke(winner);
    }

    private void UpdateFollowTarget()
    {
        // Calcola la posizione centrale tra tutti i racers
        Vector3 averagePosition = Vector3.zero;

        foreach (GameObject racer in racers)
        {
            averagePosition += racer.transform.position;
        }

        averagePosition /= racers.Length; // Ottieni la posizione media
        followTarget.position = new Vector3(averagePosition.x, followTarget.position.y, followTarget.position.z); // Mantieni altezza e profondità costanti

        // Interpolazione della posizione della camera verso il followTarget
        raceCamera.transform.position = Vector3.Lerp(
            raceCamera.transform.position,
            new Vector3(followTarget.position.x, raceCamera.transform.position.y, raceCamera.transform.position.z),
            Time.deltaTime * cameraFollowSpeed
        );
    }


    private void DisplayWinner(string winner)
    {
        countdownText.text = winner + " ha vinto!";

        // Ferma il movimento degli altri oggetti
        foreach (GameObject racer in racers)
        {
            racer.GetComponent<Rigidbody>().velocity = Vector3.zero; // Ferma il movimento dei racers
        }

        // Ritorna alla camera principale
        mainCamera.gameObject.SetActive(true);
        raceCamera.gameObject.SetActive(false);

        // Riattiva il pannello e mostra il messaggio di vittoria
        buttonPanel.SetActive(true); // Riattiva il pannello dei pulsanti
    }

    private void OnDestroy()
    {
        // Pulisci le risorse
        cts?.Dispose();
    }
}
