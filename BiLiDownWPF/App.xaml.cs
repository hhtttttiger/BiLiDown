using BiLiPrometheus;
using BiLiPrometheus.Util;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WebApiClient;

namespace BiLiDownWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ServiceProvider serviceProvider { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var service = new ServiceCollection();

            ConfigurationService(service);

            serviceProvider = service.BuildServiceProvider();

            var mainView = serviceProvider.GetRequiredService<MainWindow>();
            mainView.Show();

            base.OnStartup(e);
        }

        /// <summary>
        /// 配置容器
        /// </summary>
        /// <param name="services"></param>
        private void ConfigurationService(ServiceCollection services)
        {
            //注入
            services.AddTransient(typeof(MainWindow));

            services.AddHttpApi<IBHttp>();
            services.AddHttpClient();

            services.AddTransient<HttpClientHelper>();
        }
    }
}
