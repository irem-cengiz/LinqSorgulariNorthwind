using LinqSorgulari.Models;
using Microsoft.EntityFrameworkCore;

namespace LinqSorgulari
{
                                         //NORTHWİND LINQ SORGU ÖRNEKLERİ:
    internal class Program
    {
        NorthwindContext db= new NorthwindContext();
            //1) Bütün employee'leri getirin.
            //2) İlk employeetyi getirin.
            //3)FİrstName e göre sıralayıp son employee yi yazdırın
            //4)İsmi A İle başlayan employee lerİ listeleyin
            //5) İsmi A İle başlayan İlk Employee yi yazdırın
            //6) İsmi içerisinde a harfi içeren employeeleri listeleyin
            //7)İsmi içerisinde a harfi içeren ilk employee yi yazdırın
            //8) Adı Andrew olan employee leri yazdırın
            //9) En pahalı productı yazdırın
            //10) En ucuz productl yazdırın
            //II) Fiyatı ortala fiyat üzerinde olan ürünleri yazdırın
            //12) Product ları Stock sayısına göre sıralayın
            //13) Product ları Önce Stock sayısına küçükten büyüğe, sonra ProductName e göre büyükten küçüğe sıralayın
            //14) Tüm product ları category lerİ İle birlikte listeleyin
            //15) ProductName, CategoryName, Suplier CompanyName ile birlikte yazdırın
            //16) Order detaİIs tablosundan her bir productİdnİn unitprice'ları toplamını yazdırın.
        static void Main(string[] args)
        {
            using ( var db = new NorthwindContext())
            {
                Console.WriteLine("Soru1");
                
                var employees = db.Employees
                             .Select(e => e);
                foreach (var employee in employees)
                {
                    Console.WriteLine(employee.FirstName + " " + employee.LastName);
                }

                Console.WriteLine("Soru2");
                var firstEmployee = db.Employees.FirstOrDefault();

                Console.WriteLine(firstEmployee.FirstName);


                Console.WriteLine("Soru3");

                var sonkisi = db.Employees.OrderByDescending(e => e.FirstName).FirstOrDefault();

                Console.WriteLine(sonkisi.FirstName);

                Console.WriteLine("Soru4");

                var ailebaslayanlar = db.Employees
                            .Where(e => e.FirstName.StartsWith("A"))
                            .ToList();
                foreach (var employee in ailebaslayanlar)
                {
                    Console.WriteLine($" Name: {employee.FirstName} {employee.LastName}");
                }


                Console.WriteLine("Soru5");

                var ailebaslayankisi = db.Employees.FirstOrDefault(e => e.FirstName.StartsWith("A"));

                Console.WriteLine(ailebaslayankisi.FirstName);


                Console.WriteLine("Soru6");

                var aicerekisiler = db.Employees
                            .Where(e => e.FirstName.Contains("a")).ToList();

                foreach (var e in aicerekisiler)
                    Console.WriteLine(e.FirstName);


                Console.WriteLine("Soru7");
                var aicerenilkkisi = db.Employees.FirstOrDefault(e => e.FirstName.Contains("a"));
                Console.WriteLine(aicerenilkkisi.FirstName);

                Console.WriteLine("Soru8");

                var kisi = db.Employees.Where(e => e.FirstName == "Andrew");
                foreach (var e in kisi)
                {
                    Console.WriteLine(e.FirstName + " " + e.LastName);
                }

                Console.WriteLine("Soru9");

                var pahalip = db.Products.Max(p => p.UnitPrice);

                Console.WriteLine(pahalip);

                Console.WriteLine("Soru10");

                var enucuzp = db.Products.Min(p => p.UnitPrice);
                Console.WriteLine(enucuzp);

                Console.WriteLine("Soru11");
                var avgproduct = db.Products.Average(p => p.UnitPrice);
                var yuksekfiyat = db.Products.Where(p => p.UnitPrice > avgproduct);

                foreach (var item in yuksekfiyat)
                {
                    Console.WriteLine("Ort üzerinde product:" + item.UnitPrice + " " + " Ürün adı:" + item.ProductName);
                }

                Console.WriteLine("Soru12");

                var siraliliste = db.Products.OrderBy(p => p.UnitsInStock);
                foreach (var i in siraliliste)
                {
                    Console.WriteLine(" Ürün adı" + i.ProductName + " " + " Stok sayısı:" + i.UnitsInStock);
                }

                Console.WriteLine("Soru13");
                var siraliurunler = db.Products
                                .OrderBy(p => p.UnitsInStock)
                                .ThenByDescending(p => p.ProductName)
                                .ToList();

                foreach (var s in siraliurunler)
                {
                    Console.WriteLine(s.ProductName);
                }
            
                Console.WriteLine("Soru14");

                var p = db.Products.Include("Category").ToList();

                foreach (var product in p)
                {
                    Console.WriteLine($"Product name: {product.ProductName}   " +
                        $"Category:{product.Category.CategoryName}");
                }

                Console.WriteLine("Soru15");

                var x = db.Products.Include("Category").Include("Supplier").ToList();

                foreach (var product in x)

                {
                    Console.WriteLine($"Product name: {product.ProductName}   Company name:{product.Supplier.CompanyName}  Categoryname:{product.Category.CategoryName}");
                }

                Console.WriteLine("Soru16");

                var od = db.OrderDetails.AsEnumerable()
                    .GroupBy(od => od.ProductId);

                foreach( var item in od)
                {
                    Console.WriteLine("ProductId:" + item.FirstOrDefault()!.ProductId + " UnitPrice toplamı:" + item.Sum(x => x.UnitPrice));

                }          
               
            }
        }
    }
}