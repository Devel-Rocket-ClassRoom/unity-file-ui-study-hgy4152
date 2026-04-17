using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;
    public TextMeshProUGUI scoreValue;

    public Button nextButton;

    public float statsDelay = 1f;
    public float scoreDuration = 2f;
    private const int totalStats = 6;
    private const int statsPerColumn = 3;
    private int[] statsRoll = new int[totalStats];
    private Coroutine routine;
    private int finalScore;

    private TextMeshProUGUI[] statsLabels;
    private TextMeshProUGUI[] statsValues;


    private float elapseTime = 0;
    private int count = 0;
    private int sum = 0;
    private void Awake()
    {
        nextButton.onClick.AddListener(OnNext);
        Reset();

        statsLabels = new TextMeshProUGUI[]
        {
            leftStatLabel, rightStatLabel
        };
        statsValues = new TextMeshProUGUI[] { leftStatValue, rightStatValue };
    }


    private void Reset()
    {
        leftStatLabel.text = string.Empty;
        leftStatValue.text = string.Empty;
        rightStatLabel.text = string.Empty;
        rightStatValue.text = string.Empty;
        scoreValue.text = "000000000";

        elapseTime = 0;

    }
    private void Update()
    {
        elapseTime += Time.deltaTime;


        // count 대신 스탯 배열을 받아서 왼/오 구분하는게 더 옳은 방향

        if(elapseTime > 1 && count < 6)
        {
            elapseTime = 0;

            string label = $"stat{count}\n";
            int value = Random.Range(100, 10000);
            sum += value;
            if(count < 3)
            {
                leftStatLabel.text += label;
                leftStatValue.text += $"{value:D4}\n";
            }
            else
            {
                rightStatLabel.text += label;
                rightStatValue.text += $"{value:D4}\n";
            }

            count++;

        }

        if(count == 6)
            SetTotalScore();

    }


    private void SetTotalScore()
    {
        int total = (int)Mathf.SmoothStep(0, sum, elapseTime / 5);

        
        if (sum - total < 10)
        {
            total = sum;
            count++;
            Debug.Log("완료");
        }

        scoreValue.text = $"{total:D9}";
    }

    public override void Open()
    {
        base.Open();

        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
        ResetStats();

        //routine = StartCoroutine(coSetStat());
    }

    public override void Close()
    {
        base.Close();

        if(routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }

    public void OnNext()
    {
        windowManager.Open(0);
    }



    // 수업
    private void ResetStats()
    {
        for (int i = 0; i < totalStats; i++)
        {
            statsRoll[i] = Random.Range(0, 1000);

        }

        finalScore = Random.Range(0, 10000000);

        for (int i = 0; i < statsLabels.Length; i++)
        {
            statsLabels[i].text = string.Empty;
            statsValues[i].text = string.Empty;
        }

        scoreValue.text = $"{0:D9}";
    }

    IEnumerator coSetStat()
    {
        for (int i = 0; i < totalStats; i++)
        {
            yield return new WaitForSeconds(statsDelay);

            int column = i / statsPerColumn;
            var labelText = statsLabels[column];
            var valueText = statsValues[column];

            string newLine = (i % statsPerColumn == 0) ? string.Empty : "\n";
            labelText.text = $"{labelText.text}{newLine}Stat {i}";
            valueText.text = $"{valueText.text}{newLine}{statsRoll[i]:D4}";
        }

        int current = 0;
        float t = 0f;
        while(t < scoreDuration)
        {
            t += Time.deltaTime / scoreDuration;
            current = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
            scoreValue.text = $"{current:D9}";
            yield return null;
        }



        scoreValue.text = $"{finalScore:D9}";
        routine = null;
    }
}
