using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Structures;
using TRPO.Model;
using TRPO.View;
using TRPO.GlobalObj;

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

        public void updateReqProductsList()
        {
            view.setReqProductsList(productManager.getReqProducts());
        }

        public void addBoughtProducts()
        {
            List<ProductListEntry> tmp = null;
            try
            {
                tmp = view.getBoughtProducts();
            }
            catch (FormatException ex)
            {
                view.showMsg("Ошибка! Неверный формат данных!", ErrorLevels.Info);
            }
            if (tmp != null && tmp.Count > 0)
            {
                productManager.addIncomeProducts(tmp);
            }
            view.clearLists();
            updateProductsList();
            updateReqProductsList();
        }

        public void addProdToBuy()
        {
            view.addProductToBuyList(view.getSelectedProd());
        }
    }
}
