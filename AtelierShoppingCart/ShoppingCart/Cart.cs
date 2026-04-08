using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    /// <summary>
    /// Stores the shopping cart data.
    /// </summary>
    public class Cart
    {
        private Dictionary<string, Article> articles = new Dictionary<string, Article>();

        /// <summary>
        /// Get list of the cart articles.
        /// </summary>
        /// <example>
        /// You can assert on this collection using the following code :
        /// CollectionAssert.AreEquivalent(
        ///     new Article[] { new Article(...), new Article(...) },
        ///     test.Articles.ToList()    // using System.Linq;
        /// );
        /// </example>
        public IEnumerable<Article> Articles => articles.Values;

        /// <summary>
        /// Add an article to the cart with the given quantity and price.
        /// </summary>
        /// <param name="productName">Name of the product to add to the cart. 
        /// If an article with the same product name exist, both quantities are added.</param>
        /// <param name="qty">Product quantity. Must be strictly positive</param>
        /// <param name="price"></param>
        /// <returns>The article added to the shopping cart</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the quantity or price are negative or zero.</exception>
        public Article Add(string productName, int qty, decimal price)
        {
            if (articles.ContainsKey(productName))
            {
                var article = articles[productName];

                article.Quantity += qty;
                return article;
            }
            else
            {
                return articles[productName] = new Article(productName, qty, price);
            }
        }
        /// <summary>
        /// Decrease the quantity of the article with the given product name. 
        /// </summary>
        /// <param name="productName">Product name of the article which quantity is to decrease</param>
        /// <exception cref="System.ArgumentException">No article exists with the given product name</exception>
        public void DecreaseArticleQuantity(string productName)
        {
            if(articles[productName].Quantity == 1)
            {
                articles.Remove(productName);
            } 
            else
            {
                articles[productName].Quantity--;
            }
        }
        /// <summary>
        /// Get the total price for all the shopping cart
        /// </summary>
        public decimal TotalPrice => articles.Values.Sum( a => a.TotalPrice );

        /// <summary>
        /// Check if the shopping cart is empty
        /// </summary>
        public bool IsEmpty => articles.Any();
    }
}
