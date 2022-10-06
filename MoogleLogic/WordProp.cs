namespace MoogleLogic;

public class WordProp
{
    //La funcion de esta clase es almacenar los valores necesarios para la funcionalidad del programa

    //Propiedades:
    //TimesInCorpus = Las veces que existe en el corpus de documentos
    //Times =  Las veces que se repite en un documento
    //Score = El valor del TF-IDF de la palabra en el documento
    //Indexes = Los indices de la palabra en el texto para realizar otras funciones
    //PlusValue = Para dar un valor extra al Score cuando se use algun tipo de operador
    //SnippetValue = Fragamento de texto donde se encuentra la palabra a buscar

    //Metodos = Calcular el TF-IDF
    public WordProp(int initial, int timesInCorpus, int firstIndex){
        Times = initial;
        TimesInCorpus = timesInCorpus;
        Indexes = new List<int>();
        Indexes.Add(firstIndex);
    }

#region Fields and Properties
    public int Times{get; set;}
    public int TimesInCorpus {get; set;}
    public float Score{get; set;}
    public List<int> Indexes{get; set;}
    public float PlusValue{get; set;}

#endregion

}