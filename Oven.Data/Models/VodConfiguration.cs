using Oven.Data.Models;
using System;
using System.Collections.Generic;

namespace Oven.Data.Models
{
    public class VodConfiguration
    {
        public int VodConfigurationId { get; set; }
        public string GameName { get; set; }
        public List<Question> Questions { get; set; }

        public VodConfiguration(string gameName, List<Question> questions)
        {
            GameName = gameName ?? throw new ArgumentNullException(nameof(gameName));
            Questions = questions ?? throw new ArgumentNullException(nameof(questions));
        }

        public VodConfiguration()
        {
            GameName = string.Empty;
            Questions = new List<Question>();
        }
    }
}