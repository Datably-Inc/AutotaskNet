using AutotaskNet.Domain.ChildEntities;

namespace AutotaskNet.Domain.AttachmentChildEntities;

public abstract class AutotaskAttachmentChildEntity : AutotaskChildEntity
{
    public override string Endpoint => "Attachments";
}