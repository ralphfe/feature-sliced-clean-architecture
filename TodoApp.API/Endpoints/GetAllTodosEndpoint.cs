using Microsoft.OpenApi.Models;
using TodoApp.Application.Features.GetAllTodos;
using TodoApp.Domain.Entities;

namespace TodoApp.API.Endpoints;

public class GetAllTodosEndpoint : IEndpoint
{
    public string Pattern => "/todos";
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Pattern, Handler)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Gets all todo items",
                Description = "Retrieves a list of all todo items",
                Tags = new List<OpenApiTag> { new() { Name = "Todos" } }
            })
            .WithName("GetAllTodos")
            .Produces<IEnumerable<Todo>>(StatusCodes.Status200OK);
    }
    
    private async Task<IResult> Handler(GetAllTodosQueryHandler getAllTodosQuery)
    {
        var todos = await getAllTodosQuery.Execute();
        return Results.Ok(todos);
    }
}