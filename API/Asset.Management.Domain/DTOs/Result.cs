namespace Asset.Management.Domain.DTOs;

public class Result<T>
{
    public Result(T result)
    {
        this.Success = true;
        this.Data = result;
        this.MessagesError = new List<string>(); 
    }

    public Result(string messageError)
    {
        this.Success = false;
        this.MessagesError = new List<string> { messageError }; 
    }

    public Result(IEnumerable<string> messagesError)
    {
        this.Success = false;
        this.MessagesError = messagesError; 
    }

    public T? Data { get; private set; }
    public bool Success { get; private set; }
    public IEnumerable<string> MessagesError { get; private set; }
}