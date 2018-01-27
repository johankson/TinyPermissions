# TinyPermissions

A swing at making stuff with users, roles and other stuff simpler...

## Why

This is a pattern that I (Johan) usually use when creating role based authorization stuff.

What this library does is that it acts as a filter on your data.

### Entity Framework Core specifics

First get a context

```csharp
// Get or create a context
var context = new DuckContext();
```

On startup of your app, register filters based on the roles you define. The ```d``` parameter is the same ```DbSet``` as ```context.Ducks``` and the ```u```parameter is the current user that will request the data.

```csharp
 context.Ducks.AddRolePermissionFilter(
               "sales",
               (d, u) => d.Where(x => x.Owner.Username == u.Name));
```

To filter all data based on the sales role you have, simply use the ```WithRole```extension method.

```csharp 
// Filter the data based on the role
_context.Ducks.WithRole("sales").ToList();
```

