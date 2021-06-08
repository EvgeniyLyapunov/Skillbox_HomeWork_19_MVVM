using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_RndNames
{
    public static class Names
    {
        /// <summary>
        /// Коллекция имён клиентов
        /// </summary>
        static public List<string> FirstN;

        /// <summary>
        /// Коллекция фамилий клиентов
        /// </summary>
        static public List<string> LastN;

        /// <summary>
        /// Коллекция первого элемента в названии юрлица
        /// </summary>
        static public List<string> FirstSomething;

        /// <summary>
        /// Коллекция второго элемента в названии юрлица
        /// </summary>
        static public List<string> SecondSomething;


        static Names()
        {
            FirstN = new List<string>(35)
            {
                "Антон", "Прокофий", "Сергей", "Сидор", "Вальдемар",
                "Григорий", "Кирилл", "Аркадий", "Нестор", "Фрол",
                "Пётр", "Эдуард", "Фёдор", "Игнат", "Илья",
                "Акакий", "Борис","Митрофан", "Арнольд", "Афанасий",
                "Серафим", "Андрей", "Никифор", "Валерий", "Василий",
                "Данила", "Иннокентий", "Александр", "Герман", "Роберт",
                "Маркус", "Иоанн", "Егор", "Максим", "Герасим"
            };

            LastN = new List<string>(35)
            {
                "Попов", "Семёнов", "Кржижановский", "Добров", "Поддубный",
                "Беляков", "Неустроев", "Фролов", "Пустыркин", "Ненашев",
                "Барабанов", "Мальков", "Борзычев", "Хрюнов", "Зачётнов",
                "Сестринский", "Борщевский", "Афиногенов", "Хворостовский", "Митин",
                "Сидоров", "Пустозвонов", "Мандельштам", "Кацман", "Кутузов",
                "Балаганов", "Достоевский", "Гельдзман", "Сивухин", "Бальшскаускас",
                "Беншталь", "Хачикян", "Рабинович", "Скоробогатько", "Бдыщев"
            };

            FirstSomething = new List<string>(30)
            {
                "Сбыт", "Авто", "Обл", "Транс", "Нефте",
                "Водопровод", "Глав", "Генерал", "Перво",
                "Строй", "Газ", "Фёрст", "Юником", "Кристалл",
                "Маркет", "Бьюти", "Рем", "Диджитал", "Крепёж",
                "Станко", "Тех", "Юг", "Север", "Запад",
                "Восток", "Строй", "Сельхоз", "Алмаз", "Инвест"
            };

            SecondSomething = new List<string>(30)
            {
                "Проект", "Регион", "База", "Стандарт", "Фирма",
                "Компани", "Трейд", "Сервис", "ТехПром", "Продакшн",
                "Форпост", "Люкс", "Системз", "Ресурс", "Капитал",
                "Элемент", "Зенит", "Монтаж", "Бетон", "Космополитен",
                "Эко", "Торг", "Снаб", "Комьюнити", "Инкорпорейтед",
                "Экспо", "Гарант", "Дистрибьют", "Старз", "Партнёр"
            };
        }

        /// <summary>
        /// Метод возращает случайное имя из базы имён
        /// </summary>
        /// <param name="random">ОбЪект класса Random</param>
        /// <returns>случайное имя из базы имён</returns>
        static public string PersonFirstName(Random random)
        {
            return FirstN[random.Next(0, 34)];
        }

        static public string PersonLastName(Random random)
        {
            return LastN[random.Next(0, 34)];
        }

        static public string OrganizationName(Random random)
        {
            return FirstSomething[random.Next(0, 29)] + SecondSomething[random.Next(0, 29)];
        }

    }
}
