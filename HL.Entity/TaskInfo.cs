namespace Entity
{
    using System;
    using System.Collections;

    public class TaskInfo
    {
        private ArrayList details = new ArrayList();
        private string taskID;

        public TaskInfo(string id)
        {
            this.taskID = id;
        }

        public void AddDetail(string[] detail)
        {
            this.details.Add(detail);
        }

        public ArrayList Details
        {
            get
            {
                return this.details;
            }
        }

        public string TaskID
        {
            get
            {
                return this.taskID;
            }
        }
    }
}

