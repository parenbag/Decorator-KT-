using UnityEngine;
using System.Collections.Generic;

public class HotdogFactory
{
    private HotdogData hotdogData;
    private IngredientData ingredientData;

    public HotdogFactory(HotdogData hotdogData, IngredientData ingredientData)
    {
        this.hotdogData = hotdogData;
        this.ingredientData = ingredientData;
    }

    // Создание хотдога по типу
    public Hotdog CreateHotdog(HotdogData.HotdogType type)
    {
        var config = hotdogData.GetHotdogConfig(type);
        if (config != null)
        {
            return new CustomHotdog(config.name, config.cost, config.weight);
        }
        return null;
    }

    // Создание хотдога по имени
    public Hotdog CreateHotdog(string hotdogName)
    {
        var config = hotdogData.GetHotdogConfig(hotdogName);
        if (config != null)
        {
            return new CustomHotdog(config.name, config.cost, config.weight);
        }
        return null;
    }

    // Создание всех хотдогов из списка
    public List<Hotdog> CreateAllHotdogs()
    {
        List<Hotdog> allHotdogs = new List<Hotdog>();
        foreach (var config in hotdogData.hotdogs)
        {
            allHotdogs.Add(new CustomHotdog(config.name, config.cost, config.weight));
        }
        return allHotdogs;
    }

    // Добавление ингредиента по типу
    public Hotdog AddIngredient(Hotdog hotdog, IngredientData.IngredientType ingredientType)
    {
        var ingredient = ingredientData.GetIngredientConfig(ingredientType);
        if (ingredient != null)
        {
            switch (ingredientType)
            {
                case IngredientData.IngredientType.Pickles:
                    return new PicklesDecorator(hotdog, ingredient.displayName, ingredient.cost, ingredient.weight);
                case IngredientData.IngredientType.SweetOnion:
                    return new SweetOnionDecorator(hotdog, ingredient.displayName, ingredient.cost, ingredient.weight);
                default:
                    return new GenericDecorator(hotdog, ingredient.displayName, ingredient.cost, ingredient.weight);
            }
        }
        return hotdog;
    }

    // Универсальный метод для добавления любого ингредиента
    public Hotdog AddIngredient(Hotdog hotdog, IngredientData.IngredientConfig ingredient)
    {
        return new GenericDecorator(hotdog, ingredient.displayName, ingredient.cost, ingredient.weight);
    }
}