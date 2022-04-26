using ExamenFinal_JoseAndino.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace ExamenFinal_JoseAndino.Pages.Usuarios;

partial class Usser
{
    [Inject] private IUsuarioServicio _usuarioServicio { get; set; }

    private IEnumerable<Usuario> usuariosLista { get; set; }

    protected override async Task OnInitializedAsync()
    {
        usuariosLista = (IEnumerable<Usuario>)await _usuarioServicio.GetLista();
    }
}