using System.Text;

namespace Helpers
{
    public class StringHelper : System.IDisposable
    {
        private StringBuilder builder;

        public StringHelper(string sentence = "")
        {
            this.builder = new StringBuilder(sentence);
        }

        public int Length => this.builder != null ? this.builder.Length : 0;

        public StringHelper Append(string value)
        {
            this.builder.Append(value);
            return this;
        }

        public StringHelper Clear()
        {
            this.builder.Clear();
            return this;
        }

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

        public StringHelper ReplaceFirst(string selector, string replacement)
        {
            if (this.builder.Length < selector.Length) return this;
            bool foundSelector = false;
            int internalIndex = 0;
            for (int startIndex = 0; startIndex < this.Length; startIndex++)
            {
                /* Verifica si el selector entra en lo que queda de cadena */
                if ((this.builder.Length - 1) - startIndex < 0)
                {
                    /* Si el resultado es negativo quedan menos caracteres de los necesarios */
                    break;
                }

                internalIndex = startIndex;

                /* Recorre los caracteres del selector para ver si coincide con los de la cadena */
                for (int j = 0; j < selector.Length; j++)
                {
                    if (this.builder[internalIndex] != selector[j])
                    {
                        /* Terminar el ciclo interno porque no encuentra el selector */
                        break;
                    }
                    else if (j == selector.Length - 1)
                    {
                        foundSelector = true;
                    }
                }

                if (foundSelector)
                {
                    /* Corta el ciclo principal porque ya encontro la palabra */
                    break;
                }
            }

            this.builder.Replace(selector, replacement, internalIndex, selector.Length);

            return this;
        }

        public override string ToString()
        {
            return this.builder.ToString();
        }

        /* Operator overload */
        public static StringHelper operator + (StringHelper builder, string newString) {
            builder.Append(newString);
            return builder;
        }

        public static StringHelper operator + (StringHelper builder, object value)
        {
            builder.Append(value.ToString());
            return builder;
        }

        public void Dispose()
        {
            this.builder.Clear();
            this.builder = null;
        }

    }
}
