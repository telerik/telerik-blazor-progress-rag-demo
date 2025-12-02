namespace blazor_progress_rag_demo.Services;

/// <summary>
/// Common interface for chat message models consumed by shared components.
/// </summary>
public interface IChatMessage
{
    string Id { get; }
    string Text { get; }
    string AuthorId { get; }
    string AuthorName { get; }
    DateTime Timestamp { get; }
}
