using Faker;
using LAB_1;
using LAB_1.Models;
using System.Runtime.Serialization;
using static System.Reflection.Metadata.BlobBuilder;


using ( SapunovaBooksContext db = new SapunovaBooksContext())
{
    //добавление данных
    //Authors
    Author a1 = new Author { NameAuthor = "Иванов Иван Иванович" };
    Author a2 = new Author { NameAuthor = "Акунин Борис" };
    Author a3 = new Author { NameAuthor = "Котиков Котик" };
    db.Authors.AddRange(a1,a2,a3);
    //Publishing Houses
    PublishingHouse house1 = new PublishingHouse { Publish = "Мир", City = "Новосибирск" };
    PublishingHouse house2 = new PublishingHouse { Publish = "Питер Софт", City = "Санкт-Петербург" };
    PublishingHouse house3 = new PublishingHouse { Publish = "АСТ", City = "Москва" };
    db.PublishingHouses.AddRange(house1, house2,house3);
    db.SaveChanges();
    //Books
    Book b1 = new Book { TitleBook = "Труды без котиков", CodeAuthor = db.Authors.FirstOrDefault(p=>p.NameAuthor== "Иванов Иван Иванович")!.CodeAuthor, Pages = 250, CodePublish = db.PublishingHouses.FirstOrDefault(p=>p.Publish=="Мир")!.CodePublish };
    Book b2 = new Book { TitleBook = Faker.Lorem.Sentence(3), CodeAuthor = db.Authors.FirstOrDefault(p => p.NameAuthor == "Иванов Иван Иванович")!.CodeAuthor, Pages = 320, CodePublish = db.PublishingHouses.FirstOrDefault(p => p.Publish == "Питер Софт")!.CodePublish };
    Book b3 = new Book { TitleBook = "Турецкий гамбит", CodeAuthor = db.Authors.FirstOrDefault(p => p.NameAuthor == "Акунин Борис")!.CodeAuthor, Pages = 600, CodePublish = db.PublishingHouses.FirstOrDefault(p => p.Publish == "АСТ")!.CodePublish };
    Book b4 = new Book { TitleBook = Faker.Lorem.Sentence(3), CodeAuthor = db.Authors.FirstOrDefault(p => p.NameAuthor == "Акунин Борис")!.CodeAuthor, Pages = 2000, CodePublish = db.PublishingHouses.FirstOrDefault(p => p.Publish == "АСТ")!.CodePublish };
    Book b5 = new Book { TitleBook = "Труды о котиках", CodeAuthor = db.Authors.FirstOrDefault(p => p.NameAuthor == "Котиков Котик")!.CodeAuthor, Pages = 1700, CodePublish = db.PublishingHouses.FirstOrDefault(p => p.Publish == "Мир")!.CodePublish };
    Book b6 = new Book { TitleBook = Faker.Lorem.Sentence(3), CodeAuthor = db.Authors.FirstOrDefault(p => p.NameAuthor == "Котиков Котик")!.CodeAuthor, Pages = 270, CodePublish = db.PublishingHouses.FirstOrDefault(p => p.Publish == "Питер Софт")!.CodePublish };
    db.Books.AddRange(b1, b2, b3, b4, b5, b6);
    db.SaveChanges();
    //Delivery
    Delivery d1 = new Delivery { NameDelivery = Faker.Lorem.Sentence(4), NameCompany="ОАО Луч", Address="Комсомольская 4"};
    Delivery d2 = new Delivery { NameDelivery = Faker.Lorem.Sentence(4), NameCompany = "ЗАО Квантор", Address = "Комсомольская 10" };
    Delivery d3 = new Delivery { NameDelivery = Faker.Lorem.Sentence(4), NameCompany = Faker.Company.Name(), Address = Faker.Address.City() };
    db.Deliveries.AddRange(d1, d2, d3);
    db.SaveChanges();
    
    //Purchase
    Purchase p1 = new Purchase { CodeBook = 1, DateOrder = DateTime.Parse("09/09/2025"), CodeDelivery = db.Deliveries.FirstOrDefault(p => p.NameCompany == "ОАО Луч")!.CodeDelivery, Cost = 5000, Amount = 5 };
    Purchase p2 = new Purchase { CodeBook = 5, DateOrder = DateTime.Parse("19/08/2025"), CodeDelivery = db.Deliveries.FirstOrDefault(p => p.NameCompany == "ЗАО Квантор")!.CodeDelivery, Cost = 10000, Amount = 10 };
    Purchase p3 = new Purchase { CodeBook = 3, DateOrder = DateTime.Parse("19/08/2025"), CodeDelivery = db.Deliveries.FirstOrDefault(p => p.NameCompany == "ЗАО Квантор")!.CodeDelivery, Cost = 500, Amount = 10 };
    db.Purchases.AddRange(p1, p2, p3);
    db.SaveChanges();




        //6. Выбрать из таблицы Books названия книг и количество страниц
        //(поля Title_book и Pages), а из таблицы Authors выбрать имя соответствующего автора книги(поле Name_ author).
        var books_with_authors = db.Books.Join(db.Authors,
        b => b.CodeAuthor,
        a => a.CodeAuthor,
        (b, a) => new
        {
            Title = b.TitleBook,
            Pages = b.Pages,
            NameAuthor = a.NameAuthor
        } );
    Console.WriteLine("Названия и количеcтво страниц всех книг:");
    foreach ( var item in books_with_authors) Console.WriteLine($"title: {item.Title} pages: {item.Pages} author: {item.NameAuthor}");
    Console.WriteLine();


    //11. . Выбрать из таблицы Authors фамилии, имена, отчества авторов
    //(поле Name_ author), значения которых начинаются с ‘Иванов’.
    var ivanov = db.Authors.Where(p => p.NameAuthor.StartsWith("Иванов")).Select(p => p.NameAuthor).ToList();
    Console.WriteLine("Все Фио авторов, начинающиеся с \"Иванов\":");
    foreach (var item in ivanov) Console.WriteLine($"{item}");
    Console.WriteLine();


    //16. Вывести список названий книг (поле Title_book) и количество
    //страниц(поле Pages) из таблицы Books, у которых объем в страницах укладывается в
    //диапазон 200 – 300(условие по полю Pages).
    var title_pages = db.Books.Where(p => p.Pages >= 200 && p.Pages <= 300).Select(b => new { Title = b.TitleBook, Pages = b.Pages }).ToList();
    Console.WriteLine("книги с количеством страниц от 200 до 300");
    foreach (var item in title_pages) Console.WriteLine($"title: {item.Title}, pages: {item.Pages}");
    Console.WriteLine();


    //21. Вывести список авторов (поле Name_author) из таблицы Authors, которые начинаются на букву ‘К’.
    var starts_with_k = db.Authors.Where(p => p.NameAuthor.StartsWith("К")).Select(p => p.NameAuthor).ToList();
    Console.WriteLine("авторы на букву К");
    foreach (var item in starts_with_k) Console.WriteLine($"{item}");
    Console.WriteLine();


    //26. Вывести список издательств (поле Publish) из таблицы Publishing_house,
    //в которых выпущены книги, названия которых (поле Title_book)
    //начинаются со слова ‘Труды’ и город издания(поле City) – ‘Новосибирск’.
    var novosib = from b in db.Books
                  where b.TitleBook.StartsWith("Труды")
                  join p in db.PublishingHouses on b.CodePublish equals p.CodePublish
                  where p.City == "Новосибирск"
                  select new
                  {
                      Title = b.TitleBook,
                      Publish = p.Publish
                  };
    Console.WriteLine("Вывести список издательств в которых выпущены книги, названия которых начинаются со слова ‘Труды’ и город издания – ‘Новосибирск’.");
    foreach (var item in novosib) Console.WriteLine($"Title: {item.Title} Publish: {item.Publish}");
    Console.WriteLine();


    //31. Вывести суммарную стоимость партии одноименных книг
    //(использовать поля Amount и Cost) и название книги (поле Title_book) в каждой поставке.
    var sim_books = db.Purchases.Join(db.Books,
        p => p.CodeBook,
        b => b.CodeBook,
        (p, b) => new
        {
            Title = b.TitleBook,
            Cost_Amount = p.Amount*p.Cost
        });
    Console.WriteLine("Вывести суммарную стоимость партии одноименных книг в каждой поставке.");
    foreach (var item in sim_books) Console.WriteLine($"Title: {item.Title} Cost: {item.Cost_Amount}");
    Console.WriteLine();

    //36. Вывести среднюю стоимость (использовать поле Cost) и среднее количество экземпляров
    //книг(использовать поле Amount) в одной поставке, где автором книги является ‘Акунин’
    //(условие по полю Name_author). 
    var ak = from b in db.Books
             join a in db.Authors on b.CodeAuthor equals a.CodeAuthor
             where a.NameAuthor == "Акунин Борис"
             join p in db.Purchases on b.CodeBook equals p.CodeBook
             select new
             {
                 Cost = p.Cost,
                 Amount = p.Amount
             };
    Console.WriteLine("Вывести среднюю стоимость и среднее количество экземпляров книг в одной поставке, где автором книги является ‘Акунин’");
    foreach(var item in ak) Console.WriteLine(item.Cost);
    Console.WriteLine($"Средняя стоимость: {ak.Average(p=>p.Cost)}; Среднее количество: {ak.Average(p=>p.Amount)}");
    Console.WriteLine();


    //41. Вывести общую сумму поставок книг (использовать поле Cost) и
    //поместить результат в поле с названием Sum_cost, выполненных ‘ОАО Луч’ (условие по полю Name_company).
    var lych = db.Purchases.Join(db.Deliveries.Where(p => p.NameCompany == "ОАО Луч"),
        p => p.CodeDelivery,
        d => d.CodeDelivery,
        (p, d) => new
        {
            Cost = p.Cost
        });
    Console.WriteLine("Вывести общую сумму поставок книг, выполненных ‘ОАО Луч’");
    decimal sum_cost = 0;
    foreach (var item in lych) sum_cost += item.Cost;
    Console.WriteLine(sum_cost);
    Console.WriteLine();

    //46. Вывести список авторов (поле Name_author), книги которых были выпущены
    //в издательствах ‘Мир’, ‘Питер Софт’, ‘Наука’ (условие по полю Publish).
    var publ = from p in db.Purchases
               join b in db.Books on p.CodeBook equals b.CodeBook
               join h in db.PublishingHouses on b.CodePublish equals h.CodePublish
               where h.Publish == "Мир" || h.Publish == "Питер Софт" || h.Publish == "Наука"
               join a in db.Authors on b.CodeAuthor equals a.CodeAuthor
               select a.NameAuthor;
    Console.WriteLine("Вывести список авторов, книги которых были выпущены в издательствах ‘Мир’, ‘Питер Софт’, ‘Наука’");
    foreach (var item in publ) Console.WriteLine(item);
    Console.WriteLine();


    //Вывести список книг (поле Title_book), у которых количество страниц
    //(поле Pages) больше среднего количества страниц всех книг в таблице. 
    var more_pages = db.Books.Where(p => p.Pages > db.Books.Average(p => p.Pages)).Select(p => p.TitleBook).ToList();
    Console.WriteLine("Cписок книг , у которых количество страниц больше среднего количества страниц:");
    foreach (var item in more_pages) Console.WriteLine(item);
    Console.WriteLine();


    //56. Вывести список книг (поле Title_book), которые были поставлены
    //поставщиком ‘ЗАО Квантор’ (условие по полю Name_company).
    var kvantor = from p in db.Purchases
                  join d in db.Deliveries on p.CodeDelivery equals d.CodeDelivery
                  where d.NameCompany == "ЗАО Квантор"
                  join b in db.Books on p.CodeBook equals b.CodeBook
                  select new
                  {
                      Title = b.TitleBook
                  };
    Console.WriteLine("список книг, которые были поставлены поставщиком ‘ЗАО Квантор’");
    foreach (var item in kvantor) Console.WriteLine(item.Title);
    Console.WriteLine();


    //61. Добавить в таблицу Books новую запись, причем вместо ключевого
    //поля поставить код(поле Code_book), автоматически увеличенный на единицу
    //от максимального кода в таблице, вместо названия книги(поле Title_book) написать ‘Наука.Техника.Инновации’.
    Console.WriteLine("создаём новую книгу");
    Book new_book = new Book { TitleBook = "Наука.Техника.Инновации", CodeAuthor = 1, CodePublish = 1 };
    db.Books.Add(new_book);
    db.SaveChanges();
    var book_check = db.Books.FirstOrDefault(p => p.TitleBook == "Наука.Техника.Инновации");
    Console.WriteLine(book_check.TitleBook);


}