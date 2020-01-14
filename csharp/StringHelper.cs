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
                /* Checks if the selector fits in the remaining string */
                if ((this.builder.Length - 1) - startIndex < 0)
                {
                    /* If result is negative, there are fewer characters than needed */
                    break;
                }

                internalIndex = startIndex;

                /* Loops over the selector characters to see if they are cotained in the string */
                for (int j = 0; j < selector.Length; j++)
                {
                    if (this.builder[internalIndex] != selector[j])
                    {
                        /* Selector is not found, so end the internal loop */
                        break;
                    }
                    else if (j == selector.Length - 1)
                    {
                        foundSelector = true;
                    }
                }

                if (foundSelector)
                {
                    /* Selector is found, then finish loop */
                    break;
                }
            }

            /* Replace selector in the string, according to calculated indexes */
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
