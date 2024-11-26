﻿namespace Test_MS10.Common;

public interface ICheckQuantityTaken
{
    int CheckQuantity(int? quantity);
    public int QuantityPerPage { get; }
}

internal class CheckQuantityTaken : ICheckQuantityTaken
{
    private static readonly int MAX_QUANTITY = 50;
    private static readonly int MIN_QUANTITY = 1;
    private static readonly int QUANTITY_PER_PAGE = 10;
    public int CheckQuantity(int? quantity)
    {
        if(quantity is null || quantity == 0)
        {
            return QUANTITY_PER_PAGE;
        }

        if (quantity > MAX_QUANTITY)
        {
            return MAX_QUANTITY;
        }
        
        if (quantity < MIN_QUANTITY)
        {
            return MIN_QUANTITY;
        }

        return quantity ?? QUANTITY_PER_PAGE;
    }

    public int QuantityPerPage
    {
        get => QUANTITY_PER_PAGE;
    }
}
