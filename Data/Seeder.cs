﻿using AutoFixture;
using WebApi.Helpers;

namespace WebApi.Data
{
    public static class Seeder
    {
        
        // This is purely for a demo, don't anything like this in a real application!
        public static void Seed(this DataContext dataContext)
        {
            if (!dataContext.Products.Any())
            {
                Fixture fixture = new Fixture();
                fixture.Customize<Product>(product => product.Without(p => p.ProductId));
                //--- The next two lines add 100 rows to your database
                List<Product> products = fixture.CreateMany<Product>(100).ToList();
                dataContext.AddRange(products);
                dataContext.SaveChanges();
           }
        }
    }
}