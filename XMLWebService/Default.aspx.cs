using System;
using System.Web.UI;

namespace XMLWebService
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Calculator calculator = new Calculator();
            Result.Text = calculator.Add(1,2).ToString();
        }
    }
}