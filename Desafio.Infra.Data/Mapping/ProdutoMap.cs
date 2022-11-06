using Desafio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infra.Data.Mapping
{
    public class ProdutoMap : IEntityTypeConfiguration<Produtos>
    {
        public void Configure(EntityTypeBuilder<Produtos> builder)
        {


            builder.ToTable("produto");
            builder.HasKey(prop => prop.Codigo);
            
            builder.Property<string>("descricao")
                        .HasColumnType("STRING");

            builder.Property<string>("situacao")   
               .HasColumnType("STRING");

            builder.Property<DateTime>("data_fabricacao")
                .HasColumnType("STRING");

            builder.Property<DateTime>("data_validade")
                .HasColumnType("STRING");


            builder.Property<int>("codigo_fornecedor")
                .HasColumnType("INTEGER");


            builder.Property<string>("descricao_fornecedor")
                .HasColumnType("STRING");


            builder.Property<string>("cnpj")
                .HasColumnType("STRING");

        }
    }
}
