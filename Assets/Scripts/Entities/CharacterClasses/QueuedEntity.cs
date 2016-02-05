using Priority_Queue;


/*
    Node that will contain the entities in the battle. This Node class is required by 
*/

public sealed class QueuedEntity : FastPriorityQueueNode
{
    public Entity entity { get; set; }
    public QueuedEntity(Entity entity)
    {
        this.entity = entity;
    }
} 