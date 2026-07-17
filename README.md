# Spray and Sole — Gestor de Tienda

Aplicación móvil/multiplataforma desarrollada en **.NET MAUI** para la gestión de clientes de una tienda de calzado. Permite dar de alta, consultar, filtrar y eliminar clientes, almacenando todo en una base de datos local **SQLite**.

## **Indice**

- [Tecnológias](#tecnologías)
- [Funcionalidades](#funcionalidades)
- [Estructura del proyecto](#estructura-del-proyecto)
- [Probar en local](#probar-en-local)

## **Tecnologías**

| Tecnología               | Detalle                                                                                                                    |
| ------------------------ | -------------------------------------------------------------------------------------------------------------------------- |
| **Framework**            | <img src="https://img.shields.io/badge/.NET_MAUI-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" height="30"> v9.0 |
| **Lenguaje**             | <img src="https://img.shields.io/badge/C%23-512BD4?style=for-the-badge&logo=csharp&logoColor=white" height="30">           |
| **Base de datos**        | <img src="https://img.shields.io/badge/SQLite-0F80CC?style=for-the-badge&logo=sqlite&logoColor=white" height="30">         |
| **Plataformas objetivo** | Android, iOS, Mac Catalyst, Windows                                                                                        |

## **Funcionalidades**

### **Pantalla de bienvenida**

Pantalla principal, con reloj en tiempo real (fecha y hora) y un switch para ocultarlo.

<img width="1720" height="1000" alt="home_page" src="https://github.com/user-attachments/assets/662d86a8-7041-4230-acba-b34c31184b98" />

### **Alta de clientes**

Formulario para añadir y actualizar clientes.
<img width="1720" height="1000" alt="image" src="https://github.com/user-attachments/assets/3ed27a37-36aa-4623-9d28-dfa8526a756c" />

### **Listado de clientes** (`Consulta`)

Tabla con todos los clientes, con filtros por **ciudad** y por condición **VIP / No VIP**, y opción de **eliminar** cliente mediante gesto de deslizar (`SwipeView`).

<img width="1720" height="1000" alt="image" src="https://github.com/user-attachments/assets/b885faff-d61c-4e0a-b748-f421f9454061" />

### **Consulta individual** (`ConsultaIndividual`)

Ficha de cliente tipo "carrusel", con imagen (icono VIP), buscador y botones de **Anterior / Siguiente** para navegar entre registros.
<img width="1720" height="1000" alt="image" src="https://github.com/user-attachments/assets/d6740048-f223-423e-a2c7-cd269d769c0a" />

### **Otros**

- **Validaciones y utilidades**: comprobación de campos vacíos, normalización de texto, validación de formato de correo electrónico, diálogos de confirmación/error, y conversión bool ↔ int para SQLite.
- **Persistencia local** en base de datos **SQLite**, creada automáticamente al iniciar la app.

## **Estructura del proyecto**

```
Tienda/
├── App.xaml / App.xaml.cs              # Recursos globales, estilos y arranque de la BD
├── AppShell.xaml / AppShell.xaml.cs     # Navegación por pestañas (Shell)
├── GlobalXmlns.cs                       # Definiciones globales de namespaces XAML
├── MauiProgram.cs                       # Configuración de la app y fuentes
├── MainPage.xaml / .xaml.cs             # Pantalla de bienvenida
├── Altas.xaml                           # Alta de clientes
├── Consulta.xaml / .xaml.cs             # Listado y filtrado de clientes
├── ConsultaIndividual.xaml / .xaml.cs   # Ficha individual navegable
├── DAO/
│   └── DAOService.cs                    # Acceso a datos /
creación de tablas SQLite
├── Services/
│   ├── BooleanService.cs                # Conversión bool <-> int (para SQLite)
│   ├── DialogService.cs                 # Diálogos de alerta y confirmación
│   └── EntryService.cs                  # Utilidades para controles Entry (validación, limpieza, normalización)
├── Convertidores/
│   └── VIPConverter.cs                  # Conversor de valor VIP a imagen/ícono
└── Resources/
    ├── Styles/ (Colors.xaml, Styles.xaml)
    ├── Fonts/
    └── Images/
```

## **Probar en local**

### Requisitos previos

- [Visual Studio 2022](https://visualstudio.microsoft.com/) (17.10+) con la carga de trabajo **.NET Multi-platform App UI development**, o Visual Studio Code con la extensión de .NET MAUI.
- **.NET 9 SDK**.
- Para compilar/depurar en cada plataforma:
  - **Android**: SDK de Android y un emulador o dispositivo físico.
  - **iOS / Mac Catalyst**: macOS con Xcode.
  - **Windows**: Windows 10 versión 17763 o superior.

### Instalación

1. Clona o descarga el repositorio.
2. Abre `Tienda.csproj` (o la solución `.sln`) con Visual Studio.
3. Restaura los paquetes NuGet (se restauran automáticamente al abrir el proyecto):
   - `Microsoft.Data.Sqlite`
   - `Microsoft.Maui.Controls`
   - `Microsoft.Extensions.Logging.Debug`
4. Selecciona la plataforma/dispositivo de destino (Android, Windows, etc.).
5. Ejecuta el proyecto (`F5` o botón "Iniciar").

Al arrancar, la aplicación crea automáticamente las tablas necesarias en la base de datos SQLite local (ver `App.xaml.cs` → `InicializarBD()`).
