using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    /// <summary>
    /// Clase de soporte a la clase StringBuilder. Utilizar para consumir menos memoria en el armado de cadenas de texto.
    /// </summary>
    public class StringHelper : System.IDisposable
    {
        private StringBuilder builder;

        public StringHelper(string sentence = "")
        {
            this.builder = new StringBuilder(sentence);
        }

        /// <summary>
        /// Devuelve el tamaño de la cadena
        /// </summary>
        public int Length { get { return this.builder != null ? this.builder.Length : 0; } }

        /// <summary>
        /// Agrega una cadena al final del buffer actual
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public StringHelper Add(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                this.builder.Append(value.ToString());
            }
            return this;
        }

        /// <summary>
        /// Agrega el resultado de un llamado a ToString del parametro value, al final del buffer actual
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public StringHelper Add(object value)
        {
            if (value != null)
            {
                this.builder.Append(value.ToString());
            }
            
            return this;
        }

        /// <summary>
        /// Limpia el buffer actual
        /// </summary>
        /// <returns></returns>
        public StringHelper Clear()
        {
            this.builder.Clear();
            return this;
        }

        /// <summary>
        /// Elimina los ultimos N caracteres de acuerdo al parametro recibido
        /// </summary>
        /// <param name="characterCount"></param>
        /// <returns></returns>
        public StringHelper RemoveLast(int characterCount)
        {
            if (this.builder.Length < characterCount)
            {
                this.builder.Clear();
            }
            else
            {
                this.builder.Remove(this.Length - characterCount, characterCount);
            }
            return this;
        }

        /// <summary>
        /// Busca la primera ocurrencia del selector en el buffer actual y la reemplaza por otra cadena
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public StringHelper ReplaceFirst(string selector, string replacement)
        {
            /* If selector is greater than the text, there is nothing to replace */
            if (this.builder.Length < selector.Length) return this;
            bool foundSelector = false;
            int internalIndex = 0;
            for (int startIndex = 0; startIndex < this.Length && !foundSelector; startIndex++)
            {
                /* Verifica si el selector entra en lo que queda de cadena */
                if (this.Length - 1 - startIndex < selector.Length - 1)
                {
                    /* Si el resultado es negativo quedan menos caracteres de los necesarios */
                    break;
                }

                internalIndex = startIndex;

                /* Recorre los caracteres del selector para ver si coincide con los de la cadena */
                for (int j = 0; j < selector.Length; j++)
                {
                    if (this.builder[internalIndex + j] != selector[j])
                    {
                        /* Terminar el ciclo interno porque no encuentra el selector */
                        break;
                    }

                    if (j == selector.Length - 1)
                    {
                        foundSelector = true;
                    }
                }
            }

            if (foundSelector)
            {
                this.builder.Replace(selector, replacement, internalIndex, selector.Length);
            }

            return this;
        }

        public StringHelper Replace(string selector, string replacement)
        {
            this.builder.Replace(selector, replacement);
            return this;
        }

        /// <summary>
        /// Busca el indice de la ultima ocurrencia de la cadena pasada como parametro
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public int FindLastIndex(string selector)
        {
            const int defaultIndex = -1;

            /* Si el selector es mas largo que el texto, no hay nada que encontrar */
            if (this.builder.Length < selector.Length) return defaultIndex;

            bool keepLooking;
            int internalIndex = 0;
            int endIndex;
            for (endIndex = this.Length - 1; endIndex > 0; endIndex--)
            {
                keepLooking = true;
                internalIndex = endIndex;

                for (int selectorIndex = selector.Length - 1; 
                    internalIndex >= 0 && /* El índice interno debe mantenerse dentro de la cadena del StrinBuilder */
                    selectorIndex >= 0 && /* El índice del selector debe mantenerse dentro de la cadena */
                    keepLooking; /* Mientras esta bandera sea real quiere decir que se van recorriendo los caracteres del selector  
                                  * y se encuentran dentro de la cadena principal
                                  * Cuando es falsa se encontro un caracter no valido
                                  */
                    selectorIndex--)
                {
                    if (selector[selectorIndex] != this.builder[internalIndex])
                    {
                        keepLooking = false;
                    }
                    else if (selectorIndex == 0)
                    {
                        /* Se recorrio todo el selector y los caracteres son iguales, se encontro el resultado, terminar el proceso */
                        return internalIndex;
                    }
                    else
                    {
                        internalIndex -= 1;
                    }
                }


            }

            /* No se encontro el resultado se devuelve el valor por defecto */
            return defaultIndex;
        }

        /// <summary>
        /// Elimina los caracteres contenidos entre los indices pasados como parametro
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public StringHelper Remove(int startIndex, int endIndex)
        {
            /* Si los indices estan invertidos, no hacer nada */
            if (startIndex > endIndex) return this;

            this.builder.Remove(startIndex, endIndex - startIndex);

            return this;
        }

        /// <summary>
        /// Mantiene los caracteres contenidos entre los indices pasados como parametro y descarta el resto
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public StringHelper Substring(int startIndex, int endIndex)
        {
            /* Si se comienza más allá de la posicion 0, cortar los primeros caracteres */
            if (startIndex > 0) this.Remove(0, startIndex);

            /* Si el indice final es mayor o igual que el largo de la cadena, devolver sin más cambios */
            if (endIndex >= this.builder.Length - 1) return this;

            /* Si el indice de fin es menor que el largo de la cadena, cortar los ultimos caracteres */
            this.Remove(endIndex, this.builder.Length - 1);

            return this;
        }

        public string[] ToSubsets(int setSize)
        {
            int startIndex = 0;
            int currentSize;
            List<string> resultList = new List<string>();
            while (startIndex < this.builder.Length)
            {
                currentSize = startIndex + setSize < this.builder.Length ? setSize : this.builder.Length - startIndex;
                resultList.Add(this.builder.ToString(startIndex, currentSize));
                startIndex += currentSize;
            }
            return resultList.ToArray();
        }

        /// <summary>
        /// Convierte el buffer del StringBuilder en una cadena de texto
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.builder.ToString();
        }

        /// <summary>
        /// Sobrecarga del operador + para concatenar cadenas al valor actual del StringBuilder
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="newString"></param>
        /// <returns></returns>
        public static StringHelper operator + (StringHelper builder, string newString) {
		 
            builder.Add(newString);
            return builder;
        }

        /// <summary>
        /// Sobrecarga del operador + para concatenar expresiones de objetos en forma de cadena (Llamado a ToString) al valor actual del StringBuilder
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="newString"></param>
        /// <returns></returns>
        public static StringHelper operator + (StringHelper builder, object value)
        {
            builder.Add(value.ToString());
            return builder;
        }

        /// <summary>
        /// Limpia el StringBuilder interno y lo vuelve una referencia nula
        /// </summary>
        public void Dispose()
        {
            this.builder.Clear();
            this.builder = null;
        }

    }
}
