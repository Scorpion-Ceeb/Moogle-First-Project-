namespace MoogleLogic;


public class InputUser
{
    public Operator[] Operators {get; set;}
    public string[] Query {get; set;}

    public InputUser(Operator[] operators, string[] query)
    {
        this.Operators = operators;
        this.Query = query;
    }
    public InputUser()
    {
        this.Operators = new Operator[0];
        this.Query = new string[0];
    }
}