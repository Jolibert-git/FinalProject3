using DataAccess.Data;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Negocio
{
    public class ProductBLL
    {
        private readonly ProductDAL _productDAL;
       

        public ProductBLL()
        {
            // Inicializa la Capa de Acceso a Datos
            _productDAL = new ProductDAL();
        }

        // -------------------------------------------------------------------
        // 1. Validaciones Centrales
        private void ValidateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.CodeProduct))
            {
                throw new ArgumentException("El código del producto es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(product.NameProduct))
            {
                throw new ArgumentException("El nombre del producto es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(product.CodeDistributor))
            {
                throw new ArgumentException("Debe especificar el código del distribuidor.");
            }
            if (product.PriceProduct <= 0)
            {
                throw new ArgumentException("El precio de venta debe ser un valor positivo.");
            }
            if (product.CostProduct <= 0)
            {
                throw new ArgumentException("El costo de compra debe ser un valor positivo.");
            }
            if (product.StockProduct < 0)
            {
                throw new ArgumentException("El stock inicial no puede ser negativo.");
            }
            if (product.TaxProduct < 0 || product.TaxProduct > 1.0m) // Asumiendo un impuesto máximo del 100% (1.0)
            {
                throw new ArgumentException("La tasa de impuesto (TaxProduct) debe ser un valor entre 0 y 1.");
            }

            // Opcional: Validar que la fecha de caducidad no sea pasada si se especifica
            if (product.ExpiryDateProduct.HasValue && product.ExpiryDateProduct.Value < DateTime.Today)
            {

            }
        }

        // -------------------------------------------------------------------
        // 2. INSERTAR PRODUCTO ➕

        public bool InsertProduct(Product product)
        {
            try
            {
                ValidateProduct(product);
                // Si la validación es exitosa, llama al DAL.
                return _productDAL.InsertProduct(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al insertar producto: {ex.Message}");
                throw new Exception("Error al guardar el nuevo producto. Detalle: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------
        // 3. ACTUALIZAR PRODUCTO 🔄

        public bool UpdateProduct(Product product)
        {
            try
            {

                ValidateProduct(product);

                return _productDAL.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al actualizar producto: {ex.Message}");
                throw new Exception("Error al actualizar los datos del producto. Detalle: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------
        // 4. LEER PRODUCTOS CON FILTROS 📋

        public List<Product> GetProducts(string codeProduct = null, string nameProduct = null)
        {
            try
            {
                // La BLL podría aplicar lógica de presentación o permisos aquí.
                // Por ejemplo, no mostrar productos con stock cero a menos que se solicite explícitamente.

                return _productDAL.ReadProducts(codeProduct, nameProduct);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al obtener productos: {ex.Message}");
                throw new Exception("Error al obtener el listado de productos. Detalle: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------
        // 5. OBTENER PRODUCTO POR ID (INDIVIDUAL) 🔍

        public Product GetProductById(string codeProduct)
        {
            

            try
            {
                if (string.IsNullOrWhiteSpace(codeProduct))
                {
                    throw new ArgumentException("El código de producto es necesario para la búsqueda.");
                }

                return _productDAL.GetProductById(codeProduct);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al obtener producto por ID: {ex.Message}");
                throw new Exception("Error al buscar el producto. Detalle: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------
        // 6. ELIMINAR/ANULAR PRODUCTO ❌ (Asume la existencia de un SP en el DAL)
        public bool DeleteProduct(string codeProduct)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codeProduct))
                {
                    throw new ArgumentException("Debe especificar el código del producto a eliminar.");
                }

                return true; // Retorno temporal hasta implementar el Delete en DAL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al eliminar producto: {ex.Message}");
                throw new Exception("Error al eliminar el producto. Detalle: " + ex.Message);
            }

            

        }



        public List<Product> GetAllProducts()
        {
            try
            {
                return _productDAL.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw new Exception("Error de negocio al intentar obtener el listado de productos.", ex);
            }
        }


    }
}
