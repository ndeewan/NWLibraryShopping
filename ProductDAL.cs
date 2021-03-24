using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWLibraryShopping
{
   public class ProductDAL
    {

        public Products GetProduct(int id)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["NWCnString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from products where productid=" + id, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Products p = new Products();
            p.ProductID = id;
            p.ProductName = dr[1].ToString();
            p.UnitPrice = Convert.ToDouble(dr["UnitPrice"]);
                   cn.Close();
            return p;
        }


        //Display list of all the products in Products table
        public List<Products> GetProducts()
        {
            List<Products> prodlist = new List<Products>();
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["NWCnString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from products", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Products prod = new Products();
                prod.ProductID = Convert.ToInt32(dr["ProductID"]);
                prod.ProductName = dr["ProductName"].ToString();
                prod.UnitPrice = Convert.ToDouble(dr["UnitPrice"]);
                
                prodlist.Add(prod);
            }
            cn.Close();
            return prodlist;
        }

        //User will add product to the cart
        public void AddProduct(Products prodata)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["NWCnString"].ConnectionString);

            //AddProduct is a stored procedure that will add records in ProductCart Table

            SqlCommand cmd = new SqlCommand("AddProduct", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@proid", prodata.ProductID);
            cmd.Parameters.AddWithValue("@proname", prodata.ProductName);
            cmd.Parameters.AddWithValue("@price", prodata.UnitPrice);
            cmd.Parameters.AddWithValue("@qty", prodata.Quantity);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        //Display the content of ProductCart table
        public List<Products> GetProductCart()
        {
            List<Products> prodlist = new List<Products>();
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["NWCnString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from ProductCart", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Products prod = new Products();
                prod.ProductID = Convert.ToInt32(dr["ProductID"]);
                prod.ProductName = dr["ProductName"].ToString();
                prod.UnitPrice = Convert.ToDouble(dr["UnitPrice"]);
                prod.Quantity = Convert.ToInt32(dr["Quantity"]);
                prodlist.Add(prod);
            }
            cn.Close();
            return prodlist;
        }

        //Remove Products from the Cart
        public void DeleteProduct(int pid)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["NWCnString"].ConnectionString);

            //DeleteProduct is a stored procedure
            SqlCommand cmd = new SqlCommand("DeleteProduct", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@proid", pid);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }



    }
}
