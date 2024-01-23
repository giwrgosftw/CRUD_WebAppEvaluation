using CRUD_WebAppEvaluation.DbApplicationContext;

namespace CRUD_WebAppEvaluation.MVC.Services
{
    public interface EvaluationService
    {
        public Evaluation FindEvaluationByIdWithException(int id);
        public Evaluation FindEvaluationById(int id);
        public List<Evaluation> FindAllEvaluations();
        public void AddEvaluation(Evaluation evaluations);
        public void UpdateEvaluation(Evaluation evaluations);
        public void DeleteEvaluation(int id);
    }
}
