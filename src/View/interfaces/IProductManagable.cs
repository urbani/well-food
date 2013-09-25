using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.Structures;

namespace TRPO.View
{
    public interface IProductManagable : IInterractable
    {
        void setProductsList(List<ProductListEntry> plist);
        void setReqProductsList(List<ProductListEntry> plist);
        void clearLists();
        List<ProductListEntry> getBoughtProducts();
        ProductListEntry getSelectedProd();
        void addProductToBuyList(ProductListEntry p);
    }
}
