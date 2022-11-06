using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces;
using Desafio.Infra.Data.Context;
using Desafio.Infra.Data.Repository;
using Desafio.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.Application.Model;

namespace Desafio.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("Default");


            services.AddEntityFrameworkSqlite()
                .AddDbContext<SqliteContext>(options =>
                {
                    options.UseSqlite(connectionString);
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                });

            services.AddScoped<IBaseRepository<Produtos>, BaseRepository<Produtos>>();
            services.AddScoped<IBaseService<Produtos>, BaseService<Produtos>>();

            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<Produtos, ProdutoModel>().AfterMap((src, dest) => 
                {
                    dest.CodigoFornecedor = src.Codigo_Fornecedor;
                    dest.DataFabricacao = src.Data_Fabricacao;
                    dest.DataValidade = src.Data_Validade;
                    dest.DescricaoFornecedor = src.Descricao_Fornecedor;
                    dest.SituacaoProduto = src.Situacao;
                    dest.CnpjFornecedor = src.Cnpj;
                });


                config.CreateMap<ProdutoModel, Produtos>().AfterMap((src, dest) => 
                {
                    dest.Codigo_Fornecedor = src.CodigoFornecedor;
                    dest.Data_Fabricacao = src.DataFabricacao.Value;
                    dest.Data_Validade = src.DataValidade.Value;
                    dest.Descricao_Fornecedor = src.DescricaoFornecedor;
                    dest.Situacao = src.SituacaoProduto;
                    dest.Cnpj = src.CnpjFornecedor;
                });

            }).CreateMapper());


            //  CreateMap<Group, GroupViewModel>().ReverseMap()
            //.ForMember(dest => dest.Description, src => src.Ignore())
            //.AfterMap((src, dest) =>
            //{
            //    dest.CreatedAt = DateTime.Now;
            //    dest.Description = dest.Name;
            //});

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
