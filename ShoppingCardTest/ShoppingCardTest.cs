using ShoppingCart;

namespace ShoppingCardTest;

[TestClass]
public sealed class CartTests
{
    private const string ProductName = "Stylo";
    private const decimal UnitPrice = 2.5m;

    [TestMethod]
    public void Add_Should_Create_Article_And_Update_TotalPrice()
    {
        var cart = new Cart();

        var article = cart.Add(ProductName, 2, UnitPrice);

        Assert.AreEqual(ProductName, article.ProductName);
        Assert.AreEqual(2, article.Quantity);
        Assert.AreEqual(UnitPrice, article.Price);
        Assert.AreEqual(5m, article.TotalPrice);
        Assert.AreEqual(5m, cart.TotalPrice);
        Assert.AreEqual(1, cart.Articles.Count());
    }

    [TestMethod]
    public void Add_Same_Product_Should_Increase_Existing_Quantity()
    {
        var cart = new Cart();

        var first = cart.Add(ProductName, 2, UnitPrice);
        var second = cart.Add(ProductName, 3, UnitPrice);

        Assert.AreSame(first, second);
        Assert.AreEqual(5, second.Quantity);
        Assert.AreEqual(1, cart.Articles.Count());
        Assert.AreEqual(12.5m, cart.TotalPrice);
    }

    [TestMethod]
    public void DecreaseArticleQuantity_Should_Remove_Article_When_Quantity_Reaches_Zero()
    {
        var cart = new Cart();
        cart.Add(ProductName, 1, UnitPrice);

        cart.DecreaseArticleQuantity(ProductName);

        Assert.AreEqual(0, cart.Articles.Count());
        Assert.AreEqual(0m, cart.TotalPrice);
    }

    [TestMethod]
    public void Add_Should_Throw_When_Quantity_Is_Negative()
    {
        var cart = new Cart();

        Assert.Throws<ArgumentOutOfRangeException>(() => cart.Add(ProductName, -1, UnitPrice));
    }

    [TestMethod]
    public void Add_Should_Throw_When_Quantity_Is_Zero()
    {
        var cart = new Cart();

        Assert.Throws<ArgumentOutOfRangeException>(() => cart.Add(ProductName, 0, UnitPrice));
    }

    [TestMethod]
    public void Add_Should_Throw_When_Price_Is_Negative()
    {
        var cart = new Cart();

        Assert.Throws<ArgumentOutOfRangeException>(() => cart.Add(ProductName, 1, -1m));
    }

    [TestMethod]
    public void Empty_Cart_Should_Report_IsNotEmpty_False()
    {
        var cart = new Cart();

        Assert.IsFalse(cart.IsNotEmpty);
    }
}
