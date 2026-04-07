using AtelierStock;

Produit p1 = new Produit("AT23", "Parpaing", 0.50m, 0.30m);
Produit p2 = new Produit("AT1", "Ciment", 7.50m, 0.10m);


p1.Rentrer(25);
p2.Rentrer(36);

p1.Sortir(10);

Console.WriteLine($"{p1.Reference,-10} | {p1.Libelle, -20} | {p1.Stocks,5} | {p1.PrixVente,7:0.00}");
Console.WriteLine($"{p2.Reference,-10} | {p2.Libelle, -20} | {p2.Stocks,5} | {p2.PrixVente,7:0.00}");
