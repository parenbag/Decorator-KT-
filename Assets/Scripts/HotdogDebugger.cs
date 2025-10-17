using UnityEngine;
using System.Collections.Generic;

public class HotdogDebugger : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private HotdogData hotdogData;
    [SerializeField] private IngredientData ingredientData;

    [Header("Debug Settings")]
    [SerializeField] private bool debugOnStart = true;
    [SerializeField] private bool showAllHotdogs = true;

    [Header("Custom Tests")]
    [SerializeField] private HotdogData.HotdogType testHotdogType = HotdogData.HotdogType.Classic;
    [SerializeField] private List<IngredientData.IngredientType> testIngredients = new List<IngredientData.IngredientType>();

    private HotdogFactory factory;

    void Start()
    {
        if (debugOnStart)
        {
            InitializeAndDebug();
        }
    }

    void InitializeAndDebug()
    {
        if (hotdogData == null || ingredientData == null)
        {
            Debug.LogError("HotdogData или IngredientData не назначены в инспекторе!");
            return;
        }

        if (!ValidateData())
        {
            Debug.LogError("Данные в Scriptable Objects заполнены не полностью!");
            return;
        }

        factory = new HotdogFactory(hotdogData, ingredientData);

        if (showAllHotdogs)
        {
            DebugAllHotdogs();
        }

        DebugCustomTest();
    }

    bool ValidateData()
    {
        if (hotdogData.hotdogs.Count == 0)
        {
            Debug.LogError("Список хотдогов пуст! Добавьте хотдоги в HotdogData.");
            return false;
        }

        foreach (var hotdog in hotdogData.hotdogs)
        {
            if (string.IsNullOrEmpty(hotdog.name))
            {
                Debug.LogError("Обнаружен хотдог без имени!");
                return false;
            }
        }

        if (ingredientData.ingredients.Count == 0)
        {
            Debug.LogWarning("Список ингредиентов пуст! Добавьте ингредиенты в IngredientData.");
        }

        return true;
    }

    void DebugAllHotdogs()
    {
        Debug.Log("=== ВСЕ ХОТДОГИ В СИСТЕМЕ ===");

        var allHotdogs = factory.CreateAllHotdogs();
        foreach (var hotdog in allHotdogs)
        {
            Debug.Log($"• {hotdog.GetDescription()}");

            // Показать варианты с ингредиентами для каждого хотдога
            DebugIngredientsForHotdog(hotdog);
        }
    }

    void DebugIngredientsForHotdog(Hotdog baseHotdog)
    {
        Debug.Log($"  Дополнительные варианты для {baseHotdog.GetName()}:");

        foreach (var ingredient in ingredientData.ingredients)
        {
            var decoratedHotdog = factory.AddIngredient(baseHotdog, ingredient);
            Debug.Log($"  - {decoratedHotdog.GetDescription()}");
        }
        Debug.Log("");
    }

    void DebugCustomTest()
    {
        Debug.Log("=== ТЕСТОВАЯ КОМБИНАЦИЯ ===");

        var testHotdog = factory.CreateHotdog(testHotdogType);
        if (testHotdog != null)
        {
            Debug.Log($"Базовый хотдог: {testHotdog.GetDescription()}");

            Hotdog currentHotdog = testHotdog;
            foreach (var ingredientType in testIngredients)
            {
                currentHotdog = factory.AddIngredient(currentHotdog, ingredientType);
                Debug.Log($"С добавкой {ingredientType}: {currentHotdog.GetDescription()}");
            }
        }
    }

    [ContextMenu("Запустить отладку")]
    public void ManualDebug()
    {
        InitializeAndDebug();
    }

    [ContextMenu("Показать все хотдоги")]
    public void ShowAllHotdogs()
    {
        if (factory == null) InitializeFactory();
        DebugAllHotdogs();
    }

    [ContextMenu("Проверить данные SO")]
    public void ValidateSOData()
    {
        if (ValidateData())
        {
            Debug.Log("✓ Данные Scriptable Objects валидны!");
            Debug.Log($"Количество хотдогов: {hotdogData.hotdogs.Count}");
            Debug.Log($"Количество ингредиентов: {ingredientData.ingredients.Count}");
        }
    }

    private void InitializeFactory()
    {
        if (hotdogData != null && ingredientData != null)
        {
            factory = new HotdogFactory(hotdogData, ingredientData);
        }
    }

    // Метод для получения фабрики (может быть полезен для других скриптов)
    public HotdogFactory GetFactory()
    {
        if (factory == null) InitializeFactory();
        return factory;
    }
}