using System.Threading.Tasks;
using Tienda.Modelo;

namespace Tienda;

public partial class Altas : ContentPage
{
    private List<Entry> listaEntradas;
    private List<String> listaDatosAlta;
    private List<Cliente> listaClientes;
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

    //modifica lista
    private async void onClickAnyadir(object sender, EventArgs e)
    {
        cliente = await CrearCliente();

        if (cliente == null) return;

        if (!listaClientes.Contains(cliente))
        {
            //añade solo a la lista del programa, porque al darle al boton de guardar se le pasa la lista, no hay que modificar el repositorio
            listaClientes.Add(cliente);

            //await DisplayAlert("Correcto", "El cliente se ha guardado correctamente", "Volver");
            await DisplayAlert("Correcto", cliente.ToString(), "Volver");

        }
        else await DisplayAlert("Error", $"El cliente {cliente.correo} ya esta registrado", "Volver");
    }

    //modifica clientes de la lista, se busca a traves de correo
    private async void onClickModificar(object sender, EventArgs e)
    {
        //cliente con datos editados
        cliente = await CrearCliente();


        if (cliente != null)
        {
            if (!listaClientes.Contains(cliente)) await DisplayAlert("Error", "El cliente no existe", "Volver");
            else
            {
                int indice = listaClientes.FindIndex(c => c.Equals(cliente));
                listaClientes[indice] = cliente;
                await DisplayAlert("Correcto", $"Se ha actualizado el cliente {cliente.correo}", "Volver");
            }
        }
        else await DisplayAlert("Error", "No hay cliente seleccionado", "Volver");

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

    //guarda en el txt y la lista del repositorio la modifica
    private async void onClickGuardar(object sender, EventArgs e)
    {
        //alamcena en un booleano la respuesta del alert
        bool confirmacion =
             await DisplayAlert("Confirmación", "¿Desea guardarlo seguro en el fichero .txt?", "Si", "No");

        //gestion de la respuesta del alert
        if (confirmacion)
        {
            ClienteRepository.SaveAll(listaClientes);
            ActualizarListView();
        }
        else await DisplayAlert("Cancelado", "No se guardaron los cambios", "Volver");
    }

    //crea un cliente
    private async Task<Cliente> CrearCliente()
    {
        //limpia para que no se vayan acumulando
        listaDatosAlta.Clear();

        //si todos los campos estan completos empieza a crear
        if (TodosCamposCompletos())
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
        if (cliente != null && listaEntradas != null)
        {
            for (var i = 0; i < listaEntradas.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        listaEntradas[i].Text = cliente.nombre;
                        break;
                    case 1:
                        listaEntradas[i].Text = cliente.apellidos;
                        break;
                    case 2:
                        listaEntradas[i].Text = cliente.ciudad;
                        break;
                    case 3:
                        listaEntradas[i].Text = cliente.correo;
                        break;
                    default:
                        break;
                }
            }

            edComentario.Text = cliente.comentario;
            cbVip.IsChecked = cliente.vip;

        }
    }
    private void ActualizarListView()
    {
        //limpia por si acaso
        lvClientes.ItemsSource = null;

        //asigna la lista del Txt ya actualizada
        lvClientes.ItemsSource = ClienteRepository.GetClientesTxt();
    }

    //comprueba que los datos intro sean correctos, no sean nulos, etc..
    private bool TodosCamposCompletos()
    {
        //todos los campos Entry
        foreach (var entry in listaEntradas)
        {
            //si alguno de los campos esta vacio se devuelve al instante false
            if (string.IsNullOrWhiteSpace(entry.Text)) return false;

            //añade si esta todo bien
            listaDatosAlta.Add(entry.Text.Trim());

        }

        //Campo Editor (comentario) - si esta vacio false de inmediato
        if (string.IsNullOrWhiteSpace(edComentario.Text)) return false;

        //si todo esta bien añade el comentario y devuelve true
        listaDatosAlta.Add(edComentario.Text);
        return true;
    }

    private  void Limpiar()
    {
        foreach(var entry in listaEntradas)
        {
            entry.Text = null;
        }

        cbVip.IsChecked = false;
        edComentario.Text = string.Empty;
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
        listaEntradas = new List<Entry>
        {
          etNombre, etApellidos, etCiudad, etCorreo
        };

        listaDatosAlta = new List<string>();
        //obtiene los clientes del fichero
        listaClientes = ClienteRepository.GetClientesTxt();
        //pone en el listView la lista inicial
        lvClientes.ItemsSource = listaClientes;
    }

}