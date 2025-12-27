using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Negocio.StockMovementBLL;
using Model.Entities;
using DataAccess.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Business.Negocio
{
    public class StockMovementBLL
    {
        private readonly StockMovementDAL _movementDAL;

        public StockMovementBLL(StockMovementDAL movementDAL)
        {
            // Inicializa la Capa de Acceso a Datos
            _movementDAL = movementDAL;
        }


        // -------------------------------------------------------------------
        // NUEVO: SOBRECARGA TRANSACCIONAL 🤝

        public int InsertStockMovement(StockMovement movement, SqlConnection connection, SqlTransaction transaction)
        {
            ValidateNewMovement(movement);
            try
            {
                return _movementDAL.InsertStockMovement(movement, connection, transaction);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"BLL Error: Falló la inserción transaccional de movimiento de stock. Producto: {movement.CodeProduct}.", ex);
            }
        }






        // -------------------------------------------------------------------
        // 1. Validaciones Centrales

        private void ValidateNewMovement(StockMovement movement)
        {
            if (string.IsNullOrWhiteSpace(movement.CodeProduct))
            {
                throw new ArgumentException("El código del producto es obligatorio para registrar un movimiento.");
            }
            if (movement.MovementQuantity <= 0)
            {

                throw new ArgumentException("La cantidad del movimiento debe ser un valor positivo (mayor que cero).");
            }
            if (string.IsNullOrWhiteSpace(movement.MovementType))
            {
                throw new ArgumentException("El tipo de movimiento (ENTRADA, SALIDA, AJUSTE) es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(movement.NameMovement))
            {
                throw new ArgumentException("El nombre descriptivo del movimiento es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(movement.Operation) || (movement.Operation != "E" && movement.Operation != "S" && movement.Operation != "A"))
            {

                throw new ArgumentException("La operación debe ser 'E' (Entrada), 'S' (Salida) o 'A' (Ajuste).");
            }

            if (movement.Operation == "S" && movement.MovementQuantity > 0)
            {
                // Si es salida, la cantidad debe ser convertida a negativa para el SP/DAL
                movement.MovementQuantity *= -1;
            }
            else if ((movement.Operation == "E" || movement.Operation == "A") && movement.MovementQuantity < 0)
            {
                // Si es entrada o ajuste (positivo), la cantidad debe ser positiva
                movement.MovementQuantity *= -1;
            }
        }


        // -------------------------------------------------------------------
        // 2. INSERTAR MOVIMIENTO DE STOCK ➕

        public int InsertStockMovement(StockMovement movement)
        {
            try
            {
                ValidateNewMovement(movement);
                return _movementDAL.InsertStockMovement(movement);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al insertar movimiento de stock: {ex.Message}");
                throw new Exception("Error en la lógica de negocio al registrar el movimiento. Detalle: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------
        // 3. ACTUALIZAR MOVIMIENTO DE STOCK 🔄
        public bool UpdateStockMovement(StockMovement movement)
        {
            try
            {
                if (movement.CodeMovement <= 0)
                {
                    throw new ArgumentException("El código de movimiento es inválido para la actualización.");
                }
                if (string.IsNullOrWhiteSpace(movement.NameMovement))
                {
                    throw new ArgumentException("El nombre descriptivo del movimiento es obligatorio para la actualización.");
                }


                return _movementDAL.UpdateStockMovement(movement);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al actualizar movimiento de stock: {ex.Message}");
                throw new Exception("Error en la lógica de negocio al actualizar el movimiento. Detalle: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------
        // 4. LEER MOVIMIENTOS CON FILTROS 📋

        public List<StockMovement> GetStockMovements(DateTime? startDate, DateTime? endDate, string codeProduct = null)
        {
            try
            {
                if (startDate.HasValue && endDate.HasValue && startDate.Value > endDate.Value)
                {
                    throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha de fin.");
                }

                return _movementDAL.ReadStockMovements(startDate, endDate, codeProduct);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al obtener movimientos de stock: {ex.Message}");
                throw new Exception("Error al obtener el historial de movimientos de stock. Detalle: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------
        // 5. OBTENER MOVIMIENTO POR ID (INDIVIDUAL) 🔍
        public StockMovement GetMovementById(int codeMovement)
        {
            try
            {
                if (codeMovement <= 0)
                {
                    throw new ArgumentException("El código de movimiento debe ser un valor positivo.");
                }

                return _movementDAL.GetMovementById(codeMovement);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al obtener movimiento por ID: {ex.Message}");
                throw new Exception("Error al buscar el movimiento por ID. Detalle: " + ex.Message);
            }
        }
    }
}
