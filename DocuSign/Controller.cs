using DocuSign.BusinessObject;
using DocuSign.BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DocuSign
{
    class Controller
    {

        public static ControllerStatus STATUS = ControllerStatus.NONE;

        private static Controller SingletonInstance = null;

        private Controller() { }
        
        /**
        * Singleton Pattern Method
        */
        public static Controller GetInstance()
        {
            if (SingletonInstance == null)
                SingletonInstance = new Controller();

            return SingletonInstance;
        }


        // Program logic --------------------------------------
        /**
        * Makes basic valdiations of command input by regular expression
        */
        public bool ValidateInput(string input)
        {
            Regex rx = new Regex(@"^(HOT|COLD)(\s[1-8](,\s?[1-8])*)?$", RegexOptions.IgnoreCase);

            return rx.IsMatch(input);
        }

        /**
        * Processes input, converts commands into Person's methods, and returns oputput string
        */
        public string ProcessCommands(string input)
        {
            // Initial status of process is NONE
            Controller.STATUS = ControllerStatus.NONE;

            string output = "";
            Person person;
            List<int> commandList;

            // Determine temperature, and extract command list
            if (input.ToLower().Contains("hot"))
            {
                person = new Person(Temperature.HOT);
                commandList = input.Replace(" ", string.Empty).Substring(3).Split(',').Select(int.Parse).ToList();
            }
            else
            {
                person = new Person(Temperature.COLD);
                commandList = input.Replace(" ", string.Empty).Substring(4).Split(',').Select(int.Parse).ToList();
            }

            // Process each command
            foreach(int command in commandList)
            {
                try
                {
                    string response = matchCommand(person, command);

                    if (output.Length > 0)
                        output += ", ";
                    output += response;
                }
                catch
                {
                    if (output.Length > 0)
                        output += ", ";
                    output += "fail";

                    Controller.STATUS = ControllerStatus.FAIL;
                    return output;
                } 
            }

            // If this part of the code excecutes, means command list didn't fail
            if (commandList[commandList.Count - 1] != 7)
            {
                // If last command isn't 7 (there isn't a rule for this on the problem), then tell user (that) Person never left home
                Controller.STATUS = ControllerStatus.NEVER_LEFT;
            }
            else
            {
                Controller.STATUS = ControllerStatus.OK;
            }

            return output;
        }

        /**
        * Translates each command into the corresponding Person's method
        */
        private string matchCommand(Person person, int command)
        {
            switch (command)
            {
                case 1: // Footwear
                    person.EquipFootwear();
                    return person.Footwear;
                case 2: // Headwear
                    person.EquipHeadwear();
                    return person.Headwear;
                case 3: // Socks
                    person.EquipSocks();
                    return person.Socks;
                case 4: // Shirt
                    person.EquipShirt();
                    return person.Shirt;
                case 5: // Jacket
                    person.EquipJacket();
                    return person.Jacket;
                case 6: // Pants
                    person.EquipPants();
                    return person.Pants;
                case 7: // Leave house
                    return person.LeaveHouse();
                case 8: // Take off pajamas
                    return person.RemovePajamas();
                default:
                    throw new Exception("Unknown command.");
            }
        }
    }
}
