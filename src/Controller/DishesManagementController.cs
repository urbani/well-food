﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;
using TRPO.Model;
using TRPO.Structures;

namespace TRPO.Controller
{
    public class DishesManagementController
    {
        IDishManagable view;
        User user;
        DishesManager dishesManager;
        ProductsManager productManager;

        public DishesManagementController(User u)
        {
            user = u;
            dishesManager = new DishesManager();
            productManager = new ProductsManager();
        }

        public void setForm(IDishManagable f)
        {
            view = f;
        }

        public void updateDishInfo()
        {//TODO: Проверка хватает ли продуктов для приготовления заказа
            Dish tmpDish = dishesManager.getDish(view.getSelectedDishName());
            view.setDishInfo(tmpDish);
        }

        public void addReadyDishes()
        {
            int readyDishes = view.getReadyDishesAmount();
            String readyDish = view.getSelectedDishName();
            int redundantDishes = dishesManager.addReadyDishes(readyDish, readyDishes);
            if (redundantDishes > 0)
            {
                view.showMsg("Все заказы на блюдо [" + readyDish + "] закрыты. Осталось неучтенных блюд: [" + readyDish + " : " + redundantDishes + "шт.]", GlobalObj.ErrorLevels.Info);
            }
            updateRedundantDishesAmount();
        }

        
        public void addDishesFromStock()
        {
            int readyStockDishes = view.getReadyStockDishesAmount();
            String readyDish = view.getSelectedDishName();
            if (readyDish != "" && readyStockDishes > 0)
            {
                dishesManager.addReadyDishesFromStock(readyDish, readyStockDishes);
            }
        }

        public void updateRedundantDishesAmount()
        {
            view.setRedundantDishes(dishesManager.getRedundantDishes(view.getSelectedDishName()));
        }

        public void fillDishProd()
        {
            view.setDishesList(dishesManager.getDishNamesWithTypes());
            view.setProductsList(productManager.getProdNames());
        }

        public void updateContents()
        {
            view.updateContents(dishesManager.getDishContents(view.getSelectedDishName()));
        }

        public void updateCreationDishInfo()
        {
            Dish tmpDish = dishesManager.getDish(view.getSelectedDishName());
            view.setDishInfo(tmpDish);
        }

        public void createNewDish()
        {
            Dish d = view.getCreatedDish();
            dishesManager.createNewDish(d);
            view.setDishesList(dishesManager.getDishNamesWithTypes());
        }

        public void updateDish()
        {
            Dish d = view.getCreatedDish();
            dishesManager.updateDish(d);
            view.setDishesList(dishesManager.getDishNamesWithTypes());
        }

        public void addProductToDish()
        {
            view.addProductToContence(view.getSelectedProductName(), 1);
        }

        public void updateDishPrice()
        {
            view.setDishCreationPrice(dishesManager.getDishPrice(view.getCreatedDish()));
        }

        public void updateAbleToCookDishes()
        {
            view.setAbleToCookDishes(dishesManager.getAbleToCookDishes(view.getSelectedDishName()));
        }

        public void addProduct()
        {
            String addingProdName = view.getAddingProductName();
            Double addingProdPrice = view.getAddingProductPrice();

            if (addingProdName != "")
            {
                productManager.addProduct(addingProdName, addingProdPrice);
                view.setProductsList(productManager.getProdNames());
            }
        }

        public void deleteProductFromDish()
        {
            dishesManager.deleteProductFromDish(view.getSelectedDishName(), view.getSelectedContenceName());
            updateContents();
        }
        public void checkoutOrder()
        {

        }
    }
}