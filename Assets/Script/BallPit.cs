using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class BallPit : MonoBehaviour
{
    public float multiplier;

    public GameManagerNew GameManagerNew;

    public GameObject scorePopup;
    [Header("Score Popup Motion")]
    [Min(0f)] public float scorePopupUpDistance = 0.6f;
    [Min(0f)] public float scorePopupDownDistance = 0.4f;
    [Min(0.01f)] public float scorePopupUpDuration = 1f;
    [Min(0.01f)] public float scorePopupDownDuration = 0.25f;

    public TMP_Text multiplierText;
    public SpriteRenderer spriteRenderer;
    public Color[] colors;
    public float[] multipliers;

    public int level;


    void Start()
    {
        GameManagerNew = FindFirstObjectByType<GameManagerNew>();
        ApplyLevel();
    }

    void Update()
    {
        if (multiplierText != null)
        {
            multiplierText.text = "x" + multiplier.ToString();
        }

        if (spriteRenderer != null && colors != null && colors.Length > 0)
        {
            int colorIndex = Mathf.Clamp(level, 0, colors.Length - 1);
            spriteRenderer.color = colors[colorIndex];
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (GameManagerNew != null)
            {
                int score = Mathf.RoundToInt(multiplier * GameManagerNew.BallValue);
                GameManagerNew.money += score;
                ShowScorePopup(score);
            }
            Destroy(other.gameObject);
        }
    }

    public void UpgradeLevel()
    {
        if (level >= 6) return;
        level += 1;
        ApplyLevel();
    }

    private void ApplyLevel()
    {
        if (multipliers == null || multipliers.Length == 0)
        {
            return;
        }

        level = Mathf.Clamp(level, 0, multipliers.Length - 1);
        multiplier = multipliers[level];
    }

    public void ShowScorePopup(int score)
    {
        if (scorePopup == null) return;

        GameObject popup = Instantiate(scorePopup, transform.position, scorePopup.transform.rotation);
        TMP_Text text = popup.GetComponent<TMP_Text>();
        if (text != null)
        {
            text.text = "+" + score.ToString();
        }

        StartCoroutine(AnimateScorePopup(popup.transform));
    }

    private IEnumerator AnimateScorePopup(Transform popupTransform)
    {
        if (popupTransform == null) yield break;

        Vector3 start = popupTransform.position;
        Vector3 upTarget = start + Vector3.up * scorePopupUpDistance;
        Vector3 downTarget = start + Vector3.down * scorePopupDownDistance;

        float t = 0f;
        while (t < scorePopupUpDuration)
        {
            t += Time.deltaTime;
            float a = scorePopupUpDuration <= 0f ? 1f : Mathf.Clamp01(t / scorePopupUpDuration);
            popupTransform.position = Vector3.Lerp(start, upTarget, a);
            yield return null;
        }

        t = 0f;
        Vector3 from = popupTransform.position;
        while (t < scorePopupDownDuration)
        {
            t += Time.deltaTime;
            float a = scorePopupDownDuration <= 0f ? 1f : Mathf.Clamp01(t / scorePopupDownDuration);
            popupTransform.position = Vector3.Lerp(from, downTarget, a);
            yield return null;
        }

        if (popupTransform != null)
        {
            Destroy(popupTransform.gameObject);
        }
    }
}
