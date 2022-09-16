[System.Serializable]
public struct GameDataStruct
{
    public float GameVolume;
    public float CashBalance;
    public Stats PlayerStats;
    public AddProductData[] AddProductDatas;
}

[System.Serializable]
public struct Stats
{
    public float Strength;
    public float Charisma;
}

[System.Serializable]
public struct AddProductData
{
    public string Name;
    public bool IsActivated;
}
