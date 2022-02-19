using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;

namespace ProyectoWebG5_19.Clases
{
    public class BO_Usuario
    {
        //Atributos
        private string USUARIO;
        private string NOMBRE;
        private string APELLIDO;
        private string CLAVE;
        private int ROL;
        private string ACTIVO;

        //Getters y Setters
        public string USUARIO1 { get => USUARIO; }  //Solo lectura
        public string NOMBRE1 { get => NOMBRE; set => NOMBRE = value; }
        public string APELLIDO1 { get => APELLIDO; set => APELLIDO = value; }
        public string CLAVE1 { set => CLAVE = value; }  //Solo escritura
        public int ROL1 { get => ROL; set => ROL = value; }
        public string ACTIVO1 { get => ACTIVO; set => ACTIVO = value; }

        public DataTable GetUsuarioLogin(string UsuarioId)
        {
            //Crear la conexión Oracle
            using (OracleConnection objConexion = new OracleConnection(ConnOracle.ConexionOracle(1))) //Cambiar parametro para no usar TNSNAME
            {
                try
                {
                    objConexion.Open(); //abriendo conexión Oracle
                    OracleCommand objComando = new OracleCommand();
                    objComando.Connection = objConexion;
                    objComando.CommandText = "G5_19.SEGURIDAD.p_INGRESO_USUARIO";   //Comando a ejecutar
                    objComando.CommandType = CommandType.StoredProcedure;           //Indicar que es un procedimiento almacenado
                    OracleParameter[] parm = new OracleParameter[2];
                    //Parametros
                    parm[0] = objComando.Parameters.Add("p_usuario", OracleDbType.Varchar2, UsuarioId, ParameterDirection.Input);
                    objComando.Parameters.Add("resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    objComando.ExecuteNonQuery();
                    //Llenado del DataSet
                    OracleDataAdapter dataAdapter = new OracleDataAdapter(objComando);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "ID_USUARIO");

                    if(dataSet.Tables[0].Rows.Count > 0)
                    {
                        return dataSet.Tables[0];   //Retorna una tabla
                    }
                    else
                    {
                        return null;    //Si no hay coincidencia, no retorna nada
                    }
                }
                catch (OracleException ex)
                {
                    throw ex;
                }
            }
        }
    }
}