using System;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData gameData { get; private set; }

    [SerializeField] private float _volume;
    [SerializeField] private float _cashBalance;
    [SerializeField] private Stats _stats;
    [SerializeField] private List<AddProductData> _addProductDatas;

    public float Volume
    {
        get { return _volume; }
        set
        {
            if (value >= 0 && value <= 100)
            {
                _volume = value;
                SaveDataFile();
            }
        }
    }
    public float CashBalance
    {
        get { return _cashBalance; }
        set 
        {
            float newBalance = _cashBalance + value;
            if (newBalance >= 0)
            {
                _cashBalance = newBalance;
                SaveDataFile();
            }
        }
    }
    public Stats PlayerStats
    {
        get { return _stats; }
        set 
        {
            _stats = value;
            SaveDataFile();
        }
    }
    public AddProductData[] GetProducts()
    {
        List<AddProductData> addDataActivated = new List<AddProductData>();
        foreach (AddProductData addData in _addProductDatas)
        {
            if (addData.IsActivated == true)
            {
                addDataActivated.Add(addData);
            }
        }
        return addDataActivated.ToArray();
    }

    private void Awake()
    {
        gameData = this;

        _addProductDatas = new List<AddProductData>();
        foreach (ProductType type in Enum.GetValues(typeof(ProductType)))
        {
            AddProductData addData = new AddProductData();
            addData.Name = type.ToString();
            addData.IsActivated = false;
            _addProductDatas.Add(addData);
        }

        LoadDataFile();
    }

    public bool AddIsActivated(ProductType productType)
    {
        bool isActivated = false;

        foreach (AddProductData addData in _addProductDatas)
        {
            if (addData.Name == productType.ToString())
            {
                isActivated = addData.IsActivated;
                break;
            }
        }

        return isActivated;
    }

    public void ChangeAddProduct(string productType)
    {
        for(int i = 0; i < _addProductDatas.Count; i++)
        {
            if (_addProductDatas[i].Name == productType)
            {
                AddProductData newaAddData = new AddProductData();
                newaAddData.Name = _addProductDatas[i].Name;
                newaAddData.IsActivated = true;
                _addProductDatas[i] = newaAddData;
                break;
            }
        }
        SaveDataFile();
    }

    private void LoadDataFile()
    {
        GameDataStruct data = DataSaver.loadData<GameDataStruct>("GameDataPostMaster");

        _volume = data.GameVolume;
        _cashBalance = data.CashBalance;
        _stats = data.PlayerStats;

        if (data.AddProductDatas != null)
        {          
            for (int i = 0; i < data.AddProductDatas.Length; i++)
            {
                _addProductDatas[i] = data.AddProductDatas[i];
            }
        }

        CompareWithStandart();
    }

    private void SaveDataFile()
    {
        GameDataStruct data = new GameDataStruct
        {
            GameVolume = _volume,
            CashBalance = _cashBalance,
            PlayerStats = _stats,
            AddProductDatas = _addProductDatas.ToArray()
        };

        DataSaver.saveData(data, "GameDataPostMaster");
    }

    private void CompareWithStandart()
    {
        if (_volume < 0 || _volume > 100)
        {
            _volume = 50;
        }

        if (_cashBalance < 0)
        {
            _cashBalance = 0;
        }

        if (_stats.Strength < 1)
        {
            _stats.Strength = 1;
        }
        if (_stats.Strength > 10)
        {
            _stats.Strength = 10;
        }

        if (_stats.Charisma < 1)
        {
            _stats.Charisma = 1;
        }
        if (_stats.Charisma > 10)
        {
            _stats.Charisma = 10;
        }

        if (AddIsActivated(ProductType.Clothes) == false)
        {
            for (int i = 0; i < _addProductDatas.Count; i++)
            {
                if (_addProductDatas[i].Name == ProductType.Clothes.ToString())
                {
                    AddProductData newAddData = new AddProductData();
                    newAddData.Name = ProductType.Clothes.ToString();
                    newAddData.IsActivated = true;
                    _addProductDatas[i] = newAddData;
                    break;
                }
            }
        }

        if (AddIsActivated(ProductType.MusicalInstruments) == false)
        {
            for (int i = 0; i <= _addProductDatas.Count; i++)
            {
                if (_addProductDatas[i].Name == ProductType.MusicalInstruments.ToString())
                {
                    AddProductData newAddData = new AddProductData();
                    newAddData.Name = ProductType.MusicalInstruments.ToString();
                    newAddData.IsActivated = true;
                    _addProductDatas[i] = newAddData;
                    break;
                }
            }
        }

        SaveDataFile();
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            SaveDataFile();
        }
    }
#endif
    private void OnApplicationQuit()
    {
        SaveDataFile();
    }
}