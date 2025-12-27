using Screen.Views;
using Microsoft.Extensions.DependencyInjection;
using Business.Negocio;
using DataAccess.Data;

namespace Screen
{
    internal static class Program
    {
        // El ServiceProvider es el "cerebro" que entrega los objetos ya armados
        public static IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /*
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmPrincipalScreen());
        }
        */
        [STAThread]
        static void Main()
        {
            // Establecer el manejador de excepciones global para el hilo de la UI
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            /*
            // Establecer el manejador de excepciones para otros hilos (opcional pero recomendado)
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            ApplicationConfiguration.Initialize();
            Application.Run(new FrmPrincipalScreen());
            */
            ApplicationConfiguration.Initialize();

            // 2. Configurar el Contenedor de Dependencias
            var services = new ServiceCollection();
            ConfigureServices(services);

            // 3. Construir el proveedor
            ServiceProvider = services.BuildServiceProvider();

            // 4. Iniciar la aplicación pidiendo el formulario al contenedor
            // Esto resuelve automáticamente el error CS7036
            var mainForm = ServiceProvider.GetRequiredService<FrmPrincipalScreen>();
            Application.Run(mainForm);

        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // --- Registro de Capas (Basado en tus carpetas) ---

            // Datos y faltan clases
            services.AddSingleton<DBHelper>();
            services.AddTransient<CustomerDAL>();
            services.AddTransient<ProductDAL>();
            services.AddTransient<InvoiceDAL>();
            services.AddTransient<DistributorDAL>();

            // Negocio
            services.AddTransient<CustomerBLL>();
            services.AddTransient<ProductBLL>();
            services.AddTransient<InvoiceBLL>();
            services.AddTransient<DistributorBLL>();

            // Vistas (Formularios)
            services.AddSingleton<FrmPrincipalScreen>(); // Singleton para el principal
            services.AddTransient<FrmInvoiceCreation>(); // Transient para que sean nuevos cada vez
            services.AddTransient<FrmSelecCodeProduct>();
            //services.AddTransient<FrmCustomerEditor>();
            services.AddTransient<FrmProductEditor>();
            // Agrega aquí cualquier otro formulario que uses
        }



        // Manejador para las excepciones del hilo de la interfaz de usuario (UI)
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // Muestra la excepción de forma clara al usuario
            MessageBox.Show($"Error Fatal de la UI: {e.Exception.Message}\n\nStack Trace:\n{e.Exception.StackTrace}",
                            "Error Inesperado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            // La aplicación continuará o se cerrará, dependiendo de tu lógica, 
            // pero al menos verás el detalle.
        }

        // Manejador para excepciones de otros hilos (de fondo)
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            MessageBox.Show($"Error Fatal General: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                            "Error Inesperado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);

            if (e.IsTerminating)
            {
                // Si la CLR está terminando, es mejor no intentar continuar.
            }
        }
    } 
}