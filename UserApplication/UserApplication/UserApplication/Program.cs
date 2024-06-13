using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace UserApplication
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Инициализируем HttpClient для работы с сервером
            HttpClient httpClient = new HttpClient();

            // Создаем экземпляр сервиса текущего пользователя
            var currentUserService = new CurrentUserService();

            // Показываем форму авторизации
            using (var loginForm = new Login(currentUserService, httpClient))
            {
                var result = loginForm.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }
            }

            // Запускаем главное окно приложения
            Application.Run(new Form1(httpClient));
        }
    }
}
