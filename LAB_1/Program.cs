using Faker;
using LAB_1;
using LAB_1.Models;
using static System.Reflection.Metadata.BlobBuilder;


using ( SapunovaBooksContext db = new SapunovaBooksContext())
{
    //добавление данных



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
    Console.WriteLine("Названия и количсетво страниц всех книг:");
    foreach ( var item in books_with_authors) Console.WriteLine($"title: {item.Title} pages: {item.Pages} author: {item.NameAuthor}");


    //11. . Выбрать из таблицы Authors фамилии, имена, отчества авторов
    //(поле Name_ author), значения которых начинаются с ‘Иванов’.
    var ivanov = db.Authors.Where(p => p.NameAuthor.StartsWith("Иванов")).Select(p => p.NameAuthor).ToList();
    Console.WriteLine("Все Фио авторов, начинающиеся с \"Иванов\":");
    foreach (var item in ivanov) Console.WriteLine($"{item}");


    //16. Вывести список названий книг (поле Title_book) и количество
    //страниц(поле Pages) из таблицы Books, у которых объем в страницах укладывается в
    //диапазон 200 – 300(условие по полю Pages).
    var title_pages = db.Books.Where(p => p.Pages >= 200 && p.Pages <= 300).Select(b => new { Title = b.TitleBook, Pages = b.Pages }).ToList();
    Console.WriteLine("книги с количеством страниц от 200 до 300");
    foreach (var item in title_pages) Console.WriteLine($"title: {item.Title}, pages: {item.Pages}");

    //21. Вывести список авторов (поле Name_author) из таблицы Authors, которые начинаются на букву ‘К’.
    var starts_with_k = db.Authors.Where(p => p.NameAuthor.StartsWith("К")).Select(p => p.NameAuthor).ToList();
    Console.WriteLine("авторы на букву К");
    foreach (var item in starts_with_k) Console.WriteLine($"{item}");


    //26. Вывести список издательств (поле Publish) из таблицы Publishing_house,
    //в которых выпущены книги, названия которых (поле Title_book)
    //начинаются со слова ‘Труды’ и город издания(поле City) – ‘Новосибирск’.


    //31. Вывести суммарную стоимость партии одноименных книг
    //(использовать поля Amount и Cost) и название книги (поле Title_book) в каждой поставке.


    //36. Вывести среднюю стоимость (использовать поле Cost) и среднее количество экземпляров
    //книг(использовать поле Amount) в одной поставке, где автором книги является ‘Акунин’
    //(условие по полю Name_author). 

    //41. Вывести общую сумму поставок книг (использовать поле Cost) и
    //поместить результат в поле с названием Sum_cost, выполненных ‘ОАО Луч’ (условие по полю Name_company).


    //46. Вывести список авторов (поле Name_author), книги которых были выпущены
    //в издательствах ‘Мир’, ‘Питер Софт’, ‘Наука’ (условие по полю Publish).


    //Вывести список книг (поле Title_book), у которых количество страниц
    //(поле Pages) больше среднего количества страниц всех книг в таблице. 
    var more_pages = db.Books.Where(p => p.Pages > db.Books.Average(p => p.Pages)).Select(p => p.TitleBook).ToList();
    Console.WriteLine("Cписок книг , у которых количество страниц больше среднего количества страниц:");
    foreach (var item in more_pages) Console.WriteLine(item);


    //56. Вывести список книг (поле Title_book), которые были поставлены
    //поставщиком ‘ЗАО Квантор’ (условие по полю Name_company). 
    var from_kvantor = db.Books.Join(db.PublishingHouses.Where(p => p.Publish == "ЗАО Квантор"),
        b => b.CodePublish,
        p => p.CodePublish,
        (b, p) =>
        new
        {
            Title = b.TitleBook
        });
    Console.WriteLine("Список книг, которые были поставлены ‘ЗАО Квантор'");
    foreach (var item in from_kvantor) Console.WriteLine(item.Title);


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