using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BankAPI.DAL
{
    public class YouBakingDbContextFactory : IDesignTimeDbContextFactory<YouBakingDbContext>
    {
        public object Accounts { get; internal set; }

        public YouBakingDbContext CreateDbContext(string[] args)
        {
            // 1. Obter a configuração a partir do arquivo appsettings.json

            // Tenta encontrar o diretório raiz da aplicação (o projeto Startup: BankAPI)
            var basePath = Directory.GetCurrentDirectory();

            // Se você usar o Package Manager Console (PMC), o GetCurrentDirectory() 
            // será o projeto definido como Startup no PMC.

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                // Carrega o arquivo padrão
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                // Se você estiver usando appsettings.Development.json, descomente ou adicione esta linha:
                // .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            // 2. Tentar obter a string de conexão
            var connectionString = configuration.GetConnectionString("BankDB");

            if (string.IsNullOrEmpty(connectionString))
            {
                // Este erro será lançado se o 'BankDB' não for encontrado ou estiver vazio.
                throw new InvalidOperationException(
                    "❌ Erro: String de conexão 'BankDB' não encontrada ou vazia no appsettings.json. " +
                    "Verifique o nome e o arquivo no projeto BankAPI."
                );
            }

            // 3. Configurar e retornar o DbContext
            var builder = new DbContextOptionsBuilder<YouBakingDbContext>();
            builder.UseSqlServer(connectionString);

            return new YouBakingDbContext(builder.Options);
        }
    }
}