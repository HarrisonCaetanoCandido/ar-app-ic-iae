using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class ChancePool<T>
{
    [Tooltip("Pool's name isn't necessary at all. Although it can be useful if you are printing pool information to the console. " +
             "In this case, you will know which pool you are talking about.")]
    public string poolName;
    [Tooltip("The default item that will be given to you if there are zero items in the pool.")]
    public T defaultItem;
    [Tooltip("Optional. Use this UI visualizer to watch items' chances in real time. Or leave this field null.")]
    public ChancePoolVisualizer visualizer;
    [Tooltip("Pool of your items. You can change their probabilities in Unity's inspector. Don't make duplicate items!")]
    [SerializeField] private List<ChancePoolItem<T>> items;

    #region Constructors
    public ChancePool()
    {
        items = new List<ChancePoolItem<T>>();
    }

    public ChancePool(T defaultItem)
    {
        this.defaultItem = defaultItem;
        items = new List<ChancePoolItem<T>>();
    }

    /// <param name="count">If you know exactly how many things will be in the pool, you can pre-specify their number for array initializing.</param>
    public ChancePool(T defaultItem, int count)
    {
        this.defaultItem = defaultItem;
        items = new List<ChancePoolItem<T>>(count);
    }

    public ChancePool(T defaultItem, params ChancePoolItem<T>[] items)
    {
        this.defaultItem = defaultItem;
        this.items = new List<ChancePoolItem<T>>(items.Length);
        for (int i = 0; i < this.items.Count; i++)
            this.items[i] = items[i];
        OnPoolChanged();
    }

    public ChancePool(params ChancePoolItem<T>[] items)
    {
        this.items = new List<ChancePoolItem<T>>(items.Length);
        for (int i = 0; i < this.items.Count; i++)
            this.items[i] = items[i];
        OnPoolChanged();
    }

    public ChancePool(ChancePool<T> poolForCopyingFrom)
    {
        MakeDeepCopyFrom(poolForCopyingFrom);
    }
    #endregion Constructors

    #region Main pool management methods
    /// <summary>
    /// Get a random item from pool, taking into account all the probabilities.
    /// </summary>
    /// <returns>Random item. If there is no items in pool, returns default item. If default item isn't set, return default object for item type.</returns>
    public T GetItem()
    {
        if (items != null && items.Count > 0)
        {
            float totalWeight = 0;
            foreach (ChancePoolItem<T> item in items)
                totalWeight += item.probability;

            float rnd = Random.Range(0f, totalWeight);
            float weightCounter = 0f; // It is an accumulated weight.
            foreach (ChancePoolItem<T> item in items)
            {
                weightCounter += item.probability;
                if (weightCounter >= rnd)
                    return item.item;
            }
        }

        if (defaultItem != null)
            return defaultItem;

        ShowWarningConsoleMessage();
        return default(T);
    }

    /// <summary>
    /// Add a new item to the pool.
    /// </summary>
    /// <param name="refreshIfExistsAlready">If there is already such an item in the pool, just rewrite item's chance to a new value.</param>
    public void AddItem(T item, int chance, bool refreshIfExistsAlready = true)
    {
        bool existsAlready = false;
        for (int i = 0; i < items.Count; i++)
            if (items[i].item.Equals(item))
            {
                existsAlready = true;
                if (refreshIfExistsAlready)
                    items[i] = new ChancePoolItem<T>(item, chance);
                break;
            }

        if (existsAlready == false)
            _AddItem(item, chance);

        OnPoolChanged();
    }

    private void _AddItem(T item, float probability)
    {
        items.Add(new ChancePoolItem<T>(item, probability));
    }

    /// <summary>
    /// Remove item from pool. Also removes all item's duplicates (if you occasionally made some copies of same item in inspector).
    /// </summary>
    /// <returns>TRUE if item was found and removed from pool. FALSE if there was no such item in pool.</returns>
    public bool RemoveItem(T item)
    {
        if (items != null && items.Count > 0)
            for (int i = items.Count - 1; i >= 0; i--)
                if (items[i].item.Equals(item))
                {
                    items.RemoveAt(i);
                    OnPoolChanged();
                    return true;
                }
        return false;
    }

    public void MakeDeepCopyFrom(ChancePool<T> source)
    {
        defaultItem = source.defaultItem;
        items = new List<ChancePoolItem<T>>(source.items);

        OnPoolChanged();
    }

    /// <summary>
    /// Set new value for item's chance if such item exists in the pool.
    /// </summary>
    /// <param name="addNewItemIfNotExists">If there is no such item in pool, adds new one.</param>
    public void SetChance(T item, float probability, bool addNewItemIfNotExists = true)
    {
        for (int i = 0; i < items.Count; i++)
            if (items[i].item.Equals(item))
            {
                items[i] = new ChancePoolItem<T>(item, probability);
                return;
            }

        if (addNewItemIfNotExists)
            _AddItem(item, probability);

        OnPoolChanged();
    }

    /// <summary>
    /// Use this method if you for some reason need to know current chance for specific item.
    /// </summary>
    /// <returns>Item' current chance or "-1" if there is no such item in the pool.</returns>
    public float GetChance(T item)
    {
        for (int i = 0; i < items.Count; i++)
            if (items[i].item.Equals(item))
                return items[i].probability;

        ShowWarningConsoleMessage();
        return -1;
    }

    /// <returns>True if chance value was successfully copied from same item in source pool.
    /// False if current or/and source pool doesn't contain such item.</returns>
    public bool CopyChanceFrom(T item, ChancePool<T> sourcePool)
    {
        if (sourcePool.items == null || sourcePool.items.Count < 1)
            return false;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item.Equals(item) == false) continue;

            // There is a big chance that item is places at same index in other pool.
            if (i < sourcePool.items.Count && items[i].item.Equals(sourcePool.items[i].item))
            {
                items[i] = new ChancePoolItem<T>(sourcePool.items[i]);
                OnPoolChanged();
                return true;
            }

            // Look for same item in source pool and copy its chance.
            for (int j = 0; j < sourcePool.items.Count; j++)
                if (sourcePool.items[j].item.Equals(item))
                {
                    items[i] = new ChancePoolItem<T>(sourcePool.items[j]);
                    OnPoolChanged();
                    return true;
                }

            // There is no same item in source pool.
            return false;
        }
        return false;
    }

    /// <summary>
    /// Recalculate all item chances so total weight become equal 1.0 or 100 (depending on the selected type of normalization).
    /// </summary>
    /// <param name="normalizationMode">The desired kind of normalization: the sum of all values is equal to 1.0 or 100.</param>
    public void Normalize(NormalizationMode normalizationMode = NormalizationMode._100percents)
    {
        float totalWeight = 0f;
        foreach (ChancePoolItem<T> item in items)
            totalWeight += item.probability;

        if (normalizationMode == NormalizationMode.From_0_to_1)
            for (int i = 0; i < items.Count; i++)
                items[i] = new ChancePoolItem<T>(items[i].item, items[i].probability / totalWeight);
        else
            for (int i = 0; i < items.Count; i++)
                items[i] = new ChancePoolItem<T>(items[i].item, items[i].probability / totalWeight * 100f);
    }

    /// <summary>
    /// This method is called every time you add, change or remove item via class methods. And it is NOT called when you change values in the inspector.
    /// </summary>
    private void OnPoolChanged()
    {
        RedrawVisualizer();
    }
    #endregion Main pool management methods

    #region Other service methods
    /// <summary>
    /// If UI visualizer variable is setup, redraw it to represent current items chances.
    /// </summary>
    public void RedrawVisualizer()
    {
        if (visualizer != null)
            visualizer.Refresh(items, poolName);
    }

    /// <summary>
    /// Show pool's full information in Unity's console: number of items and their probabilities etc.
    /// </summary>
    public void ShowPoolInfoInUnityConsole()
    {
        Debug.Log(GetPoolInfo());
    }

    /// <summary>
    /// Returns string containing all information about pool: number of items and their probabilities etc.
    /// </summary>
    /// <returns>String containing all information about pool.</returns>
    public string GetPoolInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Pool info.");
        if (string.IsNullOrEmpty(poolName) == false)
        {
            sb.Append(" Name: ");
            sb.Append(poolName);
        }
        sb.Append(" Number of items: ");
        sb.Append(items.Count);
        if (items != null && items.Count > 0)
            foreach (ChancePoolItem<T> item in items)
            {
                sb.Append(" ");
                sb.Append(item.item);
                sb.Append(":");
                sb.Append(item.probability);
            }
        return sb.ToString();
    }

    private void ShowWarningConsoleMessage()
    {
        Debug.Log("Don't use a pool if it doesn't have at least one item.");
        Debug.LogWarning("Don't use a pool if it doesn't have at least one item.");
        Debug.LogError("Don't use a pool if it doesn't have at least one item.");
    }
    #endregion Other service methods

    public enum NormalizationMode : byte
    {
        From_0_to_1,
        _100percents,
    }
}
