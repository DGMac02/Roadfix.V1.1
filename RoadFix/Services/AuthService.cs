using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using Supabase.Gotrue;



namespace RoadFix.Services// Matches your folder name

{
    public class AuthService
    {
        private readonly Supabase.Client _supabase;
        private readonly NavigationManager _nav;        
        public AuthService(Supabase.Client supabase, NavigationManager nav)
    {
        _supabase = supabase;
        _nav = nav;
    }

    public async Task<bool> RegisterUser(string email, string password, string firstName, string lastName)
{
    try
    {
        // Package the names into a dictionary so Supabase records them
        var metadata = new Dictionary<string, object>
        {
            { "first_name", firstName },
            { "last_name", lastName }
        };

        var options = new Supabase.Gotrue.SignUpOptions { Data = metadata };

        // Pass the options to the SignUp method
        var session = await _supabase.Auth.SignUp(email, password, options);
        
        return session?.User != null;
    }
    catch (Exception)
    {
        return false;
    }
}


    

    public async Task<Session?> Login(string email, string password)
    {
        try
        {
            var response = await _supabase.Auth.SignIn(email, password);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login Failed: {ex.Message}");
            return null;
        }
    }

    public async Task Logout()
    {
        try
        {
            // 1. Tell Supabase to destroy the session
            await _supabase.Auth.SignOut();

            // 2. Redirect the user immediately to the login page
            _nav.NavigateTo("login");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Logout Error: {ex.Message}");
        }
    }
}}