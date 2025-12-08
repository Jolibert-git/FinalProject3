using Screen.Views;

namespace Screen
{
    internal static class Program
    {
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

            // Establecer el manejador de excepciones para otros hilos (opcional pero recomendado)
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            ApplicationConfiguration.Initialize();
            Application.Run(new FrmPrincipalScreen());
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