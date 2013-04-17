namespace Entity
{
    using System;

    public class User
    {
        private string name = "tmc";
        private string password;

        public User(string userid, string pwd)
        {
            this.name = userid;
            this.password = pwd;
        }

        public override string ToString()
        {
            return this.name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }
    }
}

