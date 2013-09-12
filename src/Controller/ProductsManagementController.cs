using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Structures;
using TRPO.Model;
using TRPO.View;

namespace TRPO.Controller
{
    public class ProductsManagementController
    {
        ProductsManager productManager;
        IProductManagable view;
        User user;

        public ProductsManagementController(User u)
        {
            productManager = new ProductsManager();
            user = u;
        }

        public void setForm(IProductManagable f)
        {
            view = f;
        }

        public void updateProductsList()
        {
            view.setProductsList(productManager.getProductsLeft());
        }
    }
}
