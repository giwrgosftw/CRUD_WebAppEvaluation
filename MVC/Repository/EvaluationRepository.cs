using CRUD_WebAppEvaluation.DbApplicationContext;

namespace CRUD_WebAppEvaluation.MVC.Repository
{
    public interface EvaluationRepository
    {
        Evaluation FindById(int id);
        List<Evaluation> FindAll();
        void Add(Evaluation evaluation);
        void Update(Evaluation existingEvaluation, Evaluation updatedEvaluation);
        void Delete(Evaluation evaluation);
    }
}
