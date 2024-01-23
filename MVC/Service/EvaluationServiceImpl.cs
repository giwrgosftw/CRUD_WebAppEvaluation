using CRUD_WebAppEvaluation.DbApplicationContext;
using CRUD_WebAppEvaluation.MVC.Repository;
using System.Data;

namespace CRUD_WebAppEvaluation.MVC.Service
{
    public class EvaluationServiceImpl : EvaluationService
    {
        private readonly EvaluationRepository evaluationRepository;
        private readonly ILogger<EvaluationService> logger;

        public EvaluationServiceImpl(EvaluationRepository evaluationRepository, ILogger<EvaluationService> logger)
        {
            this.evaluationRepository = evaluationRepository;
            this.logger = logger;
        }

        public Evaluation FindEvaluationByIdWithException(int id)
        {
            Evaluation evaluation = evaluationRepository.FindById(id);
            if (evaluation == null)
            {
                logger.LogError("[EvaluationService:FindEvaluationById()] Failed: Evaluation with given Id NOT found (Id: {Id})", id);
                throw new KeyNotFoundException();
            }
            return evaluation;
        }

        public Evaluation FindEvaluationById(int id)
        {
            Evaluation evaluation = evaluationRepository.FindById(id);
            if (evaluation == null)
            {
                logger.LogError("[EvaluationService:FindEvaluationById()] Failed: Evaluation with given Id NOT found (Id: {Id})", id);
            }
            return evaluation;
        }


        public List<Evaluation> FindAllEvaluations()
        {
            return evaluationRepository.FindAll();
        }

        public void AddEvaluation(Evaluation evaluation)
        {
            if (evaluation.Id == 0)
            {
                logger.LogError("[EvaluationService:Add()] Exception: Id NOT given.");
                throw new ArgumentException("Id NOT given");
            }
            CheckNotNullAttributesElseThrowException(evaluation.Storeid, evaluation.Detid, evaluation.StartDate);
            Evaluation evaluationDB = evaluationRepository.FindById(evaluation.Id);
            // If evaluation not exist (since new), add new Evaluation
            if (evaluationDB == null)
            {
                evaluationRepository.Add(evaluation);
                logger.LogInformation("[EvaluationService:AddEvaluation()] Success: Evaluation was added successfully!)");
            }
            else
            {
                // Return Error message
                logger.LogError("[EvaluationService:Add()] Exception: Evaluation already exists (Evaluation: {EvaluationId})", evaluation.Id);
                throw new DuplicateNameException();
            }
        }

        public void UpdateEvaluation(Evaluation evaluation)
        {
            CheckNotNullAttributesElseThrowException(evaluation.Storeid, evaluation.Detid, evaluation.StartDate);
            Evaluation evaluationDB = FindEvaluationByIdWithException(evaluation.Id);
            evaluationRepository.Update(evaluationDB, evaluation);
            logger.LogInformation("[EvaluationService:AddEvaluation()] Success: Evaluation (Id: {EvaluationId}) was updated successfully!)", evaluationDB.Id);
        }

        public void DeleteEvaluation(int id)
        {
            Evaluation evaluationDB = FindEvaluationByIdWithException(id);
            evaluationRepository.Delete(evaluationDB);
        }

        private void CheckNotNullAttributesElseThrowException(int? storeId, byte? detId, DateTime? startDate)
        {
            // Check if the NOT nullable ones are not given, so that to inform the developer accordingly
            if (storeId == 0)
            {
                logger.LogError("[EvaluationService:Update()] Exception: StoreId NOT given.");
                throw new ArgumentException("StoreId NOT given");
            }
            else if (detId == 0)
            {
                logger.LogError("[EvaluationService:Update()] Exception: DetId NOT given.");
                throw new ArgumentException("DetId NOT given");
            }
            else if (startDate == DateTime.MinValue)
            {
                logger.LogError("[EvaluationService:Update()] Exception: StartDate NOT given.");
                throw new ArgumentException("StartDate NOT given");
            }
        }
    }
}
