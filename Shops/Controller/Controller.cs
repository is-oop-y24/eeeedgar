using System;
using Shops.Entities;
using Shops.Services;
using Shops.UI;
using Spectre.Console;

namespace Shops.Controller
{
    public class Controller
    {
        private ShopManager _shopManager;
        private Shop _shop;
        private Customer _customer;

        private Controller(ShopManager shopManager)
        {
            _shopManager = shopManager;
        }

        public static Controller CreateInstance(ShopManager shopManager)
        {
            return new Controller(shopManager);
        }

        public void Run()
        {
            CheckShopManagerUiChoice(ShopManagerUi.Menu());
        }

        private void CheckShopManagerUiChoice(string choice)
        {
            switch (choice)
            {
                case "Register Shop":
                {
                    RegisterShop();
                    break;
                }

                case "Register Product":
                {
                    RegisterProduct();
                    break;
                }

                case "Register Customer":
                {
                    RegisterCustomer();
                    break;
                }

                case "Shop List":
                {
                    ShopList();
                    break;
                }

                case "Customer List":
                {
                    CustomerList();
                    break;
                }

                case "Product List":
                {
                    ProductList();
                    break;
                }

                case "Select Shop":
                {
                    SelectShop();
                    break;
                }

                case "Select Customer":
                {
                    SelectCustomer();
                    break;
                }

                case "Exit":
                {
                    break;
                }

                default:
                {
                    throw new Exception("input error");
                }
            }
        }

        private void CheckShopUiChoice(string choice)
        {
            switch (choice)
            {
                case "Stock":
                {
                    Stock();
                    break;
                }

                case "Add Position":
                {
                    AddPositionToShop();
                    break;
                }

                case "Make Delivery":
                {
                    MakeDelivery();
                    break;
                }

                case "Set Price":
                {
                    SetPrice();
                    break;
                }

                case "Back to Shop Manager":
                {
                    BackToShopManager();
                    break;
                }

                default:
                {
                    throw new Exception("input error");
                }
            }
        }

        private void CheckPersonUiChoice(string choice)
        {
            switch (choice)
            {
                case "Show Shop Stock":
                {
                    ShowShopStock();
                    break;
                }

                case "Buy":
                {
                    Buy();
                    break;
                }

                case "Back to Shop Manager":
                {
                    BackToShopManager();
                    break;
                }

                default:
                {
                    throw new Exception("input error");
                }
            }
        }

        private void RegisterShop()
        {
            string shopName = Clarifier.AskString("Shop Name");
            string shopAddress = Clarifier.AskString("Shop Address");
            AnsiConsole.Clear();
            _shopManager.RegisterShop(shopName, shopAddress);
            CheckShopManagerUiChoice(ShopManagerUi.Menu());
        }

        private void RegisterProduct()
        {
            string productName = Clarifier.AskString("Product Name");
            AnsiConsole.Clear();
            _shopManager.RegisterProduct(productName);
            CheckShopManagerUiChoice(ShopManagerUi.Menu());
        }

        private void RegisterCustomer()
        {
            string customerName = Clarifier.AskString("Customer Name");
            int customerBalance = Clarifier.AskNumber("Customer Balance");
            AnsiConsole.Clear();
            _shopManager.RegisterCustomer(customerName, customerBalance);
            CheckShopManagerUiChoice(ShopManagerUi.Menu());
        }

        private void ShopList()
        {
            ShopManagerUi.DisplayShops(_shopManager.Shops);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            CheckShopManagerUiChoice(ShopManagerUi.Menu());
        }

        private void CustomerList()
        {
            ShopManagerUi.DisplayPersons(_shopManager.Customers);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            CheckShopManagerUiChoice(ShopManagerUi.Menu());
        }

        private void ProductList()
        {
            ShopManagerUi.DisplayProducts(_shopManager.Products);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            CheckShopManagerUiChoice(ShopManagerUi.Menu());
        }

        private void SelectShop()
        {
            ShopManagerUi.DisplayShops(_shopManager.Shops);
            int shopId = Clarifier.AskNumber("Shop Id");
            AnsiConsole.Clear();
            _shop = _shopManager.GetShop(shopId);
            CheckShopUiChoice(ShopUi.Menu(_shop.Name, _shop.Address));
        }

        private void SelectCustomer()
        {
            ShopManagerUi.DisplayPersons(_shopManager.Customers);
            int customerId = Clarifier.AskNumber("Customer Id");
            AnsiConsole.Clear();
            _customer = _shopManager.GetCustomer(customerId);
            CheckPersonUiChoice(PersonUi.Menu(_customer.Name));
        }

        private void Stock()
        {
            ShopUi.DisplayStock(_shop.Name, _shop.Address, _shop.Stock);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            CheckShopUiChoice(ShopUi.Menu(_shop.Name, _shop.Address));
        }

        private void AddPositionToShop()
        {
            ShopManagerUi.DisplayProducts(_shopManager.Products);
            int productId = Clarifier.AskNumber("Product Id");
            AnsiConsole.Clear();
            _shop.AddPosition(productId);
            CheckShopUiChoice(ShopUi.Menu(_shop.Name, _shop.Address));
        }

        private void MakeDelivery()
        {
            ShopUi.DisplayStock(_shop.Name, _shop.Address, _shop.Stock);
            int productId = Clarifier.AskNumber("Product Id");
            int productAmount = Clarifier.AskNumber("Product Amount");
            AnsiConsole.Clear();
            _shop.AddProducts(productId, productAmount);
            CheckShopUiChoice(ShopUi.Menu(_shop.Name, _shop.Address));
        }

        private void SetPrice()
        {
            ShopUi.DisplayStock(_shop.Name, _shop.Address, _shop.Stock);
            int productId = Clarifier.AskNumber("Product Id");
            int productPrice = Clarifier.AskNumber("New Price");
            _shop.SetProductPrice(productId, productPrice);
            AnsiConsole.Clear();
            CheckShopUiChoice(ShopUi.Menu(_shop.Name, _shop.Address));
        }

        private void BackToShopManager()
        {
            AnsiConsole.Clear();
            CheckShopManagerUiChoice(ShopManagerUi.Menu());
        }

        private void ShowShopStock()
        {
            ShopManagerUi.DisplayShops(_shopManager.Shops);
            int shopId = Clarifier.AskNumber("Shop Id");
            AnsiConsole.Clear();
            _shop = _shopManager.GetShop(shopId);
            ShopUi.DisplayStock(_shop.Name, _shop.Address, _shop.Stock);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            CheckPersonUiChoice(PersonUi.Menu(_customer.Name));
        }

        private void Buy()
        {
            ShopManagerUi.DisplayShops(_shopManager.Shops);
            int shopId = Clarifier.AskNumber("Shop Id");
            AnsiConsole.Clear();
            Shop shop = _shopManager.GetShop(shopId);
            ShopUi.DisplayStock(shop.Name, shop.Address, shop.Stock);
            int productId = Clarifier.AskNumber("Product Id");
            int productAmount = Clarifier.AskNumber("Product Amount");
            AnsiConsole.Clear();
            _shopManager.MakeDeal(_customer, shop, productId, productAmount);
            CheckPersonUiChoice(PersonUi.Menu(_customer.Name));
        }
    }
}