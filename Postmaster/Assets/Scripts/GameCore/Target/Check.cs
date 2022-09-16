public class Check
{
    private ProductType _postBox;
    private ProductType _product;

    private float _price;
    private float _fine = 0.5f;
    private float _charisma;

    public Check(ProductType postBox, ProductType product, float price, float charisma)
    {
        _postBox = postBox;
        _product = product;
        _price = price;
        _charisma = charisma;
    }

    public float GetCash()
    {
        float cash = _price;

        if (_product != _postBox)
        {
            cash = cash * _fine;
        }
        if (cash <= 0.0f)
        {
            cash = 1.0f;
        }

        if (_charisma > 0)
        {
            cash = cash + (cash * (_charisma / 20f));
        }

        cash = (float)System.Math.Round(cash, 2);

        return cash;
    }
}
