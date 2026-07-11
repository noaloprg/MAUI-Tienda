using Tienda.DAO;

namespace Tienda
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

            InicializarBD();
        }

        // implementa el metodo del servicio de la BD para crear las tablas
        private async void InicializarBD()
        {
            await DAOService.CrearTablas();
        }

    }
}