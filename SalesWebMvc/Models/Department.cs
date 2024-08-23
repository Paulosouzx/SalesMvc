using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();


        public Department() 
        {
        }

        public Department(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            //Vamos enviar parametros de Data para a função que fizemos na classe Seller, que vai passar o initial e o final e vai nos retornar o valor final somado, na qual vamos retornar para outro local
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }

    }
}
