using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class UsuarioRepositorio : IUsuariosRepositorio
    {

        private string CadenaConexion;

        public UsuarioRepositorio(string cadenaConexion)
        {
            CadenaConexion = cadenaConexion;
        }

        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }

        public async Task<bool> Actualizar(Usuario usuarios)
        {
            int resultado;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = "UPDATE usuario SET CodigoUsuario = @CodigoUsuario, Nombre = @Nombre, Clave = @Clave, Estado= @Estado,  TipoUsuario = @TipoUsuario WHERE CodigoUsuario = @CodigoUsuario;";
                resultado = await conexion.ExecuteAsync(sql, new { usuarios.CodigoUsuario, usuarios.Nombre, usuarios.Clave, usuarios.Estado, usuarios.TipoUsuario });

                return resultado > 0;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async  Task<bool> Eliminar(Usuario usuario)
        {
            int resultado;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = "DELETE FROM usuario WHERE CodigoUsuario = @CodigoUsuario";
                resultado = await conexion.ExecuteAsync(sql, new { usuario.CodigoUsuario });

                return resultado > 0;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<IEnumerable<Usuario>> GetLista()
        {
            IEnumerable<Usuario> lista = new List<Usuario>();

            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = "SELECT * FROM usuario;";
                lista = await conexion.QueryAsync<Usuario>(sql);
            }
            catch (Exception ex)
            {

            }
            return lista;
        }

        public  async Task<Usuario> GetPorcodigo(string codigoUsuario)
        {
            Usuario user = new Usuario();
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = "SELECT * FROM usuario WHERE CodigoUsuario = @CodigoUsuario; ";
                user = await conexion.QueryFirstAsync<Usuario>(sql, new {codigoUsuario});
            }
            catch (Exception ex)
            {
            }
            return user;

        }

        public async Task<bool> Nuevo(Usuario usuario)
        {
            int resultado;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = "INSERT INTO usuario (CodigoUsuario, Nombre, Clave, Estado, TipoUsuario, ) VALUES (@CodigoUsuario, @Nombre, @Clave , @Estado, @TipoUsuario)";
                resultado = await conexion.ExecuteAsync(sql, usuario);

                return resultado > 0;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
