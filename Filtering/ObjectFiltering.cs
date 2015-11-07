using System.Collections;
using System.Collections.Generic;

namespace ImageProcessing.Filtering
{
    /// <summary>
    /// Filter the detected objects by using several filters. Our objective is to find coffee mugs!
    /// </summary>
    public class ObjectFiltering
    {
        private List<Object> originalObjectList;
        public List<Object> coffeeMugObjectList { get; private set; }
        public ObjectFiltering(List<Object> objects)
        {
            this.originalObjectList = objects;
            coffeeMugObjectList = new List<Object>();
        }

        public void Apply()
        {
            foreach(Object potentialObject in originalObjectList)
            {
                //Checking for valid values. If false, ignore this object and check the next one
                /*
                    Todo:   Currently if one of those values is false, it stops adding the obect to the coffeemug list. If this is fine, keep it. 
                            If only a certain percentage pertentage has to be true for an object to be a cup, add (weighted) counters. 
                */
                if (potentialObject.Area < 25) continue;
                if (potentialObject.Openings != 1) continue;

                coffeeMugObjectList.Add(potentialObject);
            }
            
        }




    }
}
