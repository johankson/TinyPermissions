# TinyPermissions

A swing at making stuff with users, roles and other stuff simpler...

## Why

This library solves the issue with filtering data based on roles. We are currently focusing on implementing the Entity Framework Core provider.

1. I have a collection of data (like Ducks)
2. Apply any number of filters to this data based on roles
3. Query the data based on roles

### Entity Framework Core specifics

Check out the ```TinyPermissions.Sample.WebApi``` project. It contains a SQLite database and a controller that uses the data. It fakes a user by setting  ```Thread.CurrentPrincipal``` to a fake user.

#### Steps to setup a ASP.NET Core WebApi project

The following steps are how the Sample Project is setup.

1. Install the ```TinyPermissions.EntityFrameworkCore``` nuget package.
2. Add the context in ```Startup.cs``` using the ```AddDbContextWithPermissions``` extension method.

    ```csharp
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();

        // Instead of service.AddDbContext
        services.AddDbContextWithPermissions<DuckContext>((obj) => obj.UseSqlite(
            Configuration.GetConnectionString("SampleDb"),
            b => b.MigrationsAssembly("TinyPermissions.Sample.WebApi")));
        }
    ```
3. Add some code to ```Configure(...)``` method in 
```startup.cs```.

    Note the third parameter ```DuckContext``` which is resolved by ASP.NET. 

    ```csharp
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, DuckContext context)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // do magic
        BasicPermissionService.SetBasicPermissions(context);

        app.UseMvc();
    }
    ```

4. Add a static class to handle the setup of permissions.

    This is the place where we define all filters for all roles.

    ```csharp
    public static class BasicPermissionService
    {
        public static void SetBasicPermissions(DuckContext context)
        {
            context.Ducks.AddRolePermissionFilter(
               "sales",
               (d, u) => d.Where(x => x.Owner.Username == u.Name));
        }
    }
    ```

#### How to use it

First get a context

```csharp
// Get or create a context
var context = new DuckContext();
```

On startup of your app, register filters based on the roles you define. The ```d``` parameter is the same ```DbSet``` as ```context.Ducks``` and the ```u```parameter is the current user (as ```IIdentity```) that will request the data.

```csharp
 context.Ducks.AddRolePermissionFilter(
               "sales",
               (d, u) => d.Where(x => x.Owner.Username == u.Name));
```

To filter all data based on the sales role you have, simply use the ```WithRole(role)``` extension method. The code below will only allow you to query data where the ducks owner is the current user.

```csharp 
// Filter the data based on the role
_context.Ducks.WithRole("sales").ToList();
```

The user is the current user of the thread.

```csharp
Thread.CurrentPrincipal.Identity
```