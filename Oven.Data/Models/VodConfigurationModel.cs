using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oven.Data.Models;

public class VodConfigurationModel
{
    [Key]   
    [StringLength(50)]
    public string GameName { get; set; }
    
    public List<Question> Questions { get; set; }

    public VodConfigurationModel(string gameName, List<Question> questions)
    {
        GameName = gameName ?? throw new ArgumentNullException(nameof(gameName));
        Questions = questions ?? throw new ArgumentNullException(nameof(questions));
    }

    public VodConfigurationModel()
    {
        GameName = string.Empty;
        Questions = new List<Question>();
    }
}