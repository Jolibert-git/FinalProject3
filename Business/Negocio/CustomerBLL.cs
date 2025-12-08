using DataAccess.Data;
using Microsoft.Data.SqlClient;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Negocio
{
    public class CustomerBLL
    {
        private CustomerDAL _customerDAL;

        public CustomerBLL()
        {
            _customerDAL = new CustomerDAL();
        }



        
        public Customer GetCustomerByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                Console.WriteLine("Advertencia BLL: El código de cliente no puede estar vacío para la búsqueda.");
                return null;
            }

            try
            {
                return _customerDAL.GetCustomerByCode(code);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"ERROR en CustomerBLL al buscar cliente: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR inesperado en CustomerBLL al buscar cliente: {ex.Message}");
                throw;
            }
        }
        

        public bool InsertCustomer(Customer newCustomer)
        {
            if (newCustomer == null || string.IsNullOrWhiteSpace(newCustomer.CodeCustomer) || string.IsNullOrWhiteSpace(newCustomer.FullNameCustomer))
            {
                throw new ArgumentException("El cliente o sus campos Código y Nombre Completo son obligatorios.");
            }


            if (GetCustomerByCode(newCustomer.CodeCustomer) != null)
            {
                throw new InvalidOperationException($"El código de cliente '{newCustomer.CodeCustomer}' ya existe. Use la función de Modificar.");
            }

            try
            {
                return _customerDAL.InsertCustomer(newCustomer);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"ERROR en CustomerBLL al insertar cliente: {ex.Message}");
                throw;
            }
        }

     
        public bool UpdateCustomer(Customer existingCustomer)
        {
            if (existingCustomer == null || string.IsNullOrWhiteSpace(existingCustomer.CodeCustomer) || string.IsNullOrWhiteSpace(existingCustomer.FullNameCustomer))
            {
                throw new ArgumentException("El cliente o sus campos Código y Nombre Completo son obligatorios para la actualización.");
            }


            try
            {
                return _customerDAL.UpdateCustomer(existingCustomer);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"ERROR en CustomerBLL al actualizar cliente: {ex.Message}");
                throw;
            }
        }
    }
}
