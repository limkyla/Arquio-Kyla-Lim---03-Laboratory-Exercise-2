using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arquio__Lim_Kyla___03_Laboratory_Exercise_2
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;
        List<object> showProductList;
        public frmAddProduct()
        {
            showProductList = new List<object>();
            InitializeComponent();
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            String[] ListOfProductCategory =
                {
                "Beverages", "Bread/Bakery", "Canned/Jarred Goods", "Dairy", "Frozen Goods", "Meat", "Personal Care", "Other"
                };

            foreach (var item in ListOfProductCategory)
            {
                cbCategory.Items.Add(item);
            }
        }
        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                throw new StringFormatException("Please Enter a Valid Product Name");
            return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
                throw new NumberFormatException("Please Enter a Valid Quantity");
            return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                throw new CurrencyFormatException("Please Enter a Valid Price");
            return Convert.ToDouble(price);
        }

        private DataGridView GetGridViewProductList()
        {
            return gridViewProductList;
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = txtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
            catch (NumberFormatException nfe) 
            {
                MessageBox.Show(nfe.Message); 
            }
            catch (StringFormatException sfe) 
            { 
                MessageBox.Show(sfe.Message);
            }
            catch (CurrencyFormatException cfe) 
            { 
                MessageBox.Show(cfe.Message);
            }
            finally {  }
        }
    }

    class NumberFormatException : Exception
    { 
        public NumberFormatException(string varName) : base(varName) { }
    }
    class StringFormatException : Exception
    {
        public StringFormatException(string varName) : base(varName) { }
    }
    class CurrencyFormatException : Exception
    {
        public CurrencyFormatException(string varName) : base(varName) { }
    }
}
