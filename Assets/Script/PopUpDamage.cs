using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PopUpDamage : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Скорость движения текста вверх
    [SerializeField] private float fadeDuration = 1f; // Длительность исчезновения текста

    private TMP_Text _text;
    private Color _originalColor;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _originalColor = _text.color;
    }

    public void SetText(string damage)
    {
        _text.text = damage;
        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        float elapsed = 0f;
        Vector3 startPosition = transform.position;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            // Двигаем текст вверх
            transform.position = startPosition + Vector3.up * (moveSpeed * (elapsed / fadeDuration));

            // Плавно уменьшаем прозрачность
            _text.color = new Color(_originalColor.r, _originalColor.g, _originalColor.b, 1f - (elapsed / fadeDuration));

            yield return null;
        }

        Destroy(gameObject); // Удаляем объект после завершения анимации
    }
}
