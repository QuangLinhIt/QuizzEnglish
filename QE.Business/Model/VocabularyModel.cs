using QE.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Model
{
    public class VocabularyModel
    {
        public VocabularyModel() { }
        public VocabularyModel(Vocabulary vocabulary)
        {
            this.Id = vocabulary.Id;
            this.Name = vocabulary.Name;
            this.Meaning = vocabulary.Meaning;
            this.Pronoun = vocabulary.Pronoun;
            this.Image = vocabulary.Image;
            this.Audio = vocabulary.Audio;
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Meaning { get; set; } = null!;
        public string Pronoun { get; set; } = null!;
        public string? Image { get; set; }
        public string? Audio { get; set; }
    }
}
