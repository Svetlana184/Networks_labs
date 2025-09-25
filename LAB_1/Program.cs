using Faker;
using LAB_1;
using LAB_1.Models;
using static System.Reflection.Metadata.BlobBuilder;

//6 11 16 21 26 31 36 41 46 51 56 61 
using ( SapunovaBooksContext db = new SapunovaBooksContext())
{
    //Author author = new Author { NameAuthor = Lorem.Sentence() };

    //6. Выбрать из таблицы Books названия книг и количество страниц
    //(поля Title_book и Pages), а из таблицы Authors выбрать имя соответствующего автора книги(поле Name_ author).
    var arr = db.Books.Join(db.Authors,
        b => b.CodeAuthor,
        a => a.CodeAuthor,
        (b, a) => new
        {
            Title = b.TitleBook,
            Pages = b.Pages,
            NameAuthor = a.NameAuthor
        } );
    foreach ( var item in arr ) Console.WriteLine($"title: {item.Title} pages: {item.Pages} author: {item.NameAuthor}");


    //11. . Выбрать из таблицы Authors фамилии, имена, отчества авторов
    //(поле Name_ author), значения которых начинаются с ‘Иванов’.
    var arr2 = db.Authors.Where(p => p.NameAuthor.StartsWith("Иванов")).Select(p => p.NameAuthor).ToList();
    foreach (var item in arr2) Console.WriteLine($"{item}");


    //16. Вывести список названий книг (поле Title_book) и количество
    //страниц(поле Pages) из таблицы Books, у которых объем в страницах укладывается в
    //диапазон 200 – 300(условие по полю Pages).

    //21. Вывести список авторов (поле Name_author) из таблицы Authors, которые начинаются на букву ‘К’.
    var arr3 = db.Authors.Where(p => p.NameAuthor.StartsWith("К")).Select(p => p.NameAuthor).ToList();
    foreach (var item in arr3) Console.WriteLine($"{item}");


    //26. Вывести список издательств (поле Publish) из таблицы Publishing_house,
    //в которых выпущены книги, названия которых (поле Title_book)
    //начинаются со слова ‘Труды’ и город издания(поле City) – ‘Новосибирск’.


    //31. Вывести суммарную стоимость партии одноименных книг
    //(использовать поля Amount и Cost) и название книги (поле Title_book) в каждой поставке.


    //36. Вывести среднюю стоимость (использовать поле Cost) и среднее количество экземпляров
    //книг(использовать поле Amount) в одной поставке, где автором книги является ‘Акунин’
    //(условие по полю Name_author). 

    //41. Вывести общую сумму поставок книг (использовать поле Cost) и
    //поместить результат в поле с названием Sum_cost, выполненных ‘ОАО Луч’ (условие по полю Name_company).


    //46. Вывести список авторов (поле Name_author), книги которых были выпущены
    //в издательствах ‘Мир’, ‘Питер Софт’, ‘Наука’ (условие по полю Publish).


    //Вывести список книг (поле Title_book), у которых количество страниц
    //(поле Pages) больше среднего количества страниц всех книг в таблице. .


    //56. Вывести список книг (поле Title_book), которые были поставлены
    //поставщиком ‘ЗАО Квантор’ (условие по полю Name_company). .


    //61. Добавить в таблицу Books новую запись, причем вместо ключевого
    //поля поставить код(поле Code_book), автоматически увеличенный на единицу
    //от максимального кода в таблице, вместо названия книги(поле Title_book) написать ‘Наука.Техника.Инновации’.


}