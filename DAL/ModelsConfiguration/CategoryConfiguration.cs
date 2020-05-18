using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelsConfiguration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.CategoryId);
            builder.Property(p => p.CategoryId).UseMySqlIdentityColumn();
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(30).IsRequired();
            builder.HasData(new List<Category>
            {
                new Category {CategoryId = 1, Name = "Ноутбуки"},
                new Category {CategoryId = 2, Name = "Паншеты"},
                new Category {CategoryId = 3, Name = "Видеокарты"},
                new Category {CategoryId = 4, Name = "Жесткие диски"},
                new Category {CategoryId = 5, Name = "Мониторы"},
                new Category {CategoryId = 6, Name = "Компьютеры"},
                new Category {CategoryId = 7, Name = "Процессоры"},
                new Category {CategoryId = 8, Name = "SSD"},
                new Category {CategoryId = 9, Name = "Принтеры"},
                new Category {CategoryId = 10, Name = "Память"},
                new Category {CategoryId = 11, Name = "Материнские платы"},
                new Category {CategoryId = 12, Name = "Мыши"},
                new Category {CategoryId = 13, Name = "Маршрутизаторы"},
                new Category {CategoryId = 14, Name = "Акустические системы"},
                new Category {CategoryId = 15, Name = "Клавиатуры"},
                new Category {CategoryId = 16, Name = "Блоки питания"},
                new Category {CategoryId = 17, Name = "Корпуса"},
                new Category {CategoryId = 18, Name = "Проекторы"},
                new Category {CategoryId = 19, Name = "Флеш память USB"},
                new Category {CategoryId = 20, Name = "Источники бесперебойного питания"},
                new Category {CategoryId = 21, Name = "Системы охлаждения"},
                new Category {CategoryId = 22, Name = "Игровые консоли"},
                new Category {CategoryId = 23, Name = "Сумки, рюкзаки и чехлы"},
                new Category {CategoryId = 24, Name = "Стабилизаторы напряжения"}
            });

        }
    }
}
