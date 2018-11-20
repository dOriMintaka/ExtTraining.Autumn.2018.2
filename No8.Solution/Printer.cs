using System;

namespace No8.Solution
{
    using System.IO;

    public abstract class Printer : IEquatable<Printer>
    {
        protected Printer(string name, string model)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name), "Null name!");
            this.Model = model ?? throw new ArgumentNullException(nameof(model), "Null model!");
        }

        public string Name { get; }

        public string Model { get; }
        
        public virtual void Print(FileStream fs)
        {
            if (fs == null)
            {
                throw new ArgumentNullException(nameof(fs), "Null file stream!");
            }
            
            for (int i = 0; i < fs.Length; i++)
            {
                Console.WriteLine(fs.ReadByte());
            }
        }

        public bool Equals(Printer other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(this.Name.ToUpperInvariant(), other.Name.ToUpperInvariant()) && string.Equals(this.Model.ToUpperInvariant(), other.Model.ToUpperInvariant());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((Printer)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Name != null ? this.Name.GetHashCode() : 0) * 397) ^ (this.Model != null ? this.Model.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"{this.Name} - {this.Model}";
        }
    }
}
