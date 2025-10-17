using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "HotdogData", menuName = "Hotdog System/Hotdog Data")]
public class HotdogData : ScriptableObject
{
    [System.Serializable]
    public class HotdogConfig
    {
        public string name;
        public int cost;
        public int weight;
        public HotdogType type; // ��� ������������� ����
    }

    public enum HotdogType
    {
        Classic,
        Caesar,
        Meat,
        Custom // ��� �������������� �����
    }

    public List<HotdogConfig> hotdogs = new List<HotdogConfig>();

    // ����� ��� ��������� ������� �� ����
    public HotdogConfig GetHotdogConfig(HotdogType type)
    {
        return hotdogs.Find(h => h.type == type);
    }

    // ����� ��� ��������� ������� �� �����
    public HotdogConfig GetHotdogConfig(string hotdogName)
    {
        return hotdogs.Find(h => h.name == hotdogName);
    }
}

[CreateAssetMenu(fileName = "IngredientData", menuName = "Hotdog System/Ingredient Data")]
public class IngredientData : ScriptableObject
{
    [System.Serializable]
    public class IngredientConfig
    {
        public string displayName;
        public int cost;
        public int weight;
        public IngredientType type;
    }

    public enum IngredientType
    {
        Pickles,
        SweetOnion,
        Custom
    }

    public List<IngredientConfig> ingredients = new List<IngredientConfig>();

    // ����� ��� ��������� ������� ����������� �� ����
    public IngredientConfig GetIngredientConfig(IngredientType type)
    {
        return ingredients.Find(i => i.type == type);
    }
}