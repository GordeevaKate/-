using BusinessLogic.Interfaces;
using Database.Implements;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace Практика
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<INapravlenie, NapravlenieLogic>(new
           HierarchicalLifetimeManager());
          currentContainer.RegisterType<IStudent, StudentLogic>(new
           HierarchicalLifetimeManager());
            return currentContainer;
           
        }
    }
}
