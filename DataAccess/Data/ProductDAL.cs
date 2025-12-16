using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Model.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace DataAccess.Data
{
    public class ProductDAL
    {
        private readonly DBHelper dbHelper = new DBHelper();


        //validate existen product
        public bool ValidateExistencesProduct(string code)
        {
            using (SqlConnection conn = dbHelper.OpenConnection())
            {
                string storeProdcedure = "ValidatedProduct";

                SqlParameter[] parameter = new SqlParameter[]
                {
                new SqlParameter("@CodeProduct",code)
                };

                try
                {
                    return dbHelper.ValidateExisten(conn, storeProdcedure, parameter);
                }
                catch (Exception Ex)
                {
                    Console.WriteLine($"Problem in class ProductDAL in maybe it doesn't return value{Ex.Message}");
                    throw;

                }
            }
        }
        //private readonly string _connectionString = "Data Source=DESKTOP-OA67FE6\\SQLEXPRESS;DATABASE=GcompleteQuery;Integrated Security=True;TrustServerCertificate=True;";
        // 1. INSERTAR PRODUCTO 
        public bool InsertProduct(Product product)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeProduct", product.CodeProduct),
                    new SqlParameter("@nameProduct", product.NameProduct),
                    new SqlParameter("@priceProduct", product.PriceProduct),
                    new SqlParameter("@stockProduct", product.StockProduct),
                    new SqlParameter("@unitOfMeasure", product.UnitOfMeasure),
                    new SqlParameter("@expiryDateProduct", (object)product.ExpiryDateProduct ?? DBNull.Value),
                    new SqlParameter("@locationProduct", (object)product.LocationProduct ?? DBNull.Value),
                    new SqlParameter("@codeDistributor", product.CodeDistributor),
                    new SqlParameter("@costProduct", product.CostProduct),
                    new SqlParameter("@discountCostProduct", (object)product.DiscountCostProduct ?? DBNull.Value),
                    new SqlParameter("@dateInProduct", (object)product.DateInProduct ?? DBNull.Value),
                    new SqlParameter("@discountSellProduct", (object)product.DiscountSellProduct ?? DBNull.Value),
                    new SqlParameter("@lastPriceProduct", (object)product.LastPriceProduct ?? DBNull.Value),
                    new SqlParameter("@utilityProduct", (object)product.UtilityProduct ?? DBNull.Value),
                    new SqlParameter("@minimunExistenProduct", (object)product.MinimunExistenProduct ?? DBNull.Value),
                    new SqlParameter("@taxProduct", product.TaxProduct)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery("InsertProduct", parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al insertar producto: {ex.Message}");
                throw;
            }
        }

        // 2. ACTUALIZAR PRODUCTO 🔄
        public bool UpdateProduct(Product product)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeProduct", product.CodeProduct),
                    new SqlParameter("@nameProduct", product.NameProduct),
                    new SqlParameter("@priceProduct", product.PriceProduct),
                    new SqlParameter("@stockProduct", product.StockProduct),
                    new SqlParameter("@unitOfMeasure", product.UnitOfMeasure),
                    new SqlParameter("@expiryDateProduct", (object)product.ExpiryDateProduct ?? DBNull.Value),
                    new SqlParameter("@locationProduct", (object)product.LocationProduct ?? DBNull.Value),
                    new SqlParameter("@codeDistributor", product.CodeDistributor),
                    new SqlParameter("@costProduct", product.CostProduct),
                    new SqlParameter("@discountCostProduct", (object)product.DiscountCostProduct ?? DBNull.Value),
                    new SqlParameter("@dateInProduct", (object)product.DateInProduct ?? DBNull.Value),
                    new SqlParameter("@discountSellProduct", (object)product.DiscountSellProduct ?? DBNull.Value),
                    new SqlParameter("@lastPriceProduct", (object)product.LastPriceProduct ?? DBNull.Value),
                    new SqlParameter("@utilityProduct", (object)product.UtilityProduct ?? DBNull.Value),
                    new SqlParameter("@minimunExistenProduct", (object)product.MinimunExistenProduct ?? DBNull.Value),
                    new SqlParameter("@taxProduct", product.TaxProduct)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery("UpdateProduct", parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al actualizar producto: {ex.Message}");
                throw;
            }
        }


        // 3. LEER PRODUCTOS CON FILTROS (ReadProduct) 📋
        public List<Product> ReadProducts(string codeProduct = null, string nameProduct = null)
        {
            List<Product> products = new List<Product>();
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeProduct", string.IsNullOrEmpty(codeProduct) ? (object)DBNull.Value : codeProduct),
                    new SqlParameter("@nameProduct", string.IsNullOrEmpty(nameProduct) ? (object)DBNull.Value : nameProduct)
                };

                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "ReadProduct", parameters);

                while (reader.Read())
                {
                    products.Add(MapProductFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al leer productos: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();

                if (connection != null && connection.State == ConnectionState.Open)
                {
                    dbHelper.CloseConnection();
                }
            }

            return products;
        }


        // 4. OBTENER PRODUCTO POR ID (INDIVIDUAL) 🔍

        public Product GetProductById(string codeProduct)
        {
            List<Product> products = ReadProducts(codeProduct, null);
            return products.Count > 0 ? products[0] : null;
        }

        // 5. MÉTODO DE MAPEO 🗺️

        private Product MapProductFromReader(SqlDataReader reader)
        {
            object GetValueOrDefault(string columnName)
            {
                int colIndex = reader.GetOrdinal(columnName);
                return reader.IsDBNull(colIndex) ? null : reader.GetValue(colIndex);
            }

            Product product = new Product
            {
                // 1. Mapeo de columnas con el Case (Mayúscula/Minúscula) CORRECTO
                CodeProduct = GetValueOrDefault("CodeProduct")?.ToString(),
                NameProduct = GetValueOrDefault("NameProduct")?.ToString(),
                PriceProduct = Convert.ToDecimal(GetValueOrDefault("PriceProduct") ?? 0.00m),
                CostProduct = Convert.ToDecimal(GetValueOrDefault("CostProduct") ?? 0.00m), // Corregido el Case y movido
                StockProduct = Convert.ToDecimal(GetValueOrDefault("StockProduct") ?? 0.00m),
                TaxProduct = Convert.ToDecimal(GetValueOrDefault("TaxProduct") ?? 0.00m), // Corregido el Case y movido

                UnitOfMeasure = GetValueOrDefault("UnitOfMeasure")?.ToString(),

                // Conversión de fechas y opcionales
                ExpiryDateProduct = GetValueOrDefault("ExpiryDateProduct") == null ? (DateTime?)null : Convert.ToDateTime(GetValueOrDefault("ExpiryDateProduct")),
                LocationProduct = GetValueOrDefault("LocationProduct")?.ToString(),
                CodeDistributor = GetValueOrDefault("CodeDistributor")?.ToString(),

                DiscountCostProduct = GetValueOrDefault("DiscountCostProduct") == null ? (decimal?)null : Convert.ToDecimal(GetValueOrDefault("DiscountCostProduct")),
                DateInProduct = GetValueOrDefault("DateInProduct") == null ? (DateTime?)null : Convert.ToDateTime(GetValueOrDefault("DateInProduct")),
                DiscountSellProduct = GetValueOrDefault("DiscountSellProduct") == null ? (decimal?)null : Convert.ToDecimal(GetValueOrDefault("DiscountSellProduct")),
                LastPriceProduct = GetValueOrDefault("LastPriceProduct") == null ? (decimal?)null : Convert.ToDecimal(GetValueOrDefault("LastPriceProduct")),
                UtilityProduct = GetValueOrDefault("UtilityProduct")?.ToString(),

                // Corregido el Case para la última columna
                MinimunExistenProduct = GetValueOrDefault("MinimunExistenProduct") == null ? (decimal?)null : Convert.ToDecimal(GetValueOrDefault("MinimunExistenProduct")),

                // 2. Columna faltante en el mapeo, pero presente en el SP/Reader. Asumiendo que es BIT (bool o int)
                IsActive = Convert.ToInt32(GetValueOrDefault("IsActive") ?? 0) == 1
                /*
                CodeProduct = GetValueOrDefault("CodeProduct")?.ToString(),
                NameProduct = GetValueOrDefault("NameProduct")?.ToString(),
                PriceProduct = Convert.ToDecimal(GetValueOrDefault("PriceProduct") ?? 0.00m),
                StockProduct = Convert.ToDecimal(GetValueOrDefault("StockProduct") ?? 0.00m),
                UnitOfMeasure = GetValueOrDefault("UnitOfMeasure")?.ToString(),

                // Conversión de fechas y opcionales
                ExpiryDateProduct = GetValueOrDefault("ExpiryDateProduct") == null ? (DateTime?)null : Convert.ToDateTime(GetValueOrDefault("ExpiryDateProduct")),
                LocationProduct = GetValueOrDefault("LocationProduct")?.ToString(),
                CodeDistributor = GetValueOrDefault("CodeDistributor")?.ToString(),

                CostProduct = Convert.ToDecimal(GetValueOrDefault("CostProduct") ?? 0.00m),
                DiscountCostProduct = GetValueOrDefault("DiscountCostProduct") == null ? (decimal?)null : Convert.ToDecimal(GetValueOrDefault("discountCostProduct")),
                DateInProduct = GetValueOrDefault("DateInProduct") == null ? (DateTime?)null : Convert.ToDateTime(GetValueOrDefault("dateInProduct")),
                DiscountSellProduct = GetValueOrDefault("DiscountSellProduct") == null ? (decimal?)null : Convert.ToDecimal(GetValueOrDefault("discountSellProduct")),
                LastPriceProduct = GetValueOrDefault("LastPriceProduct") == null ? (decimal?)null : Convert.ToDecimal(GetValueOrDefault("lastPriceProduct")),
                UtilityProduct = GetValueOrDefault("UtilityProduct")?.ToString(),
                MinimunExistenProduct = GetValueOrDefault("MinimunExistenProduct") == null ? (decimal?)null : Convert.ToDecimal(GetValueOrDefault("minimunExistenProduct")),
                TaxProduct = Convert.ToDecimal(GetValueOrDefault("TaxProduct") ?? 0.00m),
                IsActive = Convert.ToInt32(GetValueOrDefault("IsActive") ?? 0) == 1
                */
            };

            return product;
        }



        //6. Get it all product, used more by search product
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            SqlConnection conn = dbHelper.OpenConnection();//Open Connetion with method
                                                           //SqlDataReader reder = null; 

            // 1. Consulta SQL COMPLETA (Esta parte es correcta)
            /*string sqlQuery = @"
             SELECT 
                      CodeProduct, NameProduct, PriceProduct, CostProduct, StockProduct, TaxProduct, 
                      ExpiryDateProduct, IsActive, UnitOfMeasure, CodeDistributor, 
                      LocationProduct, DiscountCostProduct, DateInProduct, DiscountSellProduct, 
                      LastPriceProduct, UtilityProduct, MinimunExistenProduct 
                      FROM Product 
                      WHERE IsActive = 1; 
                         ";*/

            try
            {
                // 2. USO DE 'using' para asegurar que la conexión y el comando se cierren correctamente.
                // using (SqlConnection conn = new SqlConnection(_connectionString))
                //sqlQuery                                                                            //{
                using (SqlDataReader reader = dbHelper.ExecuteReader(conn, "GetAllProducts")) //ExecuteReader is a method about SqlCommand
                {
                    //conn.Open();

                    //using (SqlDataReader reader = cmd.ExecuteReader())
                    //{

                    while (reader.Read())//while there are something read
                    {
                        // 3. Mapeo complete
                        Product product = new Product
                        {
                            CodeProduct = reader["CodeProduct"].ToString(),
                            NameProduct = reader["NameProduct"].ToString(),
                            PriceProduct = Convert.ToDecimal(reader["PriceProduct"]),
                            CostProduct = Convert.ToDecimal(reader["CostProduct"]),
                            StockProduct = Convert.ToDecimal(reader["StockProduct"]),

                            TaxProduct = Convert.ToDecimal(reader["TaxProduct"]),
                            UnitOfMeasure = reader["UnitOfMeasure"] != DBNull.Value ? reader["UnitOfMeasure"].ToString() : string.Empty,
                            CodeDistributor = reader["CodeDistributor"] != DBNull.Value ? reader["CodeDistributor"].ToString() : string.Empty,
                            LocationProduct = reader["LocationProduct"] != DBNull.Value ? reader["LocationProduct"].ToString() : string.Empty,
                            UtilityProduct = reader["UtilityProduct"] != DBNull.Value ? reader["UtilityProduct"].ToString() : string.Empty,

                            ExpiryDateProduct = reader["ExpiryDateProduct"] != DBNull.Value ? (DateTime?)reader["ExpiryDateProduct"] : null,
                            DateInProduct = reader["DateInProduct"] != DBNull.Value ? (DateTime?)reader["DateInProduct"] : null,

                            DiscountCostProduct = reader["DiscountCostProduct"] != DBNull.Value ? (decimal?)reader["DiscountCostProduct"] : null,
                            DiscountSellProduct = reader["DiscountSellProduct"] != DBNull.Value ? (decimal?)reader["DiscountSellProduct"] : null,
                            LastPriceProduct = reader["LastPriceProduct"] != DBNull.Value ? (decimal?)reader["LastPriceProduct"] : null,
                            MinimunExistenProduct = reader["MinimunExistenProduct"] != DBNull.Value ? (decimal?)reader["MinimunExistenProduct"] : null,

                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        };
                        products.Add(product);
                    }
                    //}
                }
                // }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa DAL al obtener todos los productos.", ex);
            }
            return products;
        }



    }
}
