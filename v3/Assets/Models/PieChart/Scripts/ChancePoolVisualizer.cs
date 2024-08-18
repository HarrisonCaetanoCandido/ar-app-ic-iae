using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used on "Chance Pool Visualizer" prefab for drawing visual representation of item probabilities.
/// </summary>
public class ChancePoolVisualizer : MonoBehaviour
{
    [Tooltip("If FALSE, there will be \"weight\" value near item's name. If TRUE, there will be percentage value (from 0% to 100%).")]
    public bool showNormalizedValues;

    [Header("Visualizer elements")]
    [SerializeField] private Text poolNameLabel;
    [SerializeField] private RectTransform imageContainer;
    [SerializeField] private List<Image> circles;
    [SerializeField] private RectTransform namesContainer;
    [SerializeField] private List<RectTransform> nameLabels;
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject nameLabelPrefab;
    [Tooltip("These colors will be used cyclically in turn to represent pool items.")]
    [SerializeField] private Color[] colors;
    private byte colorIndex;

    public void Refresh<T>(List<ChancePoolItem<T>> items, string poolName)
    {
        // Prepare lists for first method usage.
        if (circles == null)
            circles = new List<Image>(items.Count);
        if (nameLabels == null)
            nameLabels = new List<RectTransform>(items.Count);

        // Calculate total weight of all items in pool.
        float totalWeight = 0f;
        foreach (ChancePoolItem<T> item in items)
            totalWeight += item.probability;

        colorIndex = 0;
        float currentWeight = 0f;
        for (int i = 0; i < items.Count; i++)
        {
            // Calculate item's value.
            float itemValue = items[i].probability / totalWeight;
            currentWeight += itemValue;

            // Get next color for each item.
            Color color = colors[colorIndex];
            colorIndex++;
            if (colorIndex >= colors.Length)
                colorIndex = 0;

            // Create new circle image if needed. Then set fill amount.
            if (i >= circles.Count)
            {
                Image circle = Instantiate(circlePrefab).GetComponent<Image>();
                circle.rectTransform.parent = imageContainer;
                circle.rectTransform.SetSiblingIndex(0);
                circle.rectTransform.localPosition = Vector3.zero;
                circles.Add(circle);
            }
            circles[i].gameObject.SetActive(true);
            circles[i].color = color;
            circles[i].fillAmount = currentWeight;

            // Create new name label if needed. Then set item's name and value.
            if (i >= nameLabels.Count)
            {
                RectTransform nameLabel = Instantiate(nameLabelPrefab).GetComponent<RectTransform>();
                nameLabel.parent = namesContainer;
                nameLabels.Add(nameLabel);
            }
            nameLabels[i].gameObject.SetActive(true);
            Text textLabel = nameLabels[i].GetChild(0).GetComponent<Text>();
            textLabel.text = items[i].item + ": " + (showNormalizedValues ? ((itemValue * 100f).ToString("##.#") + "%") : items[i].probability.ToString());
            textLabel.color = color;
        }

        // Switch off unused images and name labels.
        if (circles.Count > items.Count)
            for (int i = items.Count; i < circles.Count; i++)
                circles[i].gameObject.SetActive(false);
        if (nameLabels.Count > items.Count)
            for (int i = items.Count; i < nameLabels.Count; i++)
                nameLabels[i].gameObject.SetActive(false);

        // Show pool's name.
        if (string.IsNullOrEmpty(poolName) == false)
        {
            poolNameLabel.gameObject.SetActive(true);
            poolNameLabel.text = poolName;
        }
        else
            poolNameLabel.gameObject.SetActive(false);
    }
}
