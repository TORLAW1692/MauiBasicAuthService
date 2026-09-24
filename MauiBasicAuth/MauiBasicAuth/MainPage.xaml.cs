using MauiBasicAuth.DataAccess;

namespace MauiBasicAuth;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = txtUserId.Text;
        string password = txtPassword.Text;

        var userAuth = new UserAuthentication();

        bool isAuthenticated =
            userAuth.AuthenticateUser(username, password);

        if (isAuthenticated)
        {
            await DisplayAlertAsync(
                "Success",
                "User authenticated successfully!",
                "OK");
        }
        else
        {
            await DisplayAlertAsync(
                "Error",
                "Invalid username or password.",
                "OK");
        }
    }
}