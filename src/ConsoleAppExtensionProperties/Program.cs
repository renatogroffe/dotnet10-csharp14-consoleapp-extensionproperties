using Bogus;
using System.Runtime.InteropServices;

Console.WriteLine("***** Testes com C# 14 + .NET 10 | Extension Properties *****");
Console.WriteLine($"Versao do .NET em uso: {RuntimeInformation
    .FrameworkDescription} - Ambiente: {Environment.MachineName} - Kernel: {Environment
    .OSVersion.VersionString}");

var random = new Random();
var fakeProdutos = new Faker<Produto>("pt_BR").StrictMode(false)
            .RuleFor(p => p.Nome, f => f.Commerce.Product())
            .RuleFor(p => p.CodigoBarras, f => f.Commerce.Ean13())
            .RuleFor(p => p.Preco, f => random.Next(10, 30))
            .Generate(5);

Console.WriteLine();
Console.WriteLine("Informacoes sobre produtos gerados (ficticios):");
foreach (var produto in fakeProdutos)
    Console.WriteLine($"* Produto: {produto.Nome} | Codigo de Barras: {produto.CodigoBarras} | " +
        $"Preco: {produto.Preco:C2} | Preco com 10% de desconto: {produto.PrecoDesc10:C2} | " +
        $"Preco com 20% de desconto: {produto.PrecoDesc20:C2}");

public class Produto
{
    public string? CodigoBarras { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
}

public static class ProdutoExtensions
{
    extension(Produto produto)
    {
        public decimal PrecoDesc10 => produto.Preco * 0.9m;
        public decimal PrecoDesc20 => produto.Preco * 0.8m;
    }
}