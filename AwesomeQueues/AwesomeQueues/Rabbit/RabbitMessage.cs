using System;

namespace AwesomeQueues.Rabbit
{
    public class RabbitMessage<T>
    {
        /// <summary>
        /// Datetime registered when action occurs.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Action type.
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        /// Data.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="data"></param>
        public RabbitMessage(string actionType, T data)
        {
            this.Date = DateTime.Now;
            this.ActionType = actionType;
            this.Data = data;
        }
    }
}
