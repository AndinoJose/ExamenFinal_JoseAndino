using CurrieTechnologies.Razor.SweetAlert2;
using ExamenFinal_JoseAndino.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace ExamenFinal_JoseAndino.Pages.Usuarios;

    partial class EditarUsser
    {
    [Inject] private IUsuarioServicio _usuarioServicio { get; set; }

    [Inject] NavigationManager _navigationManager { get; set; }

    [Inject] SweetAlertService Swal { get; set; }


    [Parameter] public string Codigo { get; set; }

    Usuario user = new Usuario();


    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(Codigo))
        {
            user = await _usuarioServicio.GetPorcodigo(Codigo);
        }
    }

    protected async Task Guardar()
    {
        if (string.IsNullOrEmpty(user.CodigoUsuario) || string.IsNullOrEmpty(user.Nombre) || string.IsNullOrEmpty(user.TipoUsuario) || user.TipoUsuario == "Seleccionar")
        {
            return;
        }

        bool edito = await _usuarioServicio.Actualizar(user);

        if (edito)
        {
            await Swal.FireAsync("Bien Hecho", "Usuario actualizado", SweetAlertIcon.Success);
        }
        else
        {

            await Swal.FireAsync("Error", "Funcion Fallida", SweetAlertIcon.Error);
        }
        _navigationManager.NavigateTo("/usser");
    }
    protected async Task Cancelar()
    {
        _navigationManager.NavigateTo("/usser");

    }
    protected async Task Eliminar()
    {

        bool elimino = false;

        SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
        {
            Title = "¿Seguro que quiere eliminar este usuario?",
            Icon = SweetAlertIcon.Question,
            ShowCancelButton = true,
            ConfirmButtonText = "Aceptar",
            CancelButtonText = "Cancelar",
        });

        if (!string.IsNullOrEmpty(result.Value))
        {
            elimino = await _usuarioServicio.Eliminar(user);

            if (elimino)
            {
                await Swal.FireAsync("Bien Hecho", "Usuario Eliminado", SweetAlertIcon.Success);
                _navigationManager.NavigateTo("/usser");
            }
            else
            {
                await Swal.FireAsync("Error", "No Se Pudo Eliminar El Usuario", SweetAlertIcon.Error);
            }
        }
    }   



}

enum Tipo
{
    Seleccionar,
    Administrador,
    Usuario,
}

