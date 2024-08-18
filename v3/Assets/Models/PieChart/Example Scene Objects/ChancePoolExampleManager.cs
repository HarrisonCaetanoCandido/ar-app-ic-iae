using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ChancePoolExamples
{
    /// <summary>
    /// This class is used in example scene for demonstrating asset functionality.
    /// </summary>
    public class ChancePoolExampleManager : MonoBehaviour
    {
        [Tooltip("Change items in this pool and watch result.")]
        public ChancePool<RocketLaunchRisk> riskPool;

        [Header("Other example scene stuff")] 
        [SerializeField] private RectTransform resultLabel;
        [SerializeField] private Text resultLabelText;

        [ContextMenu("Normalize pool")]
        private void NormalizePool()
        {
            riskPool.Normalize();
        }

        /// <summary>
        /// This method is called by button in example scene.
        /// </summary>
        [ContextMenu("Refresh visualizer")]
        public void RefreshVisualizer()
        {
            riskPool.RedrawVisualizer();
        }

        private IEnumerator AnimateLabel()
        {
            float time = 1.5f;
            float startTime = Time.time;
            float endTime = startTime + time;

            float startY = -50f;
            float endY = 50f;
            resultLabel.gameObject.SetActive(true);
            resultLabel.anchoredPosition = new Vector3(0f, startY);

            while (Time.time < endTime)
            {
                float part = (Time.time - startTime) / time;
                float yPos = Mathf.Lerp(startY, endY, part);
                resultLabel.anchoredPosition = new Vector3(0f, yPos);
                yield return null;
            }
            resultLabel.gameObject.SetActive(false);
            yield return null;
        }

        [ContextMenu("Show info")]
        void asdad()
        {
            riskPool.ShowPoolInfoInUnityConsole();
        }
    }

    public enum RocketLaunchRisk
    {
        BemSucedido,
        FalhaTecnica,
        ProblemaMenor,
        FalhaCritica
    }
}