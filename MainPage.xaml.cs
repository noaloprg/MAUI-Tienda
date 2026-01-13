namespace Tienda
{
    public partial class MainPage : ContentPage
    {

        private const string FORMATO_FECHA = "dd/MM/yyyy \n HH:mm:ss";
        private static IDispatcherTimer timer;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GenerarFecha();
            Listeners();

        }

        private void GenerarFecha()
        {

            timer = Dispatcher.CreateTimer();

            timer.Interval = TimeSpan.FromSeconds(1);

            timer.Tick += (s, e) =>
            {
                lblFechaHora.Text = DateTime.Now.ToString(FORMATO_FECHA);
            };
            timer.Start();


        }

        private void Listeners()
        {
            swModoOscuro.Toggled += (s, e) =>
            {
                if (e.Value) lblFechaHora.IsVisible = false;
                else lblFechaHora.IsVisible = true;
            };
        }


    }
}
