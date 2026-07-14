using System.Threading.Tasks;
using Tienda.DAO;
using Tienda.Modelo;

namespace Tienda;

public partial class Consulta : ContentPage
{
    // listas que se mostraran en los pickers
    private static List<string> listaCiudades;
    private static List<string> listaVIP;

    private static List<Cliente> listaClientes;
    public Consulta()
    {
        InitializeComponent();

    }

    //solo se utiliza el método cuando es disparado por el listener de los Picker o cuando se elimina un cliente
    private void SetDatosCollection()
    {
        //almacenar los indices del valor seleccionado en los Pickers
        var indCiu = pkCiudad.SelectedIndex;
        var indVip = pkVip.SelectedIndex;

        //para no modificar la lista original
        IEnumerable<Cliente> filtrado = listaClientes;

        //Asegura que hay opcion seleccionada
        if (indCiu != -1)
        {
            string ciudad = listaCiudades[indCiu];
            //actualiza la lista de filtrado segun la ciudad
            filtrado = filtrado.Where(c => c.ciudad == ciudad);
        }

        //Asegura que hay opcion seleccionada
        if (indVip != -1)
        {
            //si es 0, es Vip 
            bool esVip = (indVip == 0);

            //actualiza la lista de filtrado segun si es Vip
            filtrado = filtrado.Where(c => c.vip == esVip);
        }

        //pone la lista doblemente filtrada
        cvClientes.ItemsSource = filtrado.ToList();
    }
    private void AsignarPicker()
    {
        //asignar datos a las List de los picker
        listaCiudades = listaClientes
            .Select(c => c.ciudad)
            .Distinct()
            .ToList();

        //asignar lista a Picker
        pkCiudad.ItemsSource = listaCiudades;
        pkVip.ItemsSource = listaVIP;

    }

    // Listener para borrar a un cliente
    private async void OnBorrarClick(object sender, EventArgs e)
    {
        // SwipeItem que contiene el objeto del Binding
        var seleccion = sender as SwipeItem;
        var cliente = seleccion.BindingContext as Cliente;

        bool confirmacion = await DisplayAlert("Eliminar", "¿Seguro que desea eliminar?", "Confirmar", "Cancelar");

        if (confirmacion)
        {
            if (seleccion != null)
            {
                if (!listaClientes.Contains(cliente)) await DisplayAlert("Error", "El cliente no existe", "Volver");
                else
                {
                    listaClientes.Remove(cliente);
                    await DisplayAlert("Eliminado", $"El cliente {cliente.correo} ha sido eliminado ", "Volver");
                    SetDatosCollection();
                }
            }
            else await DisplayAlert("Error", "No hay cliente seleccionado", "Volver");
        }
        else await DisplayAlert("Cancelado", "No se ha elimnado nada", "Volver");
    }

    private void Listeners()
    {
        //asignar a los listeners de los Picker lo métodos que modifican el CollectionView
        //cada vez que el index cambia, que ejecute el método que cambia la lista segun los indices
        pkCiudad.SelectedIndexChanged += (s, e) => SetDatosCollection();
        pkVip.SelectedIndexChanged += (s, e) => SetDatosCollection();

        // limpia la seleccion de los pickers
        btnSinFiltros.Clicked += (s, e) =>
        {
            pkCiudad.SelectedItem = null;
            pkVip.SelectedItem = null;

        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Inicializar();
    }

    private async Task Inicializar()
    {
        listaCiudades = new List<string>();
        listaVIP = new List<string>
        {
            "Vip", "No VIP"
        };

        listaClientes = await DAOService.GetAllClientes();
        cvClientes.ItemsSource = listaClientes;

        AsignarPicker();
        Listeners();
    }
}