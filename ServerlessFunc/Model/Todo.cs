using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace ServerlessFunc
{
    public class Todo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public string TaskDescrition { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class TodoTableEntity : TableEntity
    {
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public string TaskDescrition { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class TodoCreateDto
    {
        public string TaskDescrition { get; set; }
    }

    public class TodoUpdateDto
    {
        public string TaskDescrition { get; set; }
        public bool IsCompleted { get; set; }
    }

    public static class Mappings
    {
        public static TodoTableEntity ToTableEntity(this Todo todo)
        {
            return new TodoTableEntity
            {
                PartitionKey = "TODO",
                RowKey = todo.Id,
                CreatedTime = todo.CreatedTime,
                IsCompleted = todo.IsCompleted,
                TaskDescrition = todo.TaskDescrition
            };
        }

        public static Todo ToTodo(this TodoTableEntity todoTableEntity)
        {
            return new Todo
            {
                Id = todoTableEntity.RowKey,
                CreatedTime = todoTableEntity.CreatedTime,
                IsCompleted = todoTableEntity.IsCompleted,
                TaskDescrition = todoTableEntity.TaskDescrition
            };
        }
    }
}