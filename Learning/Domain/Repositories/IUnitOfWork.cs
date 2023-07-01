namespace BackEnd_SocialE.Learning.Domain.Repositories;

public interface IUnitOfWork {
    Task CompleteAsync();
}