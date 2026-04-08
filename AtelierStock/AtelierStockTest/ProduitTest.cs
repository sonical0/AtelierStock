namespace AtelierStock
{
    [TestClass]
    public class ProduitTest
    {
        [TestMethod]
        public void Initialiser_ProduitQuelconque()
        {
            // Arrange
            var prixAchat = 100.0m;
            var marge = 0.18m;

            // Act
            var p = new Produit("REF", "UnNom", prixAchat, marge);

            // Assert
            Assert.AreEqual("REF"  , p.Reference);
            Assert.AreEqual("UnNom", p.Libelle);
            Assert.AreEqual(100.0m , p.PrixAchat);
            Assert.AreEqual(118.0m , p.PrixVente);
            Assert.AreEqual(0, p.Stocks);
            Assert.IsTrue(p.EstEnRupture);
        }
        [TestMethod]
        public void Initialiser_ProduitMarge0()
        {
            // Arrange
            // Act
            var p = new Produit("REF", "UnNom", 100, 0);

            // Assert
            Assert.AreEqual("REF", p.Reference);
            Assert.AreEqual("UnNom", p.Libelle);
            Assert.AreEqual(100.0m, p.PrixAchat);
            Assert.AreEqual(100.0m, p.PrixVente);
            Assert.AreEqual(0, p.Stocks);
            Assert.IsTrue(p.EstEnRupture);
        }

        [TestMethod]
        public void Initialiser_ProduitReferenceVide_LeveArgumentException()
        {
            Action act = () => new Produit("", "UnNom", 100, 0);

            Assert.Throws<ArgumentException>(act);
        }

        [TestMethod]
        public void Rentrer_AugmenteStocks_EtRetireRupture()
        {
            var p = new Produit("REF", "UnNom", 100, 0.18m);

            p.Rentrer(10);

            Assert.AreEqual(10, p.Stocks);
            Assert.IsFalse(p.EstEnRupture);
        }

        [TestMethod]
        public void Sortir_StockSuffisant_RetourneEtRetireQuantiteDemandee()
        {
            var p = new Produit("REF", "UnNom", 100, 0.18m);
            p.Rentrer(10);

            int sorti = p.Sortir(4);

            Assert.AreEqual(4, sorti);
            Assert.AreEqual(6, p.Stocks);
            Assert.IsFalse(p.EstEnRupture);
        }

        [TestMethod]
        public void Sortir_StockInsuffisant_RetourneSeulementDisponible_EtPasseEnRupture()
        {
            var p = new Produit("REF", "UnNom", 100, 0.18m);
            p.Rentrer(3);

            int sorti = p.Sortir(10);

            Assert.AreEqual(3, sorti);
            Assert.AreEqual(0, p.Stocks);
            Assert.IsTrue(p.EstEnRupture);
        }

        [TestMethod]
        public void Sortir_QuantiteZero_StockVide_AucunChangement()
        {
            var p = new Produit("REF", "UnNom", 100, 0.18m);

            int sorti = p.Sortir(0);

            Assert.AreEqual(0, sorti);
            Assert.AreEqual(0, p.Stocks);
            Assert.IsTrue(p.EstEnRupture);
        }

        [TestMethod]
        public void Sortir_QuantiteZero_StockNonVide_AucunChangement()
        {
            var p = new Produit("REF", "UnNom", 100, 0.18m);
            p.Rentrer(10);

            int sorti = p.Sortir(0);

            Assert.AreEqual(0, sorti);
            Assert.AreEqual(10, p.Stocks);
            Assert.IsFalse(p.EstEnRupture);
        }

        [TestMethod]
        public void Sortir_EpuiseLeStock_EtPasseEnRupture()
        {
            var p = new Produit("REF", "UnNom", 100, 0.18m);
            p.Rentrer(5);

            int sorti = p.Sortir(5);

            Assert.AreEqual(5, sorti);
            Assert.AreEqual(0, p.Stocks);
            Assert.IsTrue(p.EstEnRupture);
        }

        [TestMethod]
        public void GestionStocks_OperationsSuccessives_ResteCoherente()
        {
            var p = new Produit("REF", "UnNom", 100, 0.18m);

            p.Rentrer(10);
            p.Sortir(3);
            p.Rentrer(2);
            int sorti = p.Sortir(4);

            Assert.AreEqual(4, sorti);
            Assert.AreEqual(5, p.Stocks);
            Assert.IsFalse(p.EstEnRupture);
        }

        [TestMethod]
        public void GestionStocks_QuantitesElevees_SansRegression()
        {
            var p = new Produit("REF", "UnNom", 100, 0.18m);

            p.Rentrer(1_000_000);
            int sorti = p.Sortir(999_999);

            Assert.AreEqual(999_999, sorti);
            Assert.AreEqual(1, p.Stocks);
            Assert.IsFalse(p.EstEnRupture);
        }


    }
}