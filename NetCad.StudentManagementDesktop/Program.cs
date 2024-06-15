using Autofac;
using NetCad.Data;
using NetCad.Data.Interfaces;
using NetCad.Services;
using NetCad.Services.Interfaces.Domain;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace NetCad.StudentManagementDesktop
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = ConfigureContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var scope = container.BeginLifetimeScope())
            {
                var mainForm = scope.Resolve<Form1>();
                Application.Run(mainForm);
            }
        }

        private static IContainer ConfigureContainer()
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            var builder = new ContainerBuilder();

            builder.RegisterType<SqlDataContext>().As<IDataContext>().SingleInstance().WithParameter("connectionString", connectionString);
            builder.RegisterType<StudentService>().As<IStudentService>().InstancePerLifetimeScope();
            builder.RegisterType<Form1>().AsSelf();

            return builder.Build();
        }
    }
}
