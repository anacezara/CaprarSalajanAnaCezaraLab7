using CaprarSalajanAnaCezaraLab7.Models;

namespace CaprarSalajanAnaCezaraLab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
        InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
       this.BindingContext)
        {
            BindingContext = new Product()
        });

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }

    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        var selectedItem = listView.SelectedItem as Product;
        if (selectedItem != null)
        {
            var shopl = (ShopList)BindingContext;
            await App.Database.DeleteListProductAsync(shopl.ID, selectedItem.ID);
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        }
    }

}