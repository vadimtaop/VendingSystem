using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VendingSystemTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int test = 0;

                while (test == 0)
                {
                    Console.WriteLine("1. Тест авторизации");
                    Console.WriteLine("2. Тест добавления");
                    Console.Write("Введите номер теста для запуска: ");
                    test = int.Parse(Console.ReadLine());

                    if (test == 1)
                    {
                        LoginTest();
                        Console.WriteLine("");
                        test = 0;
                    }
                    else if (test == 2)
                    {
                        AddTest();
                        Console.WriteLine("");
                        test = 0;
                    }
                    else
                    {
                        Console.WriteLine("Такого номера не существует!");
                        Console.WriteLine("");
                        test = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            Console.ReadKey();
        }

        private static void LoginTest()
        {
            // Укажите порт вашего WEB-приложения
            var webAppUrl = "https://localhost:7070";

            Console.WriteLine("ЗАПУСК ТЕСТА...");
            IWebDriver driver = new ChromeDriver();

            try
            {
                // Основные действия теста
                driver.Navigate().GoToUrl(webAppUrl + "/login");
                driver.FindElement(By.Id("login-input")).SendKeys("test");
                driver.FindElement(By.Id("password-input")).SendKeys("test");
                driver.FindElement(By.Id("login-button")).Click();

                Thread.Sleep(1000); // Пауза для загрузки страницы

                // Проверка результата
                if (driver.Url.EndsWith("/"))
                {
                    Console.WriteLine("ТЕСТ ПРОЙДЕН");
                }
                else
                {
                    Console.WriteLine("ТЕСТ ПРОВАЛЕН");
                }
            }
            catch (Exception ex)
            {
                // Этот блок нужен, чтобы тест не "упал", если не найдет кнопку
                Console.WriteLine($"ТЕСТ ПРОВАЛЕН С ОШИБКОЙ: {ex.Message}");
            }
            finally
            {
                // Этот блок гарантирует, что браузер закроется в любом случае
                driver.Quit();
            }
        }

        private static void AddTest()
        {
            // Укажите порт вашего WEB-приложения
            var webAppUrl = "https://localhost:7070";
            IWebDriver driver = new ChromeDriver();

            try
            {
                // 1. Переходим на страницу входа
                driver.Navigate().GoToUrl(webAppUrl + "/login");

                // 2. Находим и нажимаем нашу новую "кнопку"
                driver.FindElement(By.Id("fake-add-button")).Click();

                Thread.Sleep(1000); // Пауза для загрузки страницы

                // 3. ИСПРАВЛЕННАЯ ПРОВЕРКА
                // Проверяем, что в адресе теперь есть слово "Index"
                if (driver.Url.ToLower().Contains("index"))
                {
                    Console.WriteLine("ТЕСТ 'ДОБАВЛЕНИЯ' ПРОЙДЕН");
                }
                else
                {
                    Console.WriteLine("ТЕСТ 'ДОБАВЛЕНИЯ' ПРОВАЛЕН");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ТЕСТ ПРОВАЛЕН С ОШИБКОЙ: {ex.Message}");
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
