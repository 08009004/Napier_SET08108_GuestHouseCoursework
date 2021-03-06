﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Represents individuals.
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public class Person : PersonComponent
    {
        // PROPERTIES:

        private String name;
        public override String Name
        {
            get
            {
                return this.name;
            }
        }

        // METHODS:

        /*
         * Constructor.
         * 
         * throws ArgumentException if firstName or secondName is null or
         * equals String.Empty
         */
        public Person(String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("ConcretePerson.Name must not"
                                            + " be null nor String.Empty");
            }

            this.name = name;
        }

        /*
         * Returns a textual representation of the Person object
         * in order to persist it to a CSV file; fields must come
         * in the same order as enumerated in PersonFields.cs
         */
        public override String ToCSV()
        {
            return "#PERSON\r\n" + Name + "\r\n";
        }
    }
}
