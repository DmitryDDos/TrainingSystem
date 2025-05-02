using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trSys.Models;

public class Test
{
    private Test () { }
    public Test(int moduleId)
    {
        ModuleId = moduleId;
        //файлы в бинарный поток в через доп метод + мб это вынести в другую таблицу
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; private set;}
    public int ModuleId {get; private set;}

    //навигационные поля
    public Module? Module {get; private set;}
    public IEnumerable<Question> Questions {get; private set;} = new List<Question>();
}
