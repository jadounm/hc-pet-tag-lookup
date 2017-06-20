using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTagLookup.Models
{
    public class TagData : IEnumerable<TagData>
    {
        private List<TagData> td = new List<TagData>();

        public string TagNo { get; set; }
        public string MicroNo { get; set; }
        public string OwnerName { get; set; }
        public string Phone { get; set; }
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string Breed { get; set; }
        public string PetColor { get; set; }
        public string VacExpDate { get; set; }
        public string TagExpDate { get; set; }
        
        public IEnumerator<TagData> GetEnumerator()
        {
            return td.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return td.GetEnumerator();
        }
    }
}