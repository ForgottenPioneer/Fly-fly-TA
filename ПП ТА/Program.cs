using System.Collections.Generic;
using System;

class TravelPlanner
{
    static void Main()
    {
        // База данных городов
        var database = new Dictionary<string, (string Country, List<string> FoodAndPlaces)>(StringComparer.OrdinalIgnoreCase)
        {
            // Россия
            { "Москва", ("Россия", new List<string> { "Московский бефстроганов", "Красная площадь", "Третьяковская галерея" }) },
            { "Санкт-Петербург", ("Россия", new List<string> { "Пышки", "Эрмитаж", "Дворцовая площадь" }) },
            { "Казань", ("Россия", new List<string> { "Эчпочмак", "Казанский Кремль", "Мечеть Кул-Шариф" }) },
            { "Новосибирск", ("Россия", new List<string> { "Сибирские пельмени", "Новосибирский театр оперы и балета", "Зоопарк" }) },
            { "Сочи", ("Россия", new List<string> { "Адлерская форель", "Олимпийский парк", "Красная Поляна" }) },
            { "Владивосток", ("Россия", new List<string> { "Дальневосточные крабы", "Русский мост", "Бухта Золотой Рог" }) },
            
            // Беларусь
            { "Минск", ("Беларусь", new List<string> { "Минские драники", "Замок Мир", "Костёл Святого Симеона" }) },
            { "Гродно", ("Беларусь", new List<string> { "Гродненский квас", "Фарный костёл", "Парк Жилибера" }) },
            { "Брест", ("Беларусь", new List<string> { "Брестский хрен", "Брестская крепость", "Музей железнодорожной техники" }) },
            { "Витебск", ("Беларусь", new List<string> { "Печёный карась", "Дом-музей Шагала", "Софийский собор" }) },
            { "Гомель", ("Беларусь", new List<string> { "Гомельская кулебяка", "Гомельский дворец Румянцевых и Паскевичей", "Парк Румянцевых" }) },
            { "Могилев", ("Беларусь", new List<string> { "Могилёвский борщ", "Могилёвская ратуша", "Зоосад" }) },

            // Канада
            { "Торонто", ("Канада", new List<string> { "Торонтовский путин (Poutine)", "CN Tower", "Рынок St. Lawrence" }) },
            { "Монреаль", ("Канада", new List<string> { "Монреальский бэгл", "Базилика Нотр-Дам", "Парк Mount Royal" }) },
            { "Квебек", ("Канада", new List<string> { "Квебекский кленовый сироп", "Квебекский замок", "Старый город" }) },
            { "Ванкувер", ("Канада", new List<string> { "Ванкуверский лосось", "Парк Стэнли", "Китайский квартал" }) },
            { "Оттава", ("Канада", new List<string> { "Оттавская бисквитная пудра", "Парламентский холм", "Национальный галерея" }) },
            { "Калгари", ("Канада", new List<string> { "Калгарийский стейк", "Башня Калгари", "Олимпийский парк" }) },
            { "Эдмонтон", ("Канада", new List<string> { "Эдмонтонский бобровый пирог", "Форт Эдмонтон", "Королевский музей" }) },

            // Китай
            { "Пекин", ("Китай", new List<string> { "Утка по-пекински", "Запретный город", "Великая китайская стена" }) },
            { "Шанхай", ("Китай", new List<string> { "Шанхайские димсамы", "Сад Юйюань", "Набережная Вайтань" }) },
            { "Сиань", ("Китай", new List<string> { "Сианьская лапша бианбиан", "Терракотовая армия", "Колокольная башня" }) },
            { "Гуанчжоу", ("Китай", new List<string> { "Гуанчжоуский ча-сиу", "Кантонская башня", "Храм шести баньянов" }) },
            { "Чэнду", ("Китай", new List<string> { "Чэндуский хотпот", "Исследовательская база панд", "Храм Ухоу" }) },
            { "Гуйлинь", ("Китай", new List<string> { "Гуйлиньский рис", "Горы Лицзян", "Река Ли" }) },
            { "Ханчжоу", ("Китай", new List<string> { "Ханчжоуский чай лунцзин", "Западное озеро", "Храм Линьинь" }) },

            // Казахстан
            { "Алматы", ("Казахстан", new List<string> { "Алматинский бешбармак", "Горы Алатау", "Медео" }) },
            { "Нур-Султан", ("Казахстан", new List<string> { "Кумыс", "Байтерек", "Национальный музей" }) },
            { "Шымкент", ("Казахстан", new List<string> { "Шымкентский казы", "Крепость Шымкент", "Этнографический парк" }) },
            { "Атырау", ("Казахстан", new List<string> { "Атырауская уха", "Каспийское море", "Пешеходный мост" }) },
            { "Караганда", ("Казахстан", new List<string> { "Карагандинская каша", "Шахтёрская площадь", "Экопарк Ботакар" }) },
            { "Тараз", ("Казахстан", new List<string> { "Тараская пахлава", "Мавзолей Айша-Биби", "Крепость Тараз" }) },
            { "Костанай", ("Казахстан", new List<string> { "Костанайский баурсак", "Национальный парк Наурызым", "Музей Ибрая Алтынсарина" }) }
    };

    // Маршруты только с пересадкой
    var indirectRoutes = new HashSet<(string From, string To)>(new CaseInsensitiveTupleComparer())
        {
            ("Москва", "Торонто"),
            ("Владивосток", "Торонто"),
            ("Санкт-Петербург", "Торонто"),
            ("Сиань", "Торонто"),
            ("Алматы", "Торонто"),
            ("Караганда", "Торонто"),
            ("Шанхай", "Монреаль"),
            ("Сиань", "Минск"),
            ("Алматы", "Гомель"),
            ("Караганда", "Квебек"),
            ("Торонто", "Москва"),
            ("Торонто", "Владивосток"),
            ("Торонто", "Санкт-Петербург"),
            ("Торонто", "Сиань"),
            ("Торонто", "Алматы"),
            ("Торонто", "Караганда"),
            ("Монрель", "Шанхай"),
            ("Минск", "Сиань"),
            ("Гомель", "Алматы"),
            ("Квебек", "Караганда"),
            ("Тараз", "Оттава"),
            ("Пекин", "Грондо"),
            ("Сочи", "Могилев"),
            ("Костанай", "Владивосток"),
        };

    // Ввод городов
    Console.WriteLine("Город отправления:");
        string city1 = NormalizeCity(Console.ReadLine(), database);
    Console.WriteLine("Город прибытия:");
        string city2 = NormalizeCity(Console.ReadLine(), database);

        // Проверяем, существуют ли города в базе
        if (city1 == null || city2 == null)
        {
            Console.WriteLine("Один или оба города не найдены в базе данных.");
            return;
        }

// Проверяем, требуется ли пересадка для маршрута
bool isIndirect = indirectRoutes.Contains((city1, city2));

// Выбор типа билета
string ticketInfo;
if (isIndirect)
{
    ticketInfo = "На данном маршруте доступны только рейсы с пересадкой.";
}
else
{
    Console.WriteLine("Выберите тип билета (1 - Прямой, 2 - С пересадкой):");
    string ticketType = Console.ReadLine();

    ticketInfo = ticketType switch
    {
        "1" => "Вы выбрали прямой рейс.",
        "2" => "Вы выбрали рейс с пересадкой.",
        _ => "Неверный выбор, отображаются оба варианта билета."
    };
}

// Вывод информации о маршруте
Console.WriteLine("\nВаш маршрут:");
Console.WriteLine($"Из {city1} ({database[city1].Country}) в {city2} ({database[city2].Country}).");
Console.WriteLine(ticketInfo);

// Места и блюда
Console.WriteLine("\nМеста и блюда, которые можно попробовать:");
Console.WriteLine($"{city1}:");
foreach (var item in database[city1].FoodAndPlaces)
{
    Console.WriteLine($"- {item}");
}

Console.WriteLine($"{city2}:");
foreach (var item in database[city2].FoodAndPlaces)
{
    Console.WriteLine($"- {item}");
}
    }

    static string NormalizeCity(string input, Dictionary<string, (string Country, List<string> FoodAndPlaces)> database)
{
    if (input == null) return null;
    foreach (var city in database.Keys)
    {
        if (string.Equals(city, input, StringComparison.OrdinalIgnoreCase))
        {
            return city;
        }
    }
    return null;
}
}

class CaseInsensitiveTupleComparer : IEqualityComparer<(string From, string To)>
{
    public bool Equals((string From, string To) x, (string From, string To) y)
    {
        return string.Equals(x.From, y.From, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(x.To, y.To, StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode((string From, string To) obj)
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(obj.From) ^
               StringComparer.OrdinalIgnoreCase.GetHashCode(obj.To);
    }
}
