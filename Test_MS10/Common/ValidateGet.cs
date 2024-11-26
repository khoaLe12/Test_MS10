﻿namespace Test_MS10.Common;

public interface IValidateGet
{
    void ValidateGetRequest(ref int startPage, ref int endPage, int? quantity, ref int quantityResult);
}

internal class ValidateGet : IValidateGet
{
    private readonly ICheckQuantityTaken _checkQuantityTaken;

    public ValidateGet(ICheckQuantityTaken checkQuantityTaken)
    {
        _checkQuantityTaken = checkQuantityTaken;
    }

    public void ValidateGetRequest(ref int startPage, ref int endPage, int? quantity, ref int quantityResult)
    {
        quantityResult = _checkQuantityTaken.CheckQuantity(quantity);

        if (startPage <= 0)
        {
            startPage = 1;
        }

        if (endPage <= 0)
        {
            endPage = 1;
        }

        return;
    }
}
