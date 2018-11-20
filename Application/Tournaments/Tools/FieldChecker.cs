using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tournaments.Tools
{
    /// <summary>
    /// Checks whether at least one of the given fields contents is empty
    /// </summary>
    public static class FieldChecker
    {
        /// <summary>
        /// Checks whether at least one of the given field contents is empty
        /// </summary>
        /// <param name="fields">an array of field contents</param>
        /// <returns>true if at least one of them is empty, false otherwise</returns>
        public static bool FieldIsEmpty(params object[] fields)
        {
            bool isEmpty = false;
            string message = "Tous les champs doivent être renseignés.";

            foreach (var item in fields)
            {
                if (item is string)
                {
                    isEmpty |= string.IsNullOrEmpty((item as string).Trim());
                }
                else if (item is int)
                {
                    bool noneOrJustOne = (int)item == 0 || (int)item == 1;
                    isEmpty |= noneOrJustOne;
                    message += noneOrJustOne ? " Un tournoi contient au moins 2 participants !" : "";
                }
                else
                {
                    isEmpty |= item == null;
                }
            }
            if (isEmpty)
            {
                MessageBox.Show(message, "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            return isEmpty;
        }
    }
}
