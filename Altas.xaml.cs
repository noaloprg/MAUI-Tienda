using System.Threading.Tasks;
using Tienda.DAO;
using Tienda.Modelo;
using Tienda.Services;

namespace Tienda;

public partial class Altas : ContentPage
{
    // lista de los entry que se deben rellenar
    private List<Entry> listaCamposEntrada;

    // lista con los datos introducidos en los campos
    private List<String> listaDatosAlta;

    // lista de clientes que contiene la BD
    private List<Cliente> listaClientes;

    // cliente seleccionado
    private Cliente cliente;

    public Altas()
    {
        InitializeComponent();
        Inicializar();
        Listeners();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Limpiar();
    }

    // listener para AÑADIR cliente
    private async void onClickAnyadir(object sender, EventArgs e)
    {
        cliente = await CrearCliente();

        if (cliente == null) return;

        // verifica si el correo ya se encuentra registrado
        bool isRegistrado = listaClientes.Any(cl => string.Equals(cl.correo, cliente.correo));

        if (!isRegistrado)
        {
            var isCreado = await DAOService.ModificarInsertarCliente(cliente, true);

            if (isCreado)
            {
                await DisplayAlert("Correcto", "El cliente se ha guardado correctamente", "Volver");
                ActualizarListView();
            }
            else await DisplayAlert("Error de insercion", "Algo salió mal al intentar insertar. Vuelva a intentarlo", "Volver");
            //  else await DisplayAlert("Error de insercion", isCreado, "Volver");

        }
        else await DisplayAlert("Error", $"El cliente {cliente.correo} ya esta registrado", "Volver");
    }

    //modifica clientes de la lista, se busca a traves de correo
    private async void onClickModificar(object sender, EventArgs e)
    {
        var seleccion = lvClientes.SelectedItem;

        // comprueba que haya un cliente seleccionado
        if (seleccion == null) await DisplayAlert("Error", "No hay cliente seleccionado", "Volver");

        else
        {
            // en caso de que haya seleccionado uno se puede ya convertir en cliente
            Cliente clienteSeleccionado = seleccion as Cliente;

            // cliente en memoria con datos editados
            cliente = await CrearCliente();
            cliente.id = clienteSeleccionado.id;

            bool isActualizado = await DAOService.ModificarInsertarCliente(cliente, false);

            if (isActualizado)
            {
                await DisplayAlert("Correcto", $"Se ha actualizado el cliente {cliente.correo}", "Volver");
                ActualizarListView();
                Limpiar();
            }
            else await DisplayAlert("Error de actualizado", $"Algo salió mal. Vuelva a intentarlo", "Volver");
        }
    }

    //gestiona la seleccion de un cliente
    private void lvClientes_ItemSlected(object sender, SelectedItemChangedEventArgs e)
    {
        cliente = e.SelectedItem as Cliente;

        if (cliente != null)
        {
            AsignarAtributosEntry();
        }
    }

    //limpia los campos
    private void onClickLimpiar(object sender, EventArgs e)
    {
        Limpiar();
    }

    //crea un cliente
    private async Task<Cliente> CrearCliente()
    {
        //limpia para que no se vayan acumulando
        listaDatosAlta.Clear();

        //si todos los campos estan completos empieza a crear
        if (AlmacenarContenidoEntries())
        {
            var nomb = listaDatosAlta[0];
            var apell = listaDatosAlta[1];
            var ciu = listaDatosAlta[2];
            var email = listaDatosAlta[3];
            var coment = listaDatosAlta[4];

            return new Cliente
            {
                nombre = nomb,
                apellidos = apell,
                ciudad = ciu,
                correo = email,
                comentario = coment,
                vip = cbVip.IsChecked
            };
        }
        else
        {
            await DisplayAlert("Error", "Todos los campos deben estar rellenos", "Volver");
            return null;
        }
    }

    //asigna los atributos de un cliente a toods los Entry de la pantalla Alta
    private void AsignarAtributosEntry()
    {
        if (cliente != null && listaCamposEntrada != null)
        {
            for (var i = 0; i < listaCamposEntrada.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        listaCamposEntrada[i].Text = cliente.nombre;
                        break;
                    case 1:
                        listaCamposEntrada[i].Text = cliente.apellidos;
                        break;
                    case 2:
                        listaCamposEntrada[i].Text = cliente.ciudad;
                        break;
                    case 3:
                        listaCamposEntrada[i].Text = cliente.correo;
                        break;
                    default:
                        break;
                }
            }

            edComentario.Text = cliente.comentario;
            cbVip.IsChecked = cliente.vip;

        }
    }

    // acctualiza la lista lateral
    private async void ActualizarListView()
    {
        //limpia la lista para evitar valores sucios
        lvClientes.ItemsSource = null;

        //asigna la lista del Txt ya actualizada
        lvClientes.ItemsSource = await DAOService.GetAllClientes();
    }

    //Almacena los datos introducidos en los entries y hace comprobaciones
    private bool AlmacenarContenidoEntries()
    {
        bool isCorrecto = EntryService.ComprobarContenidoEntries(listaCamposEntrada);

        if (isCorrecto)
        {
            // Selecciona el texto de todos los campos una vez validados
            listaDatosAlta = listaCamposEntrada.Select(et => et.Text.Trim()).ToList();

            //Campo Editor (comentario) - si esta vacio false de inmediato
            if (string.IsNullOrWhiteSpace(edComentario.Text)) return false;

            //si todo esta bien añade el comentario y devuelve true
            listaDatosAlta.Add(edComentario.Text);
            return true;
        }
        else return false;
    }

    // Limpia los campos de entrada
    private void Limpiar()
    {

        EntryService.LimpiarContenidoEntries(listaCamposEntrada);

        cbVip.IsChecked = false;
        edComentario.Text = string.Empty;

        lvClientes.SelectedItem = null;
    }

    private void Listeners()
    {
        //checkBox de si es o no vip
        cbVip.CheckedChanged += (sender, args) =>
        {
            //comprueba si el checkbox esta clickado y pone una imágen y otra
            imgVip.Source = args.Value ? "vip.png" : "public.png";

        };
    }

    private void Inicializar()
    {
        //almacena todos los entrys
        listaCamposEntrada = new List<Entry>
        {
          etNombre, etApellidos, etCiudad, etCorreo
        };

        listaDatosAlta = new List<string>();
        listaClientes = new List<Cliente>();

        ActualizarListView();
    }

}