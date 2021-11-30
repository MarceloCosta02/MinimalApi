using MinimalApi.Data;
using MinimalApi.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

#region GET

app.MapGet("/v1/todos", (Context context) =>
{
    var todos = context.Todos;
    return todos is not null ? Results.Ok(todos) : Results.NotFound();
}).Produces<Todo>();

#endregion

#region POST

app.MapPost("/v1/todos", (Context context, CreateTodoViewModel model) =>
{
    var todo = model.MapTo();
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"/v1/todos/{todo.Id}", todo);
});

#endregion

app.Run();