using Tienda.Modelo;

namespace Tienda;

public partial class ConsultaIndividual : ContentPage
{
    private List<Cliente> listaClientes;
    public ConsultaIndividual()
    {
        InitializeComponent();
        Listeners();
    }

    private void MostrarElementoBuscado()
    {
        //obtiene el texto del elemento del editText
        var textoIntro = etBuscador.Text.ToLower();

        if (!string.IsNullOrWhiteSpace(textoIntro))
        {
        
            //en una lista nueva almacena el filtrado segun el texto introducitdo en el EditText
            var filtrado = listaClientes
                            .Where(cl => cl.nombreCompleto.ToLower().Contains(textoIntro)
                            || cl.ciudad.ToLower().Contains(textoIntro))
                            .ToList();

            //si hay elementos en que mostrar los botones se muestran, si no, no
            if (filtrado.Count == 0) MostrarBotones(false);
            else MostrarBotones(true);

            //pone la lista del filtrado en el CollectionView
            cvConsultaIndividual.ItemsSource = filtrado;
        }
    }
    private void Siguiente()
    {
        //cliente seleccionado
        Cliente clienteSeleccionado = cvConsultaIndividual.SelectedItem as Cliente;

        //si no hay ningunno seleccioado selecciona el priemro
        if (clienteSeleccionado == null && listaClientes.Count > 0)
        {

            cvConsultaIndividual.SelectedItem = listaClientes[0];
        }
        else
        {

            //indice del cliente seleccionado
            int indiceActual = listaClientes.IndexOf(clienteSeleccionado);

            //comprueba que hay alguno seleccionado y que el seleccionado es menor al tamaño de la lista
            if (indiceActual != -1 && indiceActual < listaClientes.Count - 1)
            {
                // Mueve al siguiente elemento
                cvConsultaIndividual.SelectedItem = listaClientes[indiceActual + 1];
                Desplazar(indiceActual + 1);
            }
            else if (indiceActual == listaClientes.Count - 1)
            {
                // Si es el último elemento, vuelve al primero (comportamiento cíclico)
                cvConsultaIndividual.SelectedItem = listaClientes[0];
                Desplazar(0);
            }
        }
    }

    private void Atras()
    {
        //alamcena cliente
        Cliente clienteSelect = cvConsultaIndividual.SelectedItem as Cliente;

        // si es nula la selcción y la lista no esta vacia
        if (clienteSelect == null && listaClientes.Count > 0)
        {
            //se pone en el 1º elemento
            cvConsultaIndividual.SelectedItem = listaClientes[0];
            Desplazar(0);
        }
        else
        {
            //obtiene el indice para acceder al elemento de la lista
            int indiceCliente = listaClientes.IndexOf(clienteSelect);

            //si el indice del cliente no es negativo (hay selección)
            if (indiceCliente > 0)
            {
                //se mueve al anterior
                cvConsultaIndividual.SelectedItem = listaClientes[indiceCliente - 1];
                Desplazar(indiceCliente - 1);
            }
            // si es 0 (si es el 1º)
            else if (indiceCliente == 0)
            {
                //se mueve al último (total de lista -1 porque son indices)
                cvConsultaIndividual.SelectedItem = listaClientes[listaClientes.Count - 1];
                Desplazar(listaClientes.Count - 1);
            }
        }
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        Mostrar();
        Inicializar();
    }
    private void MostrarBotones(bool activo)
    {
        btnSig.IsVisible = activo;
        btnAnterior.IsVisible = activo;
    }
    private void Desplazar(int indice)
    {
        //segun el indice la lista se mueve a la posicion hasta hacerlo visible y lo hace con desplazamiento suave
        cvConsultaIndividual.ScrollTo(indice, position: ScrollToPosition.MakeVisible, animate: true);
    }
    private void Mostrar()
    {
        MostrarBotones(true);
        cvConsultaIndividual.SelectedItem = null;
        cvConsultaIndividual.ItemsSource = listaClientes;

    }
    private void Listeners()
    {
        btnSig.Clicked += (s, e) => Siguiente();
        btnAnterior.Clicked += (s, e) => Atras();
        etBuscador.TextChanged += (s, e) => MostrarElementoBuscado();

    }
    private void Inicializar()
    {
        listaClientes = ClienteRepository.GetClientesTxt();
        cvConsultaIndividual.ItemsSource = listaClientes;
    }
}