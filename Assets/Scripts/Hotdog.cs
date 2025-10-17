using UnityEngine;
using System.Collections.Generic;

// Абстрактный базовый класс Hotdog
public abstract class Hotdog
{
    protected string name;
    protected int baseCost;
    protected int baseWeight;

    public Hotdog(string name, int cost, int weight)
    {
        this.name = name;
        this.baseCost = cost;
        this.baseWeight = weight;
    }

    public virtual string GetName()
    {
        return name;
    }

    public virtual int GetCost()
    {
        return baseCost;
    }

    public virtual int GetWeight()
    {
        return baseWeight;
    }

    public virtual string GetDescription()
    {
        return $"{GetName()} ({GetWeight()}г) — {GetCost()}р.";
    }
}

// Базовый класс для всех хотдогов с поддержкой SO
public class CustomHotdog : Hotdog
{
    public CustomHotdog(string name, int cost, int weight) : base(name, cost, weight) { }
}

// Базовый класс-декоратор
public abstract class HotdogDecorator : Hotdog
{
    protected Hotdog decoratedHotdog;

    public HotdogDecorator(Hotdog hotdog, string decoratorName, int decoratorCost, int decoratorWeight)
        : base(hotdog.GetName(), hotdog.GetCost(), hotdog.GetWeight())
    {
        decoratedHotdog = hotdog;
        name = $"{hotdog.GetName()} {decoratorName}";
        baseCost = hotdog.GetCost() + decoratorCost;
        baseWeight = hotdog.GetWeight() + decoratorWeight;
    }
}

// Конкретные декораторы
public class PicklesDecorator : HotdogDecorator
{
    public PicklesDecorator(Hotdog hotdog, string decoratorName, int decoratorCost, int decoratorWeight)
        : base(hotdog, decoratorName, decoratorCost, decoratorWeight) { }
}

public class SweetOnionDecorator : HotdogDecorator
{
    public SweetOnionDecorator(Hotdog hotdog, string decoratorName, int decoratorCost, int decoratorWeight)
        : base(hotdog, decoratorName, decoratorCost, decoratorWeight) { }
}

// Универсальный декоратор для любых ингредиентов
public class GenericDecorator : HotdogDecorator
{
    public GenericDecorator(Hotdog hotdog, string decoratorName, int decoratorCost, int decoratorWeight)
        : base(hotdog, decoratorName, decoratorCost, decoratorWeight) { }
}