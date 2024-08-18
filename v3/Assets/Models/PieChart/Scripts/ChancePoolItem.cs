[System.Serializable]
public struct ChancePoolItem<V>
{
    public V item;
    public float probability;

    public ChancePoolItem(V item, float prob)
    {
        this.item = item;
        this.probability = prob;
    }

    /// <summary>Copy all values from "source item".</summary>
    public ChancePoolItem(ChancePoolItem<V> sourceItem)
    {
        this.item = sourceItem.item;
        this.probability = sourceItem.probability;
    }
}
