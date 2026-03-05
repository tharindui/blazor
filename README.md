# Blazor CRUD Learning App (.NET + Razor Components)

This repository contains a **simple but fully explained CRUD app** built with **.NET 8 Blazor (Razor Components)**.

The goal is to help you learn not just "how to make it work", but also **why each part exists**.

---

## 1) What you will learn

By reading and running this project, you will understand:

- How a Blazor app starts (`Program.cs` and DI registration).
- How Razor Components are structured (`App.razor`, `Routes.razor`, layouts, pages).
- How routing works with `@page`.
- How dependency injection works in components (`@inject`).
- How to build forms with `EditForm` + `InputText` + `InputNumber` + validation.
- How DataAnnotations validation works.
- How to implement CRUD with a service layer.
- How and when component state updates and re-renders.

---

## 2) Project structure

```text
.
├── BlazorCrudLearning.csproj
├── Program.cs
├── README.md
├── Components/
│   ├── App.razor
│   ├── Routes.razor
│   ├── _Imports.razor
│   ├── Layout/
│   │   ├── MainLayout.razor
│   │   └── NavMenu.razor
│   └── Pages/
│       ├── Home.razor
│       └── Products.razor
├── Models/
│   └── Product.cs
├── Services/
│   ├── IProductService.cs
│   └── InMemoryProductService.cs
└── wwwroot/
    └── app.css
```

---

## 3) Quick start

> Prerequisite: .NET 8 SDK installed.

```bash
dotnet restore
dotnet run
```

Then open the URL shown in terminal (usually `https://localhost:xxxx`).

---

## 4) Core Blazor concepts explained

## 4.1 Program startup (`Program.cs`)

This is where the host and services are configured:

- `AddRazorComponents()` enables Razor component rendering.
- `AddInteractiveServerComponents()` enables server interactivity.
- `AddScoped<IProductService, InMemoryProductService>()` registers our CRUD service.
- `MapRazorComponents<App>()` maps the component app to HTTP endpoints.

### Why DI matters

Instead of new-ing services inside components, you inject abstractions (`IProductService`).
That makes code easier to test and replace later (e.g., swap in EF Core DB service).

---

## 4.2 Root components (`App.razor`, `Routes.razor`)

- `App.razor` defines the HTML shell and includes CSS + Blazor runtime script.
- `Routes.razor` creates the router and chooses layout/page based on URL.

Think of it as:

- `App.razor` = overall HTML document.
- `Routes.razor` = URL -> Component mapping.

---

## 4.3 Shared UI (`MainLayout`, `NavMenu`)

`MainLayout.razor` wraps each page with sidebar + content area.
`NavMenu.razor` contains links using `NavLink`:

- `href=""` => Home page (`/`)
- `href="products"` => CRUD page (`/products`)

`NavLink` automatically applies an active class to the current route.

---

## 4.4 Pages and routing

In Razor components, routes are declared with `@page`.

Examples:

- `@page "/"` in `Home.razor`.
- `@page "/products"` in `Products.razor`.

When browser URL matches, that component is rendered.

---

## 4.5 Model + validation (`Product.cs`)

The `Product` model includes validation attributes:

- `[Required]` for required name.
- `[StringLength]` for max lengths.
- `[Range]` for numeric constraints.

These rules are automatically used by Blazor forms when using `DataAnnotationsValidator`.

---

## 4.6 Service layer (`IProductService`, `InMemoryProductService`)

The app uses a **service abstraction**:

- `IProductService` defines operations (`GetAll`, `GetById`, `Create`, `Update`, `Delete`).
- `InMemoryProductService` stores data in a local list for learning.

### Why an in-memory service?

It keeps the learning focus on Blazor mechanics first.
Later, you can replace it with:

- Entity Framework Core + SQL Server/PostgreSQL.
- Dapper + relational DB.
- HTTP API calls.

Without changing UI logic much.

---

## 4.7 CRUD component (`Products.razor`)

This page shows most important Blazor patterns.

### State fields

- `products`: list displayed in table.
- `formModel`: object bound to form inputs.
- `isEditing`: toggles create/update mode.

### Lifecycle

- `OnInitialized()` runs when component starts.
- Calls `LoadProducts()` to populate table.

### Form binding

`EditForm Model="formModel" OnValidSubmit="HandleSubmit"`

- Inputs use `@bind-Value` two-way binding.
- On submit, Blazor validates model.
- If valid, `HandleSubmit` runs.

### Create/Update flow

`HandleSubmit()` checks `isEditing`:

- false => create new product
- true => update existing product

Then it reloads list + resets form.

### Edit flow

`BeginEdit(id)`:

1. Fetches item from service.
2. Sets `formModel` with existing data.
3. Sets `isEditing = true`.

Now button label changes from Create -> Update.

### Delete flow

`Delete(id)`:

1. Calls service delete.
2. If deleting currently edited row, resets form.
3. Reloads table.

---

## 5) Important Blazor syntax cheat sheet

- `@page "/route"` -> route declaration.
- `@inject ServiceType Name` -> DI in component.
- `@code { ... }` -> C# logic block.
- `@if (...) { ... }` -> conditional rendering.
- `@foreach (...) { ... }` -> list rendering.
- `@bind-Value="model.Prop"` -> two-way binding.
- `@onclick="Handler"` -> event handling.

---

## 6) How rendering works in simple words

Blazor re-renders components when state changes, for example:

- You assign new values to component fields.
- Event handlers finish (`@onclick`, form submit).
- Lifecycle methods run.

In this sample, after CRUD actions we update state (`products`, `formModel`, `isEditing`), and UI refreshes automatically.

---

## 7) Next learning steps (recommended)

1. Move from in-memory list to **EF Core + SQLite**.
2. Add async methods (`Task`, `async/await`) in service + component.
3. Split `Products.razor` into smaller components:
   - `ProductForm.razor`
   - `ProductTable.razor`
4. Add search + pagination.
5. Add unit tests for service and bUnit tests for components.
6. Add authentication/authorization for protected CRUD.

---

## 8) FAQ

### Q: Why does data disappear after restart?
Because current service uses in-memory list only. Use a database for persistence.

### Q: Do I need JavaScript for this CRUD?
Not for this basic scenario. Blazor handles interactivity in C#.

### Q: Is this Blazor Server or WebAssembly?
This sample is configured with interactive server components.

---

## 9) Notes about comments in code

I added explanatory comments in key files (startup, service, and CRUD component).
Avoid over-commenting every obvious line in real production code; comment *intent* and design decisions.

